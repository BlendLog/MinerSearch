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

        }

        #region Перетаскивание формы

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        #endregion

        void SetupColumns()
        {
            dataGridReviewThreats.ReadOnly = false;
            dataGridReviewThreats.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridReviewThreats.RowHeadersVisible = false;
            dataGridReviewThreats.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridReviewThreats.AllowUserToResizeColumns = true;
            dataGridReviewThreats.AllowUserToResizeRows = false;

            // 1. Выбор действия (ComboBox) — первое, что видит пользователь
            var colNewAction = new DataGridViewComboBoxColumn
            {
                Name = ColNewAction,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_Review_SelectAction"),
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Flat,
                Width = 200
            };

            // Заполняем ComboBox значениями нового enum ScanActionTypeUserSelected
            foreach (ScanActionTypeUserSelected action in Enum.GetValues(typeof(ScanActionTypeUserSelected)))
            {
                colNewAction.Items.Add(GetLocalizedUserActionName(action));
            }

            dataGridReviewThreats.Columns.Add(colNewAction);

            // 2. Рекомендуемое действие
            var colCurrentAction = new DataGridViewTextBoxColumn
            {
                Name = ColCurrentAction,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_Review_RecommendedAction"),
                ReadOnly = true,
                Width = 200
            };
            dataGridReviewThreats.Columns.Add(colCurrentAction);

            // 3. Класс (Process, File, Service и т.д.)
            var colClass = new DataGridViewTextBoxColumn
            {
                Name = ColClass,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Class"),
                ReadOnly = true,
                Width = 120
            };
            dataGridReviewThreats.Columns.Add(colClass);

            // 4. Тип угрозы (Malware, Unsafe и т.д.)
            var colType = new DataGridViewTextBoxColumn
            {
                Name = ColType,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_ObjectType"),
                ReadOnly = true,
                Width = 140
            };
            dataGridReviewThreats.Columns.Add(colType);

            // 5. Путь / имя объекта — последний, заполняет остаток
            var colPath = new DataGridViewTextBoxColumn
            {
                Name = ColPath,
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Path"),
                ReadOnly = true,
                Width = 400
            };
            dataGridReviewThreats.Columns.Add(colPath);

            // Явно помечаем не-ComboBox колонки как ReadOnly
            foreach (var col in dataGridReviewThreats.Columns)
            {
                if (col is DataGridViewComboBoxColumn) continue;
                ((DataGridViewTextBoxColumn)col).ReadOnly = true;
            }
        }

        void ApplyLocalization()
        {
            top.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewInstructions");
            btnApply.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewApplyBtn");
            bulkApplyBtn.Text = AppConfig.GetInstance.LL.GetLocalizedString("_ReviewBulkApplyBtn");
        }

        void PopulateDataGridView()
        {
            foreach (var decision in _decisions)
            {
                if (decision == null || decision.Target == null) continue;

                string type = GetLocalizedObjectType(decision.ObjectType);
                string path = BuildDescription(decision);
                string @class = GetLocalizedClassName(decision.Target.Kind);

                // Действие по умолчанию — на основе флагов threat-объекта, а не ActionType
                // (ActionType по умолчанию == Cured, анализаторы его не выставляют)
                var defaultUserAction = GetDefaultUserActionFromFlags(decision);
                string defaultActionText = GetLocalizedUserActionName(defaultUserAction);

                int rowIndex = dataGridReviewThreats.Rows.Add(
                    defaultActionText, // New Action (Выбор)
                    defaultActionText, // Current Action (Рекомендуемое)
                    @class,            // Class
                    type,              // Type
                    path               // Path
                );

                var row = dataGridReviewThreats.Rows[rowIndex];
                row.Tag = decision;

                // Окрашиваем ячейку типа угрозы сразу после установки Tag
                switch (decision.ObjectType)
                {
                    case ScanObjectType.Malware:
                        row.Cells[ColType].Style.ForeColor = Color.Firebrick;
                        break;
                    case ScanObjectType.Infected:
                    case ScanObjectType.Unsafe:
                        row.Cells[ColType].Style.ForeColor = Color.Red;
                        break;
                    case ScanObjectType.Suspicious:
                        row.Cells[ColType].Style.ForeColor = Color.Orange;
                        break;
                }

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
        /// Возвращает действие по умолчанию на основе флагов threat-объекта
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

        /// <summary>
        /// Локализованное имя класса угрозы (Process, File, Service и т.д.).
        /// </summary>
        private string GetLocalizedClassName(ThreatObjectKind kind)
        {
            try
            {
                switch (kind)
                {
                    case ThreatObjectKind.Process:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Process");
                    case ThreatObjectKind.RegistryObject:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_RegistryObject");
                    case ThreatObjectKind.File:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_File");
                    case ThreatObjectKind.Directory:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Directory");
                    case ThreatObjectKind.FirewallRule:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_FirewallRule");
                    case ThreatObjectKind.UserProfile:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_UserProfile");
                    case ThreatObjectKind.WmiSubscription:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_WmiSubscription");
                    case ThreatObjectKind.ScheduledTask:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_ScheduledTask");
                    case ThreatObjectKind.Service:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Service");
                    case ThreatObjectKind.ShellStartupFile:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_ShellStartupFile");
                    case ThreatObjectKind.Hosts:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Hosts");
                    case ThreatObjectKind.Other:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Other");
                    default:
                        return AppConfig.GetInstance.LL.GetLocalizedString("_ThreatClass_Unknown");
                }
            }
            catch
            {
                return kind.ToString();
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

        private void btnApply_Click(object sender, EventArgs e)
        {
            dataGridReviewThreats.EndEdit();
            _applied = true;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridReviewThreats.CancelEdit();
            _applied = false;
            Close();
        }

        /// <summary>
        /// Возвращает true, если пользователь нажал "Apply" (а не Cancel или X).
        /// </summary>
        public bool IsApplied() => _applied;

        private void FormThreatReview_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Заполняет ComboBox bulk-действия пересечением применимых действий
        /// для ВСЕХ строк в гриде (а не всех значений enum).
        /// </summary>
        private void PopulateBulkComboBox()
        {
            var commonActions = ComputeCommonActionsAllRows();

            // Если пересечение пусто — оставить Delete и Skip
            if (commonActions.Count == 0)
            {
                commonActions.Add(ScanActionTypeUserSelected.Delete);
                commonActions.Add(ScanActionTypeUserSelected.Skip);
            }

            bulkActionComboBox.Items.Clear();
            foreach (var action in commonActions)
            {
                bulkActionComboBox.Items.Add(GetLocalizedUserActionName(action));
            }

            // Устанавливаем первое применимое действие как значение по умолчанию
            if (bulkActionComboBox.Items.Count > 0)
                bulkActionComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Вычисляет пересечение применимых действий для ВСЕХ строк в гриде.
        /// </summary>
        private List<ScanActionTypeUserSelected> ComputeCommonActionsAllRows()
        {
            List<ScanActionTypeUserSelected> common = null;

            foreach (var decision in _decisions)
            {
                if (decision == null || decision.Target == null) continue;

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
        /// Обработчик кнопки "Задать для всех".
        /// Устанавливает выбранное действие в строки, где оно применимо.
        /// </summary>
        private void bulkApplyBtn_Click(object sender, EventArgs e)
        {
            if (bulkActionComboBox.SelectedItem == null) return;

            var userAction = ParseUserActionFromString(bulkActionComboBox.SelectedItem.ToString());

            for (int i = 0; i < dataGridReviewThreats.Rows.Count; i++)
            {
                var row = dataGridReviewThreats.Rows[i];
                if (!(row.Tag is ThreatDecision decision) || decision.Target == null)
                    continue;

                // Проверка: действие должно быть применимо к данному типу угрозы
                var applicable = GetApplicableActions(decision.Target.Kind);
                bool applicableList = false;
                foreach (var a in applicable)
                {
                    if (a == userAction) { applicableList = true; break; }
                }
                if (!applicableList) continue;

                var cell = (DataGridViewComboBoxCell)row.Cells[ColNewAction];
                cell.Value = GetLocalizedUserActionName(userAction);
                decision.UserOverrideAction = userAction;
            }
        }

        /// <summary>
        /// Обновляет bulkActionComboBox: оставляет только действия,
        /// применимые ко ВСЕМ выделенным строкам (пересечение).
        /// </summary>
        private void UpdateBulkComboBoxForSelectedRows()
        {
            var selectedIndices = new List<int>();
            foreach (DataGridViewRow row in dataGridReviewThreats.SelectedRows)
            {
                if (row.Index >= 0)
                    selectedIndices.Add(row.Index);
            }

            if (selectedIndices.Count == 0)
            {
                // Нет выделения — показать все действия
                PopulateBulkComboBox();
                return;
            }

            // Вычислить пересечение применимых действий для всех выделенных строк
            var commonActions = ComputeCommonActions(selectedIndices);

            // Если пересечение пусто — оставить Delete и Skip
            if (commonActions.Count == 0)
            {
                commonActions.Add(ScanActionTypeUserSelected.Delete);
                commonActions.Add(ScanActionTypeUserSelected.Skip);
            }

            // Сохраняем текущий выбор
            var selectedItem = bulkActionComboBox.SelectedItem;
            var selectedText = selectedItem?.ToString();

            // Перезаполняем ComboBox только применимыми действиями
            bulkActionComboBox.Items.Clear();
            foreach (var action in commonActions)
            {
                bulkActionComboBox.Items.Add(GetLocalizedUserActionName(action));
            }

            // Восстанавливаем выбор, если он остался в списке
            if (selectedText != null)
            {
                for (int i = 0; i < bulkActionComboBox.Items.Count; i++)
                {
                    if (bulkActionComboBox.Items[i].ToString() == selectedText)
                    {
                        bulkActionComboBox.SelectedIndex = i;
                        return;
                    }
                }
                // Если текущий выбор не в пересечении — выбрать первый
                bulkActionComboBox.SelectedIndex = 0;
            }
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
        /// Пересекает target с other "на месте".
        /// </summary>
        private static void IntersectLists<T>(List<T> target, IEnumerable<T> other)
        {
            var otherSet = new HashSet<T>(other);
            target.RemoveAll(item => !otherSet.Contains(item));
        }

        #endregion

        private void FormThreatReview_Load(object sender, EventArgs e)
        {
            dataGridReviewThreats.AutoResizeColumns();
            dataGridReviewThreats.SelectionChanged += dataGridReviewThreats_SelectionChanged;
        }

        private void dataGridReviewThreats_SelectionChanged(object sender, EventArgs e)
        {
            UpdateBulkComboBoxForSelectedRows();
        }

        private void dataGridReviewThreats_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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

        private void dataGridReviewThreats_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridReviewThreats.IsCurrentCellDirty && dataGridReviewThreats.CurrentCell?.OwningColumn?.Name == ColNewAction)
            {
                dataGridReviewThreats.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
