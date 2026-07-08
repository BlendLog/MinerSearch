using MSearch.Core;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    public partial class FormThreatReview : FormShadow
    {
        private readonly List<ThreatDecision> _decisions;
        private bool _applied = false;

        // Поля bulk-действия
        private List<int> _highlightedRows = new List<int>();

        // Имена колонок DataGridView
        private const string ColType = "colType";
        private const string ColPath = "colPath";
        private const string ColClass = "colClass";
        private const string ColCurrentAction = "colCurrentAction";
        private const string ColNewAction = "colNewAction";

        public FormThreatReview(List<ThreatDecision> decisions)
        {
            _decisions = decisions;
            InitializeComponent();
            SetupColumns();
            PopulateDataGridView();
            PopulateBulkComboBox();
            ApplyLocalization();
            ApplyBulkLocalization();

            // Подключение событий
            dataGridReviewThreats.CellValueChanged += DataGrid_CellValueChanged;
            dataGridReviewThreats.CurrentCellDirtyStateChanged += DataGrid_CurrentCellDirtyStateChanged;
            btnApply.Click += BtnApply_Click;
            btnCancel.Click += BtnCancel_Click;
            bulkApplyBtn.Click += BulkApplyBtn_Click;
            top.MouseDown += Top_MouseDown;
            this.FormClosing += ReviewForm_Closing;
        }

        #region Перетаскивание формы

        private void Top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        #endregion

        void SetupColumns()
        {
            // Разрешаем редактирование только ComboBox-колонки
            dataGridReviewThreats.ReadOnly = false;
            dataGridReviewThreats.EditMode = DataGridViewEditMode.EditOnEnter;

            // Отключаем столбец выбора строки — убираем лишнюю колонку
            dataGridReviewThreats.RowHeadersVisible = false;
            // CellSelect вместо RowHeaderSelect
            dataGridReviewThreats.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // 2. Тип угрозы (Malware, Unsafe и т.д.)
            var colType = new DataGridViewTextBoxColumn
            {
                Name = ColType,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_ObjectType"),
                ReadOnly = true,
                FillWeight = 20
            };
            dataGridReviewThreats.Columns.Add(colType);

            // 3. Путь / имя объекта
            var colPath = new DataGridViewTextBoxColumn
            {
                Name = ColPath,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Path"),
                ReadOnly = true,
                FillWeight = 40
            };
            dataGridReviewThreats.Columns.Add(colPath);

            // 4. Класс (Process, File, Service и т.д.)
            var colClass = new DataGridViewTextBoxColumn
            {
                Name = ColClass,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Class"),
                ReadOnly = true,
                FillWeight = 12
            };
            dataGridReviewThreats.Columns.Add(colClass);

            // 5. Текущее автоматическое действие
            var colCurrentAction = new DataGridViewTextBoxColumn
            {
                Name = ColCurrentAction,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Action"),
                ReadOnly = true,
                FillWeight = 13
            };
            dataGridReviewThreats.Columns.Add(colCurrentAction);

            // Явно помечаем не-ComboBox колонки как ReadOnly (на случай глобального ReadOnly=false)
            foreach (var col in dataGridReviewThreats.Columns)
            {
                if (col is DataGridViewComboBoxColumn) continue;
                ((DataGridViewTextBoxColumn)col).ReadOnly = true;
            }

            // 6. Выбранное пользователем действие (ComboBox) — только осмысленные операции
            var colNewAction = new DataGridViewComboBoxColumn
            {
                Name = ColNewAction,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_Review_SelectAction"),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Standard,
                FillWeight = 15
            };

            // Заполняем ComboBox значениями нового enum ScanActionTypeUserSelected
            // Храним строковое представление, при чтении парсим обратно в enum
            foreach (ScanActionTypeUserSelected action in Enum.GetValues(typeof(ScanActionTypeUserSelected)))
            {
                colNewAction.Items.Add(GetLocalizedUserActionName(action));
            }

            dataGridReviewThreats.Columns.Add(colNewAction);

            // DataGrid на всю ширину формы
            dataGridReviewThreats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void ApplyLocalization()
        {
            top.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewInstructions");
            btnApply.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewApplyBtn");
            btnCancel.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewCancelBtn");
        }

        void PopulateDataGridView()
        {
            foreach (var decision in _decisions)
            {
                if (decision == null || decision.Target == null) continue;

                string type = GetLocalizedObjectType(decision.ObjectType);
                string path = BuildDescription(decision);
                string @class = decision.Target.Kind.ToString();

                // Действие по умолчанию — на основе флагов threat-объекта, а не ActionType
                // (ActionType по умолчанию == Cured, анализаторы его не выставляют)
                var defaultUserAction = GetDefaultUserActionFromFlags(decision);
                string defaultActionText = GetLocalizedUserActionName(defaultUserAction);

                int rowIndex = dataGridReviewThreats.Rows.Add(
                    type,      // Type
                    path,      // Path
                    @class,    // Class
                    defaultActionText, // Current Action
                    defaultActionText  // New Action (default)
                );

                var row = dataGridReviewThreats.Rows[rowIndex];
                row.Tag = decision;

                // Настраиваем ComboBox: только применимые к данному классу действия
                var cell = (DataGridViewComboBoxCell)row.Cells[ColNewAction];
                var applicableActions = GetApplicableActions(decision.Target.Kind);
                cell.Items.Clear();
                foreach (var action in applicableActions)
                    cell.Items.Add(GetLocalizedUserActionName(action));

                row.Cells[ColNewAction].Value = defaultActionText;
            }

            dataGridReviewThreats.ClearSelection();
        }

        /// <summary>
        /// Маппит ScanActionType (результат анализа) на ScanActionTypeUserSelected (действие пользователя).
        /// Если автоматическое действие — "исцелить", то пользовательское тоже Cure.
        /// </summary>
        private ScanActionTypeUserSelected? MapScanActionToUserAction(ScanActionType action)
        {
            switch (action)
            {
                case ScanActionType.Cured:
                    return ScanActionTypeUserSelected.Cure;
                case ScanActionType.Quarantine:
                    return ScanActionTypeUserSelected.Quarantine;
                case ScanActionType.Deleted:
                    return ScanActionTypeUserSelected.Delete;
                default:
                    // Active, Terminated, Inaccessible, Error, Skipped и т.д. → Skip (оставить как есть)
                    return ScanActionTypeUserSelected.Skip;
            }
        }

        /// <summary>
        /// Возвращает действие по умолчанию на основе флагов threat-объекта,
        /// а не ActionType (который анализаторы не выставляют, и он всегда == Cured).
        /// </summary>
        private ScanActionTypeUserSelected GetDefaultUserActionFromFlags(ThreatDecision decision)
        {
            var target = decision.Target;
            switch (target.Kind)
            {
                case ThreatObjectKind.File:
                    var f = target as FileThreatObject;
                    if (f != null)
                    {
                        if (f.ShouldDeleteFile) return ScanActionTypeUserSelected.Delete;
                        if (f.ShouldMoveFileToQuarantine) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.Directory:
                    var d = target as DirectoryThreatObject;
                    if (d != null)
                    {
                        if (d.ShouldDeleteDirectory || d.ShouldUnlockAndDelete) return ScanActionTypeUserSelected.Delete;
                    }
                    break;

                case ThreatObjectKind.Process:
                    var p = target as ProcessThreatObject;
                    if (p != null && p.ShouldTerminate) return ScanActionTypeUserSelected.Delete;
                    break;

                case ThreatObjectKind.RegistryObject:
                    var r = target as RegistryThreatObject;
                    if (r != null)
                    {
                        if (r.ActionDelete) return ScanActionTypeUserSelected.Delete;
                        if (r.ActionSetData || r.ActionSetSibling) return ScanActionTypeUserSelected.Cure;
                    }
                    break;

                case ThreatObjectKind.Service:
                    var svc = target as ServiceThreatObject;
                    if (svc != null)
                    {
                        if (svc.ShouldDeleteService) return ScanActionTypeUserSelected.Delete;
                        if (svc.ShouldRestoreService || svc.ShouldRestoreServiceDll) return ScanActionTypeUserSelected.Cure;
                        if (svc.ShouldDisableService || svc.ShouldStopService) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.ScheduledTask:
                    var t = target as TaskThreatObject;
                    if (t != null)
                    {
                        if (t.ActionDeleteTask) return ScanActionTypeUserSelected.Delete;
                    }
                    break;

                case ThreatObjectKind.FirewallRule:
                    var fw = target as FirewallRuleThreatObject;
                    if (fw != null && fw.ShouldDelete) return ScanActionTypeUserSelected.Delete;
                    break;

                case ThreatObjectKind.ShellStartupFile:
                    var sf = target as ShellStartupFileThreatObject;
                    if (sf != null)
                    {
                        if (sf.ShouldDeleteFile) return ScanActionTypeUserSelected.Delete;
                        if (sf.ShouldMoveToQuarantine) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.Hosts:
                    var h = target as HostsThreatObject;
                    if (h != null)
                    {
                        if (h.ShouldRemoveInfectedLines) return ScanActionTypeUserSelected.Cure;
                        if (h.ShouldQuarantineFile) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.WmiSubscription:
                    // Любое не-Skip = удаление подписки
                    return ScanActionTypeUserSelected.Delete;
            }
            return ScanActionTypeUserSelected.Skip;
        }

        /// <summary>
        /// Возвращает список действий, применимых к данному классу угрозы.
        /// Cure применимо только к Hosts (очистка строк), Service (восстановление) и Registry (восстановление).
        /// </summary>
        private static ScanActionTypeUserSelected[] GetApplicableActions(ThreatObjectKind kind)
        {
            switch (kind)
            {
                case ThreatObjectKind.Hosts:
                    return new[] { ScanActionTypeUserSelected.Cure, ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.Service:
                case ThreatObjectKind.RegistryObject:
                    return new[] { ScanActionTypeUserSelected.Cure, ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.FirewallRule:
                case ThreatObjectKind.WmiSubscription:
                case ThreatObjectKind.Directory:
                case ThreatObjectKind.UserProfile:
                case ThreatObjectKind.Other:
                    // Только удалить или пропустить
                    return new[] { ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                default:
                    // Process, File, ScheduledTask, ShellStartupFile — карантин, удалить, пропустить (без Cure)
                    return new[] { ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };
            }
        }

        private void DataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Обновляем UserOverrideAction при изменении ComboBox
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (dataGridReviewThreats.Columns[e.ColumnIndex].Name != ColNewAction) return;
            if (e.RowIndex >= dataGridReviewThreats.Rows.Count) return;

            var row = dataGridReviewThreats.Rows[e.RowIndex];
            if (row.Tag is ThreatDecision decision && row.Cells[ColNewAction].Value != null)
            {
                string selectedText = row.Cells[ColNewAction].Value.ToString();
                decision.UserOverrideAction = ParseUserActionFromString(selectedText);
            }
        }

        private void DataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridReviewThreats.IsCurrentCellDirty &&
                dataGridReviewThreats.CurrentCell?.OwningColumn?.Name == ColNewAction)
            {
                dataGridReviewThreats.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private ScanActionTypeUserSelected ParseUserActionFromString(string text)
        {
            foreach (ScanActionTypeUserSelected action in Enum.GetValues(typeof(ScanActionTypeUserSelected)))
            {
                if (GetLocalizedUserActionName(action).Equals(text, StringComparison.OrdinalIgnoreCase))
                    return action;
            }
            return ScanActionTypeUserSelected.Skip;
        }

        private string GetLocalizedObjectType(ScanObjectType type)
        {
            try
            {
                switch (type)
                {
                    case ScanObjectType.Malware:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Malware");
                    case ScanObjectType.Unsafe:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Unsafe");
                    case ScanObjectType.Suspicious:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Suspicious");
                    case ScanObjectType.Infected:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Infected");
                    default:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatType_Unknown");
                }
            }
            catch
            {
                return type.ToString();
            }
        }

        private string GetLocalizedAction(ScanActionType action)
        {
            try
            {
                switch (action)
                {
                    case ScanActionType.Cured:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Cured");
                    case ScanActionType.Quarantine:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Quarantine");
                    case ScanActionType.Deleted:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Deleted");
                    case ScanActionType.Error:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Error");
                    case ScanActionType.Active:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Active");
                    case ScanActionType.Terminated:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Terminated");
                    case ScanActionType.Inaccessible:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Inaccessible");
                    case ScanActionType.Disabled:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Disabled");
                    case ScanActionType.LockedByAntivirus:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_LockedByAV");
                    default:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ActionType_Skipped");
                }
            }
            catch
            {
                return action.ToString();
            }
        }

        /// <summary>
        /// Локализованное имя действия из нового enum ScanActionTypeUserSelected.
        /// </summary>
        private string GetLocalizedUserActionName(ScanActionTypeUserSelected action)
        {
            try
            {
                switch (action)
                {
                    case ScanActionTypeUserSelected.Cure:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Cure");
                    case ScanActionTypeUserSelected.Quarantine:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Quarantine");
                    case ScanActionTypeUserSelected.Delete:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Delete");
                    case ScanActionTypeUserSelected.Skip:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Skip");
                    default:
                        return action.ToString();
                }
            }
            catch
            {
                return action.ToString();
            }
        }

        private string BuildDescription(ThreatDecision decision)
        {
            var target = decision.Target;
            try
            {
                switch (target.Kind)
                {
                    case ThreatObjectKind.Process:
                        var proc = target as ProcessThreatObject;
                        return $"{proc?.ProcessName} - PID: {proc?.ProcessId}";

                    case ThreatObjectKind.ScheduledTask:
                        var task = target as TaskThreatObject;
                        return $"{task?.Info.Path}\\{task?.Info.Name}";

                    case ThreatObjectKind.RegistryObject:
                        var reg = target as RegistryThreatObject;
                        string hiveShort = reg?.Hive == "HKEY_LOCAL_MACHINE" ? "HKLM" : "HKCU";
                        return $@"{hiveShort}\{reg?.KeyPath}\{reg?.ValueName}";

                    case ThreatObjectKind.File:
                        var file = target as FileThreatObject;
                        return file?.FilePath;

                    case ThreatObjectKind.ShellStartupFile:
                        var shellFile = target as ShellStartupFileThreatObject;
                        if (shellFile != null && shellFile.IsShortcut)
                            return $"{shellFile.FileName} --> {shellFile.ShortcutTargetPath}";
                        return shellFile?.FilePath;

                    case ThreatObjectKind.FirewallRule:
                        var fwRule = target as FirewallRuleThreatObject;
                        return fwRule != null ? $"{fwRule.RuleName} → {fwRule.ApplicationName}" : target.Id;

                    case ThreatObjectKind.WmiSubscription:
                        var wmi = target as WmiSubscriptionThreatObject;
                        return wmi != null ? $"{wmi.Name} -> {wmi.CommandLineTemplate}" : target.Id;

                    case ThreatObjectKind.Hosts:
                        var hosts = target as HostsThreatObject;
                        return hosts != null ? hosts.HostsFilePath : target.Id;

                    case ThreatObjectKind.Service:
                        var svc = target as ServiceThreatObject;
                        return svc?.ServiceName ?? target.Id;

                    default:
                        return target.Id;
                }
            }
            catch
            {
                return target.Id;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            dataGridReviewThreats.CancelEdit();
            _applied = false;
            Close();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            dataGridReviewThreats.EndEdit();
            _applied = true;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            dataGridReviewThreats.CancelEdit();
            _applied = false;
            Close();
        }

        /// <summary>
        /// Возвращает true, если пользователь нажал "Apply" (а не Cancel или X).
        /// </summary>
        public bool IsApplied() => _applied;

        private void ReviewForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (!_applied && e.CloseReason == CloseReason.UserClosing)
            {
                dataGridReviewThreats.CancelEdit();
                _applied = false;
            }
        }

        /// <summary>
        /// Применяет выбор пользователя (UserOverrideAction) к флагам действий в каждом ThreatObject.
        /// Вызывается после нажатия "Apply" перед выполнением ApplyDecisions().
        /// </summary>
        public void ApplyUserOverrides()
        {
            foreach (var decision in _decisions)
            {
                if (decision == null || decision.Target == null) continue;
                if (!decision.UserOverrideAction.HasValue) continue;

                ApplyOverrideToTarget(decision);
            }
        }

        private void ApplyOverrideToTarget(ThreatDecision decision)
        {
            var userAction = decision.UserOverrideAction.Value;

            // Skip — пользователь не хочет ничего менять, оставляем автоматическое решение
            if (userAction == ScanActionTypeUserSelected.Skip) return;

            switch (decision.Target.Kind)
            {
                case ThreatObjectKind.File:
                    ApplyOverrideToFile(decision.Target as FileThreatObject, userAction);
                    break;

                case ThreatObjectKind.Directory:
                    ApplyOverrideToFile(decision.Target as DirectoryThreatObject, userAction);
                    break;

                case ThreatObjectKind.ShellStartupFile:
                    ApplyOverrideToFileShell(decision.Target as ShellStartupFileThreatObject, userAction);
                    break;

                case ThreatObjectKind.Process:
                    ApplyOverrideToProcess(decision.Target as ProcessThreatObject, userAction);
                    break;

                case ThreatObjectKind.Service:
                    ApplyOverrideToService(decision.Target as ServiceThreatObject, userAction);
                    break;

                case ThreatObjectKind.RegistryObject:
                    ApplyOverrideToRegistry(decision.Target as RegistryThreatObject, userAction);
                    break;

                case ThreatObjectKind.ScheduledTask:
                    ApplyOverrideToTask(decision.Target as TaskThreatObject, userAction);
                    break;

                case ThreatObjectKind.FirewallRule:
                    ApplyOverrideToFirewall(decision.Target as FirewallRuleThreatObject, userAction);
                    break;

                case ThreatObjectKind.WmiSubscription:
                    // WMI — override через ActionType (Handler обрабатывает по ActionType)
                    decision.ActionType = MapUserActionToScanAction(action: userAction);
                    break;

                case ThreatObjectKind.Hosts:
                    // Hosts — аналогично
                    decision.ActionType = MapUserActionToScanAction(action: userAction);
                    break;

                default:
                    break;
            }
        }

        private ScanActionType MapUserActionToScanAction(ScanActionTypeUserSelected action)
        {
            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    return ScanActionType.Cured;
                case ScanActionTypeUserSelected.Quarantine:
                    return ScanActionType.Quarantine;
                case ScanActionTypeUserSelected.Delete:
                    return ScanActionType.Deleted;
                case ScanActionTypeUserSelected.Skip:
                    return ScanActionType.Skipped;
                default:
                    return ScanActionType.Skipped;
            }
        }

        private void ApplyOverrideToFile(FileThreatObject file, ScanActionTypeUserSelected action)
        {
            if (file == null) return;
            file.ShouldDeleteFile = false;
            file.ShouldMoveFileToQuarantine = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Quarantine:
                    file.ShouldMoveFileToQuarantine = true;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    file.ShouldDeleteFile = true;
                    break;
            }
        }

        private void ApplyOverrideToFile(DirectoryThreatObject dir, ScanActionTypeUserSelected action)
        {
            if (dir == null) return;
            dir.ShouldDeleteDirectory = false;
            dir.ShouldUnlockAndDelete = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Delete:
                    dir.ShouldDeleteDirectory = true;
                    break;
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Quarantine:
                    dir.ShouldUnlockAndDelete = true;
                    break;
            }
        }

        private void ApplyOverrideToFileShell(ShellStartupFileThreatObject shell, ScanActionTypeUserSelected action)
        {
            if (shell == null) return;
            shell.ShouldDeleteFile = false;
            shell.ShouldMoveToQuarantine = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Quarantine:
                    shell.ShouldMoveToQuarantine = true;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    shell.ShouldDeleteFile = true;
                    break;
            }

            // Если это ярлык — применяем к целевому файлу тоже
            if (shell.IsShortcut && shell.ShortcutTargetFile != null)
            {
                ApplyOverrideToFile(shell.ShortcutTargetFile, action);
            }
        }

        private void ApplyOverrideToProcess(ProcessThreatObject proc, ScanActionTypeUserSelected action)
        {
            if (proc == null) return;
            proc.ShouldTerminate = false;
            proc.ShouldSuspend = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Delete:
                case ScanActionTypeUserSelected.Quarantine:
                    proc.ShouldTerminate = true;
                    // Также применяем override к связанному файлу процесса
                    ApplyOverrideToFile(proc.FileProcess, action);
                    break;
            }
        }

        private void ApplyOverrideToService(ServiceThreatObject svc, ScanActionTypeUserSelected action)
        {
            if (svc == null) return;

            // Сохраняем флаги, установленные анализатором
            bool shouldStopServiceWasSet = svc.ShouldStopService;
            bool shouldDisableServiceWasSet = svc.ShouldDisableService;
            bool shouldDeleteServiceWasSet = svc.ShouldDeleteService;
            bool shouldRestoreServiceWasSet = svc.ShouldRestoreService;
            bool shouldRestoreServiceDllWasSet = svc.ShouldRestoreServiceDll;
            bool shouldResetSddlWasSet = svc.ShouldResetSddl;
            bool shouldRemoveFromSafeModeWasSet = svc.ShouldRemoveFromSafeMode;

            // Сбрасываем все флаги
            svc.ShouldStopService = false;
            svc.ShouldDisableService = false;
            svc.ShouldDeleteService = false;
            svc.ShouldRestoreService = false;
            svc.ShouldRestoreServiceDll = false;
            svc.ShouldResetSddl = false;
            svc.ShouldRemoveFromSafeMode = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    // Лечение — восстанавливаем флаги анализатора для восстановления службы
                    svc.ShouldRestoreService = shouldRestoreServiceWasSet;
                    svc.ShouldRestoreServiceDll = shouldRestoreServiceDllWasSet;
                    svc.ShouldResetSddl = shouldResetSddlWasSet;
                    // Также восстанавливаем ShouldRemoveFromSafeMode если было установлено
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    // Удаление — используем флаги анализатора или устанавливаем новые
                    svc.ShouldStopService = shouldStopServiceWasSet || true;
                    svc.ShouldDisableService = shouldDisableServiceWasSet || true;
                    svc.ShouldDeleteService = shouldDeleteServiceWasSet || true;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    // Карантин — останавливаем и отключаем службу
                    svc.ShouldStopService = true;
                    svc.ShouldDisableService = true;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Skip:
                    // Пропускаем — оставляем все флаги сброшенными
                    break;
            }

            // Применяем override к связанным файлам
            if (svc.LinkedServiceFile != null)
                ApplyOverrideToFile(svc.LinkedServiceFile, action);
            if (svc.LinkedServiceDll != null)
                ApplyOverrideToFile(svc.LinkedServiceDll, action);
        }

        private void ApplyOverrideToRegistry(RegistryThreatObject reg, ScanActionTypeUserSelected action)
        {
            if (reg == null) return;

            // Сохраняем флаги, установленные анализатором (для лечения)
            bool actionSetDataWasSet = reg.ActionSetData;
            bool actionSetSiblingWasSet = reg.ActionSetSibling;
            bool actionUnlockFirstWasSet = reg.ActionUnlockFirst;
            bool actionDeleteParentKeyWasSet = reg.ActionDeleteParentKey;

            // Сбрасываем только флаги удаления
            reg.ActionDelete = false;
            reg.ActionSetData = false;
            reg.ActionSetSibling = false;
            reg.ActionUnlockFirst = false;
            reg.ActionDeleteParentKey = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    // Лечение — восстанавливаем флаги, установленные анализатором
                    reg.ActionSetData = actionSetDataWasSet;
                    reg.ActionSetSibling = actionSetSiblingWasSet;
                    reg.ActionUnlockFirst = actionUnlockFirstWasSet;
                    reg.ActionDeleteParentKey = actionDeleteParentKeyWasSet;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    reg.ActionDelete = true;
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    reg.ActionDelete = true;
                    break;
                case ScanActionTypeUserSelected.Skip:
                    // Пропускаем — оставляем все флаги сброшенными
                    break;
            }

            if (reg.LinkedFile != null)
                ApplyOverrideToFile(reg.LinkedFile, action);
        }

        private void ApplyOverrideToTask(TaskThreatObject task, ScanActionTypeUserSelected action)
        {
            if (task == null) return;

            // Сохраняем флаги, установленные анализатором
            bool actionDeleteTaskWasSet = task.ActionDeleteTask;
            bool actionDeleteFileWasSet = task.ActionDeleteFile;
            bool actionDeleteAdditionalFileWasSet = task.ActionDeleteAdditionalFile;

            // Сбрасываем флаги
            task.ActionDeleteTask = false;
            task.ActionDeleteFile = false;
            task.ActionDeleteAdditionalFile = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Delete:
                    // Восстанавливаем флаги для лечения/удаления
                    task.ActionDeleteTask = actionDeleteTaskWasSet || actionDeleteFileWasSet;
                    task.ActionDeleteFile = actionDeleteFileWasSet;
                    task.ActionDeleteAdditionalFile = actionDeleteAdditionalFileWasSet;
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    // Для карантина удаляем только задачу, файл помещаем в карантин
                    task.ActionDeleteTask = true;
                    // Файл будет обработан через ApplyOverrideToFile (карантин)
                    break;
                case ScanActionTypeUserSelected.Skip:
                    // Пропускаем — оставляем флаги сброшенными
                    break;
            }

            if (task.LinkedFile != null)
                ApplyOverrideToFile(task.LinkedFile, action);
            if (task.LinkedFileFromArgs != null)
                ApplyOverrideToFile(task.LinkedFileFromArgs, action);
        }

        private void ApplyOverrideToFirewall(FirewallRuleThreatObject fw, ScanActionTypeUserSelected action)
        {
            if (fw == null) return;
            // Для firewall все действия кроме Skip — удаление правила
            if (action != ScanActionTypeUserSelected.Skip)
                fw.ShouldDelete = true;
        }

        #region Bulk Action

        /// <summary>
        /// Заполняет ComboBox bulk-действия действиями из ScanActionTypeUserSelected.
        /// По умолчанию выбрано Quarantine.
        /// </summary>
        private void PopulateBulkComboBox()
        {
            bulkActionComboBox.Items.Clear();
            foreach (ScanActionTypeUserSelected action in Enum.GetValues(typeof(ScanActionTypeUserSelected)))
            {
                bulkActionComboBox.Items.Add(GetLocalizedUserActionName(action));
            }
            // Устанавливаем Quarantine как значение по умолчанию
            bulkActionComboBox.SelectedIndex = (int)ScanActionTypeUserSelected.Quarantine;
        }

        /// <summary>
        /// Применяет локализацию к bulk-элементам.
        /// </summary>
        private void ApplyBulkLocalization()
        {
            bulkLabel.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkLabel");
            bulkApplyBtn.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkApplyBtn");
        }

        /// <summary>
        /// Обработчик кнопки "Задать для всех".
        /// Устанавливает выбранное действие в per-row ComboBox для применимых строк,
        /// подсвечивает неприменимые строки жёлтым.
        /// </summary>
        private void BulkApplyBtn_Click(object sender, EventArgs e)
        {
            // Сбросить подсветку предыдущих строк
            ClearHighlights();
            _highlightedRows.Clear();

            if (bulkActionComboBox.SelectedItem == null) return;

            var userAction = ParseUserActionFromString(bulkActionComboBox.SelectedItem.ToString());
            bool hasHighlighted = false;

            for (int i = 0; i < dataGridReviewThreats.Rows.Count; i++)
            {
                var row = dataGridReviewThreats.Rows[i];
                if (!(row.Tag is ThreatDecision decision) || decision.Target == null)
                    continue;

                var applicableActions = GetApplicableActions(decision.Target.Kind);
                bool isApplicable = ArrayContains(applicableActions, userAction);

                var cell = (DataGridViewComboBoxCell)row.Cells[ColNewAction];

                if (isApplicable)
                {
                    // Устанавливаем выбранное действие
                    cell.Value = GetLocalizedUserActionName(userAction);
                    decision.UserOverrideAction = userAction;
                }
                else
                {
                    // Подсвечиваем неприменимую строку
                    cell.Style.BackColor = Color.Yellow;
                    _highlightedRows.Add(i);
                    hasHighlighted = true;
                }
            }

            // Если есть неприменимые строки — показать диалог
            if (hasHighlighted)
            {
                ShowBulkActionDialog();
            }
        }

        /// <summary>
        /// Показывает диалог с вариантами действий для неприменимых строк.
        /// Варианты — пересечение применимых действий для всех неприменимых строк.
        /// </summary>
        private void ShowBulkActionDialog()
        {
            // Вычислить пересечение применимых действий для всех подсвеченных строк
            var commonActions = ComputeCommonActions(_highlightedRows);

            // Если пересечение пусто — показать только Delete и Skip
            if (commonActions.Count == 0)
            {
                commonActions.Add(ScanActionTypeUserSelected.Delete);
                commonActions.Add(ScanActionTypeUserSelected.Skip);
            }

            // Показать диалог
            var result = DialogDispatcher.Show(
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkIncompatible"),
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewTitle"),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                Color.RoyalBlue);

            if (result == DialogResult.Cancel)
            {
                // Отмена — сбросить подсветку и значения
                ClearHighlights();
                return;
            }

            // Показать поддиалог с выбором действия
            ApplyActionToHighlightedRows(commonActions);
        }

        /// <summary>
        /// Вычисляет пересечение применимых действий для списка строк.
        /// </summary>
        private List<ScanActionTypeUserSelected> ComputeCommonActions(List<int> rowIndices)
        {
            if (rowIndices.Count == 0) return new List<ScanActionTypeUserSelected>();

            List<ScanActionTypeUserSelected> common = null;

            foreach (int index in rowIndices)
            {
                var row = dataGridReviewThreats.Rows[index];
                if (!(row.Tag is ThreatDecision decision) || decision.Target == null)
                    continue;

                var applicable = GetApplicableActions(decision.Target.Kind);
                var applicableList = new List<ScanActionTypeUserSelected>(applicable);

                if (common == null)
                {
                    common = applicableList;
                }
                else
                {
                    IntersectLists(common, applicableList);
                }
            }

            return common ?? new List<ScanActionTypeUserSelected>();
        }

        /// <summary>
        /// Применяет выбранное действие к подсвеченным строкам через поддиалог.
        /// </summary>
        private static void IntersectLists<T>(List<T> target, IEnumerable<T> other)
        {
            var otherSet = new HashSet<T>(other);
            target.RemoveAll(item => !otherSet.Contains(item));
        }

        private void ApplyActionToHighlightedRows(List<ScanActionTypeUserSelected> commonActions)
        {
            // Создаём временную форму-диалог с кнопками
            using (var dialog = new BulkActionDialog(commonActions))
            {
                var result = dialog.ShowDialog(this);
                if (result == DialogResult.OK && dialog.SelectedAction.HasValue)
                {
                    var action = dialog.SelectedAction.Value;
                    foreach (int index in _highlightedRows)
                    {
                        var row = dataGridReviewThreats.Rows[index];
                        if (!(row.Tag is ThreatDecision decision) || decision.Target == null)
                            continue;

                        var cell = (DataGridViewComboBoxCell)row.Cells[ColNewAction];
                        cell.Value = GetLocalizedUserActionName(action);
                        cell.Style.BackColor = Color.White;
                        decision.UserOverrideAction = action;
                    }
                }
                else
                {
                    // Отмена — сбросить подсветку
                    ClearHighlights();
                }
            }
        }

        private void ClearHighlights()
        {
            foreach (int index in _highlightedRows)
            {
                var row = dataGridReviewThreats.Rows[index];
                if (row.Cells[ColNewAction] is DataGridViewComboBoxCell cell)
                {
                    cell.Style.BackColor = Color.White;
                }
            }
            _highlightedRows.Clear();
        }

        private static bool ArrayContains<T>(T[] array, T value)
        {
            foreach (var item in array)
            {
                if (item.Equals(value)) return true;
            }
            return false;
        }

        #endregion
    }

    /// <summary>
    /// Вспомогательная форма-диалог для выбора действия из пересечения применимых действий.
    /// </summary>
    internal class BulkActionDialog : Form
    {
        public ScanActionTypeUserSelected? SelectedAction { get; private set; }

        public BulkActionDialog(List<ScanActionTypeUserSelected> actions)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewTitle");
            ShowInTaskbar = false;
            MaximizeBox = false;
            MinimizeBox = false;

            var flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var messageLabel = new Label
            {
                Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkIncompatible"),
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Verdana", 9F, FontStyle.Bold),
                Padding = new Padding(0, 10, 0, 10)
            };
            flowPanel.Controls.Add(messageLabel);

            foreach (var action in actions)
            {
                var btn = new RoundButton
                {
                    Text = GetLocalizedUserActionName(action),
                    Dock = DockStyle.Top,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = Color.RoyalBlue,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Height = 36,
                    Margin = new Padding(0, 2, 0, 2)
                };
                btn.FlatAppearance.BorderSize = 0;

                btn.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
                btn.FlatAppearance.MouseDownBackColor = Color.Navy;
                btn.Click += (s, e) =>
                {
                    SelectedAction = action;
                    DialogResult = DialogResult.OK;
                    Close();
                };

                flowPanel.Controls.Add(btn);
            }

            // Кнопка "Отмена"
            var cancelBtn = new RoundButton
            {
                Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkAction_Cancel"),
                Dock = DockStyle.Bottom,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 36,
                Margin = new Padding(0, 2, 0, 5)
            };
            cancelBtn.FlatAppearance.BorderSize = 0;
            cancelBtn.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            cancelBtn.Click += (s, e) =>
            {
                SelectedAction = null;
                DialogResult = DialogResult.Cancel;
                Close();
            };
            flowPanel.Controls.Add(cancelBtn);

            Controls.Add(flowPanel);
            Size = new Size(400, Math.Max(200, flowPanel.PreferredSize.Height + 20));
        }

        private static string GetLocalizedUserActionName(ScanActionTypeUserSelected action)
        {
            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Cure");
                case ScanActionTypeUserSelected.Quarantine:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Quarantine");
                case ScanActionTypeUserSelected.Delete:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Delete");
                case ScanActionTypeUserSelected.Skip:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAction_Skip");
                default:
                    return action.ToString();
            }
        }
    }
}
