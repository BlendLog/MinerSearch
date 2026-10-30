using DBase;
using MSearch.Core;
using MSearch.Core.ThreatDecisions;
using MSearch.Core.ThreatObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Промежуточная страница обзора неизвестных угроз.
    /// Заменяет отдельное окно FormThreatReview.
    /// </summary>
    public partial class ThreatReviewPage : UserControl
    {
        public event EventHandler Applied;
        public event EventHandler ContinueRequested;
        public event EventHandler SkipAllRequested;

        private List<ThreatDecision> _decisions = new List<ThreatDecision>();
        private bool _suppressActionChanged = false;

        // Применимые действия по индексу строки (порядок совпадает с items ComboBox-ячейки)
        private readonly Dictionary<int, List<ScanActionTypeUserSelected>> _rowActions =
            new Dictionary<int, List<ScanActionTypeUserSelected>>();

        // Действия bulk-списка (порядок совпадает с items comboBulk)
        private readonly List<ScanActionTypeUserSelected> _bulkActions = new List<ScanActionTypeUserSelected>();

        private const string ColType = "colType";
        private const string ColPath = "colPath";
        private const string ColClass = "colClass";
        private const string ColCurrentAction = "colCurrentAction";
        private const string ColNewAction = "colNewAction";

        public ThreatReviewPage()
        {
            InitializeComponent();
            SetupColumns();

            if (!IsInDesignMode)
                ApplyLanguage();
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void panelGridHost_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelGridHost.Width - 1, panelGridHost.Height - 1);
            }
        }

        private void dataGridReview_SelectionChanged(object sender, EventArgs e)
        {
            UpdateBulkComboBoxForSelectedRows();
        }

        /// <summary>
        /// Заполняет страницу новым набором решений.
        /// </summary>
        public void InitializeReview(List<ThreatDecision> decisions)
        {
            _decisions = decisions ?? new List<ThreatDecision>();

            _suppressActionChanged = true;
            try
            {
                dataGridReview.Rows.Clear();
                PopulateRows();
                PopulateBulkComboBox();
            }
            finally
            {
                _suppressActionChanged = false;
            }

            dataGridReview.ClearSelection();
            UpdateSetRecommendedButtonState();
        }

        private void SetupColumns()
        {
            dataGridReview.ReadOnly = false;
            dataGridReview.Columns.Clear();

            DataGridViewComboBoxColumn colNewAction = new DataGridViewComboBoxColumn
            {
                Name = ColNewAction,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18F,
                MinimumWidth = 170
            };
            dataGridReview.Columns.Add(colNewAction);

            dataGridReview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColCurrentAction,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 18F,
                MinimumWidth = 170
            });

            dataGridReview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColClass,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 13F,
                MinimumWidth = 130
            });

            dataGridReview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColType,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 13F,
                MinimumWidth = 150
            });

            dataGridReview.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColPath,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 38F,
                MinimumWidth = 500
            });

            // Путь тянется мышью и может выйти за ширину окна (h-scroll),
            // чтобы можно было прочитать полный путь.
            DataGridViewColumn reviewPathColumn = dataGridReview.Columns[ColPath];
            reviewPathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            reviewPathColumn.Width = 500;
            reviewPathColumn.MinimumWidth = 300;
            reviewPathColumn.Resizable = DataGridViewTriState.True;

            dataGridReview.ScrollBars = ScrollBars.Both;

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 251),
                ForeColor = Theme.Muted,
                Font = Theme.BodyBold,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
            dataGridReview.ColumnHeadersDefaultCellStyle = headerStyle;

            dataGridReview.DefaultCellStyle.Font = Theme.Body;
            dataGridReview.DefaultCellStyle.BackColor = Color.White;
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_ReviewTitle");
            labelInstruction.Text = L("_ReviewInstructions");

            btnApply.Text = L("_ReviewApplyBtn");
            btnBulkApply.Text = L("_ReviewBulkApplyBtn");
            btnSetRecommended.Text = L("_ReviewSetRecommendedBtn");
            btnContinue.Text = L("_ReviewContinueBtn");
            btnSkipAll.Text = L("_ReviewSkipAllBtn");

            if (dataGridReview.Columns.Count > 0)
            {
                dataGridReview.Columns[ColNewAction].HeaderText = L("_Review_SelectAction");
                dataGridReview.Columns[ColCurrentAction].HeaderText = L("_Review_RecommendedAction");
                dataGridReview.Columns[ColClass].HeaderText = L("_DataGridHeader_Class");
                dataGridReview.Columns[ColType].HeaderText = L("_DataGridHeader_ObjectType");
                dataGridReview.Columns[ColPath].HeaderText = L("_DataGridHeader_Path");
            }
        }

        private void PopulateRows()
        {
            _rowActions.Clear();

            foreach (ThreatDecision decision in _decisions)
            {
                if (decision == null || decision.Target == null)
                    continue;

                string type = GetLocalizedObjectType(decision.ObjectType);
                string path = BuildDescription(decision);
                string @class = GetLocalizedClassName(decision.Target.Kind);

                ScanActionTypeUserSelected defaultUserAction = GetDefaultUserActionFromFlags(decision);
                string defaultActionText = GetLocalizedUserActionName(defaultUserAction);

                int rowIndex = dataGridReview.Rows.Add(
                    defaultActionText,
                    defaultActionText,
                    @class,
                    type,
                    path);

                DataGridViewRow row = dataGridReview.Rows[rowIndex];
                row.Tag = decision;

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

                ScanActionTypeUserSelected[] applicableActions = GetApplicableActions(decision.Target.Kind, decision.Target);
                List<ScanActionTypeUserSelected> actions = new List<ScanActionTypeUserSelected>(applicableActions);
                _rowActions[rowIndex] = actions;

                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells[ColNewAction];
                cell.Items.Clear();
                foreach (ScanActionTypeUserSelected action in applicableActions)
                    cell.Items.Add(GetLocalizedUserActionName(action));

                row.Cells[ColNewAction].Value = defaultActionText;
            }
        }

        private ScanActionTypeUserSelected GetUserActionFromRow(DataGridViewRow row)
        {
            object value = row.Cells[ColNewAction].Value;
            if (value == null)
                return ScanActionTypeUserSelected.Skip;

            List<ScanActionTypeUserSelected> actions;
            if (!_rowActions.TryGetValue(row.Index, out actions))
                return ScanActionTypeUserSelected.Skip;

            DataGridViewComboBoxCell cell = row.Cells[ColNewAction] as DataGridViewComboBoxCell;
            if (cell == null)
                return ScanActionTypeUserSelected.Skip;

            int index = cell.Items.IndexOf(value);
            if (index < 0 || index >= actions.Count)
                return ScanActionTypeUserSelected.Skip;

            return actions[index];
        }

        private void dataGridReview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;
            if (dataGridReview.Columns[e.ColumnIndex].Name != ColNewAction)
                return;
            if (e.RowIndex >= dataGridReview.Rows.Count)
                return;
            if (_suppressActionChanged)
                return;

            DataGridViewRow row = dataGridReview.Rows[e.RowIndex];
            ThreatDecision decision = row.Tag as ThreatDecision;
            if (decision != null && row.Cells[ColNewAction].Value != null)
                decision.UserOverrideAction = GetUserActionFromRow(row);

            UpdateSetRecommendedButtonState();
        }

        private void dataGridReview_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridReview.IsCurrentCellDirty &&
                dataGridReview.CurrentCell != null &&
                dataGridReview.CurrentCell.OwningColumn != null &&
                dataGridReview.CurrentCell.OwningColumn.Name == ColNewAction)
            {
                dataGridReview.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        #region Кнопки страницы

        private void btnApply_Click(object sender, EventArgs e)
        {
            dataGridReview.EndEdit();
            ApplyUserOverrides();

            EventHandler handler = Applied;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            dataGridReview.CancelEdit();
            ResetUserOverrides();

            EventHandler handler = ContinueRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void btnSkipAll_Click(object sender, EventArgs e)
        {
            dataGridReview.CancelEdit();
            ApplySkipAll();

            EventHandler handler = SkipAllRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        #endregion

        #region Действия по умолчанию и применимость

        private ScanActionTypeUserSelected GetDefaultUserActionFromFlags(ThreatDecision decision)
        {
            IThreatObject target = decision.Target;
            switch (target.Kind)
            {
                case ThreatObjectKind.File:
                    FileThreatObject f = target as FileThreatObject;
                    if (f != null)
                    {
                        if (f.ShouldDeleteFile) return ScanActionTypeUserSelected.Delete;
                        if (f.ShouldMoveFileToQuarantine) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.Directory:
                    DirectoryThreatObject d = target as DirectoryThreatObject;
                    if (d != null)
                    {
                        if (d.ShouldDeleteDirectory || d.ShouldUnlockAndDelete) return ScanActionTypeUserSelected.Delete;
                    }
                    break;

                case ThreatObjectKind.Process:
                    ProcessThreatObject p = target as ProcessThreatObject;
                    if (p != null && p.ShouldTerminate) return ScanActionTypeUserSelected.Terminate;
                    break;

                case ThreatObjectKind.RegistryObject:
                    RegistryThreatObject r = target as RegistryThreatObject;
                    if (r != null)
                    {
                        if (r.ActionQuarantine) return ScanActionTypeUserSelected.Quarantine;
                        if (r.ActionSetData || r.ActionSetSibling || r.ActionRemoveDefenderExclusion)
                            return ScanActionTypeUserSelected.Cure;
                        if (r.ActionDelete || r.ActionDeleteParentKey)
                            return ScanActionTypeUserSelected.Delete;
                    }
                    break;

                case ThreatObjectKind.Service:
                    ServiceThreatObject svc = target as ServiceThreatObject;
                    if (svc != null)
                    {
                        if (svc.ShouldQuarantineService) return ScanActionTypeUserSelected.Quarantine;
                        if (svc.ShouldDeleteService) return ScanActionTypeUserSelected.Delete;
                        if (svc.ShouldRestoreService || svc.ShouldRestoreServiceDll) return ScanActionTypeUserSelected.Cure;
                        if (svc.ShouldDisableService || svc.ShouldStopService) return ScanActionTypeUserSelected.Disable;
                    }
                    break;

                case ThreatObjectKind.ScheduledTask:
                    TaskThreatObject t = target as TaskThreatObject;
                    if (t != null)
                    {
                        if (t.ActionQuarantineTask) return ScanActionTypeUserSelected.Quarantine;
                        if (t.ActionDeleteTask) return ScanActionTypeUserSelected.Delete;
                    }
                    break;

                case ThreatObjectKind.FirewallRule:
                    FirewallRuleThreatObject fw = target as FirewallRuleThreatObject;
                    if (fw != null && fw.ShouldDelete) return ScanActionTypeUserSelected.Delete;
                    break;

                case ThreatObjectKind.ShellStartupFile:
                    ShellStartupFileThreatObject sf = target as ShellStartupFileThreatObject;
                    if (sf != null)
                    {
                        if (sf.ShouldDeleteFile) return ScanActionTypeUserSelected.Delete;
                        if (sf.ShouldMoveToQuarantine) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.Hosts:
                    HostsThreatObject h = target as HostsThreatObject;
                    if (h != null)
                    {
                        if (h.ShouldRemoveInfectedLines) return ScanActionTypeUserSelected.Cure;
                        if (h.ShouldQuarantineFile) return ScanActionTypeUserSelected.Quarantine;
                    }
                    break;

                case ThreatObjectKind.WmiSubscription:
                    return ScanActionTypeUserSelected.Delete;

                case ThreatObjectKind.UserProfile:
                    UserProfileThreatObject up = target as UserProfileThreatObject;
                    if (up != null && up.ShouldDelete) return ScanActionTypeUserSelected.Delete;
                    break;
            }

            return ScanActionTypeUserSelected.Skip;
        }

        private static ScanActionTypeUserSelected[] GetApplicableActions(ThreatObjectKind kind, IThreatObject target)
        {
            switch (kind)
            {
                case ThreatObjectKind.Hosts:
                    return new[] { ScanActionTypeUserSelected.Cure, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.RegistryObject:
                    RegistryThreatObject reg = target as RegistryThreatObject;
                    if (reg != null)
                    {
                        if (reg.ActionSetData || reg.ActionSetSibling || reg.ActionRemoveDefenderExclusion)
                            return new[] { ScanActionTypeUserSelected.Cure, ScanActionTypeUserSelected.Skip };

                        if (reg.ActionDelete || reg.ActionDeleteParentKey)
                            return new[] { ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };
                    }
                    return new[] { ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.Service:
                    if (target is ServiceThreatObject svc && svc.ServiceName.Equals(MSData.GetInstance.consts[MSKeys.TermServiceName], StringComparison.OrdinalIgnoreCase))
                        return new[] { ScanActionTypeUserSelected.Cure, ScanActionTypeUserSelected.Disable, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };
                    return new[] { ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Disable, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.FirewallRule:
                case ThreatObjectKind.WmiSubscription:
                case ThreatObjectKind.Directory:
                case ThreatObjectKind.UserProfile:
                    return new[] { ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.ScheduledTask:
                    return new[] { ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };

                case ThreatObjectKind.Process:
                    return new[] { ScanActionTypeUserSelected.Terminate, ScanActionTypeUserSelected.Skip };

                default:
                    return new[] { ScanActionTypeUserSelected.Quarantine, ScanActionTypeUserSelected.Delete, ScanActionTypeUserSelected.Skip };
            }
        }

        #endregion

        #region Локализованные подписи

        private string GetLocalizedObjectType(ScanObjectType type)
        {
            switch (type)
            {
                case ScanObjectType.Malware:
                    return L("_ThreatType_Malware");
                case ScanObjectType.Unsafe:
                    return L("_ThreatType_Unsafe");
                case ScanObjectType.Suspicious:
                    return L("_ThreatType_Suspicious");
                case ScanObjectType.Infected:
                    return L("_ThreatType_Infected");
                default:
                    return L("_ThreatType_Unknown");
            }
        }

        private string GetLocalizedUserActionName(ScanActionTypeUserSelected action)
        {
            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    return L("_Action_Cure");
                case ScanActionTypeUserSelected.Quarantine:
                    return L("_Quarantine");
                case ScanActionTypeUserSelected.Delete:
                    return L("_Action_Delete");
                case ScanActionTypeUserSelected.Terminate:
                    return L("_Action_Terminate");
                case ScanActionTypeUserSelected.Disable:
                    return L("_Action_Disable");
                case ScanActionTypeUserSelected.Skip:
                    return L("_Action_Skip");
                default:
                    return action.ToString();
            }
        }

        private string GetLocalizedClassName(ThreatObjectKind kind)
        {
            switch (kind)
            {
                case ThreatObjectKind.Process:
                    return L("_ObjectType_Process");
                case ThreatObjectKind.RegistryObject:
                    return L("_ObjectType_RegistryObject");
                case ThreatObjectKind.File:
                    return L("_ObjectType_File");
                case ThreatObjectKind.Directory:
                    return L("_ObjectType_Directory");
                case ThreatObjectKind.FirewallRule:
                    return L("_ObjectType_FirewallRule");
                case ThreatObjectKind.UserProfile:
                    return L("_ObjectType_UserProfile");
                case ThreatObjectKind.WmiSubscription:
                    return L("_ObjectType_WmiSubscription");
                case ThreatObjectKind.ScheduledTask:
                    return L("_ObjectType_ScheduledTask");
                case ThreatObjectKind.Service:
                    return L("_ObjectType_Service");
                case ThreatObjectKind.ShellStartupFile:
                    return L("_ObjectType_ShellStartupFile");
                case ThreatObjectKind.Hosts:
                    return L("_ObjectType_Hosts");
                case ThreatObjectKind.Other:
                    return L("_ObjectType_Other");
                default:
                    return L("_ObjectType_Unknown");
            }
        }

        private string BuildDescription(ThreatDecision decision)
        {
            IThreatObject target = decision.Target;
            try
            {
                switch (target.Kind)
                {
                    case ThreatObjectKind.Process:
                        ProcessThreatObject proc = target as ProcessThreatObject;
                        return proc != null ? proc.ProcessName + " - PID: " + proc.ProcessId : target.Id;

                    case ThreatObjectKind.ScheduledTask:
                        TaskThreatObject task = target as TaskThreatObject;
                        return task != null ? task.Info.Path + "\\" + task.Info.Name : target.Id;

                    case ThreatObjectKind.RegistryObject:
                        RegistryThreatObject reg = target as RegistryThreatObject;
                        string hiveShort = reg != null && reg.Hive == "HKEY_LOCAL_MACHINE" ? "HKLM" : "HKCU";
                        return reg != null ? hiveShort + "\\" + reg.KeyPath + "\\" + reg.ValueName : target.Id;

                    case ThreatObjectKind.File:
                        FileThreatObject file = target as FileThreatObject;
                        return file != null ? file.FilePath : target.Id;

                    case ThreatObjectKind.ShellStartupFile:
                        ShellStartupFileThreatObject shellFile = target as ShellStartupFileThreatObject;
                        if (shellFile != null && shellFile.IsShortcut)
                            return shellFile.FileName + " --> " + shellFile.ShortcutTargetPath;
                        return shellFile != null ? shellFile.FilePath : target.Id;

                    case ThreatObjectKind.FirewallRule:
                        FirewallRuleThreatObject fwRule = target as FirewallRuleThreatObject;
                        return fwRule != null ? fwRule.RuleName + " → " + fwRule.ApplicationName : target.Id;

                    case ThreatObjectKind.WmiSubscription:
                        WmiSubscriptionThreatObject wmi = target as WmiSubscriptionThreatObject;
                        return wmi != null ? wmi.Name + " -> " + wmi.CommandLineTemplate : target.Id;

                    case ThreatObjectKind.Hosts:
                        HostsThreatObject hosts = target as HostsThreatObject;
                        return hosts != null ? hosts.HostsFilePath : target.Id;

                    case ThreatObjectKind.Service:
                        ServiceThreatObject svc = target as ServiceThreatObject;
                        return svc != null ? svc.ServiceName : target.Id;

                    default:
                        return target.Id;
                }
            }
            catch
            {
                return target.Id;
            }
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }

        #endregion

        #region Применение выбора

        /// <summary>Сбрасывает ручной выбор пользователя во всех решениях.</summary>
        public void ResetUserOverrides()
        {
            foreach (ThreatDecision decision in _decisions)
            {
                if (decision != null)
                    decision.UserOverrideAction = null;
            }
        }

        /// <summary>Помечает все решения как «Пропустить».</summary>
        public void ApplySkipAll()
        {
            foreach (ThreatDecision decision in _decisions)
            {
                if (decision != null)
                    decision.UserOverrideAction = ScanActionTypeUserSelected.Skip;
            }
        }

        /// <summary>Применяет выбор пользователя к флагам действий в каждом ThreatObject.</summary>
        public void ApplyUserOverrides()
        {
            foreach (ThreatDecision decision in _decisions)
            {
                if (decision == null || decision.Target == null || !decision.UserOverrideAction.HasValue)
                    continue;

                ApplyOverrideToTarget(decision);
            }
        }

        private void ApplyOverrideToTarget(ThreatDecision decision)
        {
            ScanActionTypeUserSelected userAction = decision.UserOverrideAction.Value;

            if (userAction == ScanActionTypeUserSelected.Skip)
                return;

            switch (decision.Target.Kind)
            {
                case ThreatObjectKind.File:
                    ApplyOverrideToFile(decision.Target as FileThreatObject, userAction);
                    break;
                case ThreatObjectKind.Directory:
                    ApplyOverrideToDirectory(decision.Target as DirectoryThreatObject, userAction);
                    break;
                case ThreatObjectKind.ShellStartupFile:
                    ApplyOverrideToShell(decision.Target as ShellStartupFileThreatObject, userAction);
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
                    decision.ActionType = MapUserActionToScanAction(userAction);
                    break;
                case ThreatObjectKind.Hosts:
                    decision.ActionType = MapUserActionToScanAction(userAction);
                    break;
                case ThreatObjectKind.UserProfile:
                    decision.ActionType = MapUserActionToScanAction(userAction);
                    break;
                default:
                    break;
            }
        }

        private static ScanActionType MapUserActionToScanAction(ScanActionTypeUserSelected action)
        {
            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    return ScanActionType.Cured;
                case ScanActionTypeUserSelected.Quarantine:
                    return ScanActionType.Quarantine;
                case ScanActionTypeUserSelected.Delete:
                    return ScanActionType.Deleted;
                case ScanActionTypeUserSelected.Terminate:
                    return ScanActionType.Terminated;
                case ScanActionTypeUserSelected.Disable:
                    return ScanActionType.Disabled;
                default:
                    return ScanActionType.Skipped;
            }
        }

        private static void ApplyOverrideToFile(FileThreatObject file, ScanActionTypeUserSelected action)
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

        private static void ApplyOverrideToDirectory(DirectoryThreatObject dir, ScanActionTypeUserSelected action)
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

        private static void ApplyOverrideToShell(ShellStartupFileThreatObject shell, ScanActionTypeUserSelected action)
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

            if (shell.IsShortcut && shell.ShortcutTargetFile != null)
                ApplyOverrideToFile(shell.ShortcutTargetFile, action);
        }

        private static void ApplyOverrideToProcess(ProcessThreatObject proc, ScanActionTypeUserSelected action)
        {
            if (proc == null) return;
            proc.ShouldTerminate = false;
            proc.ShouldSuspend = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Terminate:
                    proc.ShouldTerminate = true;
                    break;

                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Delete:
                case ScanActionTypeUserSelected.Quarantine:
                    proc.ShouldTerminate = true;
                    ApplyOverrideToFile(proc.FileProcess, action);
                    break;
            }
        }

        private static void ApplyOverrideToService(ServiceThreatObject svc, ScanActionTypeUserSelected action)
        {
            if (svc == null) return;

            bool shouldStopServiceWasSet = svc.ShouldStopService;
            bool shouldDisableServiceWasSet = svc.ShouldDisableService;
            bool shouldDeleteServiceWasSet = svc.ShouldDeleteService;
            bool shouldRestoreServiceWasSet = svc.ShouldRestoreService;
            bool shouldRestoreServiceDllWasSet = svc.ShouldRestoreServiceDll;
            bool shouldResetSddlWasSet = svc.ShouldResetSddl;
            bool shouldRemoveFromSafeModeWasSet = svc.ShouldRemoveFromSafeMode;

            svc.ShouldStopService = false;
            svc.ShouldDisableService = false;
            svc.ShouldDeleteService = false;
            svc.ShouldQuarantineService = false;
            svc.ShouldRestoreService = false;
            svc.ShouldRestoreServiceDll = false;
            svc.ShouldResetSddl = false;
            svc.ShouldRemoveFromSafeMode = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    svc.ShouldRestoreService = shouldRestoreServiceWasSet;
                    svc.ShouldRestoreServiceDll = shouldRestoreServiceDllWasSet;
                    svc.ShouldResetSddl = shouldResetSddlWasSet;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    svc.ShouldStopService = true;
                    svc.ShouldDisableService = true;
                    svc.ShouldDeleteService = true;
                    svc.ShouldResetSddl = shouldResetSddlWasSet;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Disable:
                    svc.ShouldDisableService = true;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    svc.ShouldQuarantineService = true;
                    svc.ShouldStopService = true;
                    svc.ShouldDisableService = true;
                    svc.ShouldRemoveFromSafeMode = shouldRemoveFromSafeModeWasSet;
                    break;
            }

            if (svc.LinkedServiceFile != null)
                ApplyOverrideToFile(svc.LinkedServiceFile, action);
            if (svc.LinkedServiceDll != null)
                ApplyOverrideToFile(svc.LinkedServiceDll, action);
        }

        private static void ApplyOverrideToRegistry(RegistryThreatObject reg, ScanActionTypeUserSelected action)
        {
            if (reg == null) return;

            bool actionSetDataWasSet = reg.ActionSetData;
            bool actionSetSiblingWasSet = reg.ActionSetSibling;
            bool actionUnlockFirstWasSet = reg.ActionUnlockFirst;
            bool actionDeleteParentKeyWasSet = reg.ActionDeleteParentKey;
            bool actionDeleteWasSet = reg.ActionDelete;
            bool actionRemoveDefenderExclusionWasSet = reg.ActionRemoveDefenderExclusion;

            reg.ActionDelete = false;
            reg.ActionSetData = false;
            reg.ActionSetSibling = false;
            reg.ActionUnlockFirst = false;
            reg.ActionDeleteParentKey = false;
            reg.ActionQuarantine = false;
            reg.ActionRemoveDefenderExclusion = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                    reg.ActionSetData = actionSetDataWasSet;
                    reg.ActionSetSibling = actionSetSiblingWasSet;
                    reg.ActionRemoveDefenderExclusion = actionRemoveDefenderExclusionWasSet;
                    break;
                case ScanActionTypeUserSelected.Delete:
                    RestoreRegistryDeleteFlags(reg, actionDeleteWasSet, actionDeleteParentKeyWasSet, actionUnlockFirstWasSet);
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    reg.ActionQuarantine = true;
                    RestoreRegistryDeleteFlags(reg, actionDeleteWasSet, actionDeleteParentKeyWasSet, actionUnlockFirstWasSet);
                    break;
            }

            if (reg.LinkedFile != null)
                ApplyOverrideToFile(reg.LinkedFile, action);
        }

        private static void RestoreRegistryDeleteFlags(RegistryThreatObject reg, bool deleteWasSet, bool deleteParentKeyWasSet, bool unlockFirstWasSet)
        {
            reg.ActionDeleteParentKey = deleteParentKeyWasSet;
            reg.ActionUnlockFirst = unlockFirstWasSet;
            reg.ActionDelete = deleteWasSet || !deleteParentKeyWasSet;
        }

        private static void ApplyOverrideToTask(TaskThreatObject task, ScanActionTypeUserSelected action)
        {
            if (task == null) return;

            bool actionDeleteTaskWasSet = task.ActionDeleteTask;
            bool actionDeleteFileWasSet = task.ActionDeleteFile;

            task.ActionDeleteTask = false;
            task.ActionDeleteFile = false;
            task.ActionQuarantineTask = false;

            switch (action)
            {
                case ScanActionTypeUserSelected.Cure:
                case ScanActionTypeUserSelected.Delete:
                    task.ActionDeleteTask = actionDeleteTaskWasSet || actionDeleteFileWasSet;
                    task.ActionDeleteFile = actionDeleteFileWasSet;
                    break;
                case ScanActionTypeUserSelected.Quarantine:
                    task.ActionQuarantineTask = true;
                    task.ActionDeleteTask = true;
                    break;
            }

            if (task.LinkedFile != null)
                ApplyOverrideToFile(task.LinkedFile, action);
            if (task.LinkedFileFromArgs != null)
                ApplyOverrideToFile(task.LinkedFileFromArgs, action);
        }

        private static void ApplyOverrideToFirewall(FirewallRuleThreatObject fw, ScanActionTypeUserSelected action)
        {
            if (fw != null && action != ScanActionTypeUserSelected.Skip)
                fw.ShouldDelete = true;
        }

        #endregion

        #region Bulk-действия

        private void PopulateBulkComboBox()
        {
            List<ScanActionTypeUserSelected> commonActions = ComputeCommonActionsAllRows();

            if (commonActions.Count == 0)
            {
                commonActions.Add(ScanActionTypeUserSelected.Delete);
                commonActions.Add(ScanActionTypeUserSelected.Skip);
            }

            _bulkActions.Clear();
            _bulkActions.AddRange(commonActions);

            comboBulk.Items.Clear();
            foreach (ScanActionTypeUserSelected action in commonActions)
                comboBulk.Items.Add(GetLocalizedUserActionName(action));

            if (comboBulk.Items.Count > 0)
                comboBulk.SelectedIndex = 0;
        }

        private List<ScanActionTypeUserSelected> ComputeCommonActionsAllRows()
        {
            List<ScanActionTypeUserSelected> common = null;

            foreach (ThreatDecision decision in _decisions)
            {
                if (decision == null || decision.Target == null)
                    continue;

                List<ScanActionTypeUserSelected> applicable =
                    new List<ScanActionTypeUserSelected>(GetApplicableActions(decision.Target.Kind, decision.Target));

                if (common == null)
                    common = applicable;
                else
                    IntersectLists(common, applicable);
            }

            return common ?? new List<ScanActionTypeUserSelected>();
        }

        private void bulkApplyBtn_Click(object sender, EventArgs e)
        {
            int index = comboBulk.SelectedIndex;
            if (index < 0 || index >= _bulkActions.Count)
                return;

            ScanActionTypeUserSelected userAction = _bulkActions[index];

            for (int i = 0; i < dataGridReview.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridReview.Rows[i];
                ThreatDecision decision = row.Tag as ThreatDecision;
                if (decision == null || decision.Target == null)
                    continue;

                bool applicable = false;
                foreach (ScanActionTypeUserSelected action in GetApplicableActions(decision.Target.Kind, decision.Target))
                {
                    if (action == userAction) { applicable = true; break; }
                }
                if (!applicable)
                    continue;

                row.Cells[ColNewAction].Value = GetLocalizedUserActionName(userAction);
                decision.UserOverrideAction = userAction;
            }

            UpdateSetRecommendedButtonState();
        }

        private void UpdateBulkComboBoxForSelectedRows()
        {
            List<int> selectedIndices = new List<int>();
            foreach (DataGridViewRow row in dataGridReview.SelectedRows)
            {
                if (row.Index >= 0)
                    selectedIndices.Add(row.Index);
            }

            if (selectedIndices.Count == 0)
            {
                PopulateBulkComboBox();
                return;
            }

            List<ScanActionTypeUserSelected> commonActions = ComputeCommonActions(selectedIndices);

            if (commonActions.Count == 0)
            {
                commonActions.Add(ScanActionTypeUserSelected.Delete);
                commonActions.Add(ScanActionTypeUserSelected.Skip);
            }

            string selectedText = comboBulk.SelectedItem != null ? comboBulk.SelectedItem.ToString() : null;

            _bulkActions.Clear();
            _bulkActions.AddRange(commonActions);

            comboBulk.Items.Clear();
            foreach (ScanActionTypeUserSelected action in commonActions)
                comboBulk.Items.Add(GetLocalizedUserActionName(action));

            if (selectedText != null)
            {
                for (int i = 0; i < comboBulk.Items.Count; i++)
                {
                    if (comboBulk.Items[i].ToString() == selectedText)
                    {
                        comboBulk.SelectedIndex = i;
                        return;
                    }
                }

                comboBulk.SelectedIndex = 0;
            }
        }

        private List<ScanActionTypeUserSelected> ComputeCommonActions(List<int> rowIndices)
        {
            if (rowIndices.Count == 0)
                return new List<ScanActionTypeUserSelected>();

            List<ScanActionTypeUserSelected> common = null;

            foreach (int index in rowIndices)
            {
                DataGridViewRow row = dataGridReview.Rows[index];
                ThreatDecision decision = row.Tag as ThreatDecision;
                if (decision == null || decision.Target == null)
                    continue;

                List<ScanActionTypeUserSelected> applicable =
                    new List<ScanActionTypeUserSelected>(GetApplicableActions(decision.Target.Kind, decision.Target));

                if (common == null)
                    common = applicable;
                else
                    IntersectLists(common, applicable);
            }

            return common ?? new List<ScanActionTypeUserSelected>();
        }

        private static void IntersectLists<T>(List<T> target, IEnumerable<T> other)
        {
            HashSet<T> otherSet = new HashSet<T>(other);
            target.RemoveAll(item => !otherSet.Contains(item));
        }

        #endregion

        #region Рекомендуемые действия

        private void UpdateSetRecommendedButtonState()
        {
            bool hasManualChanges = false;

            foreach (DataGridViewRow row in dataGridReview.Rows)
            {
                ThreatDecision decision = row.Tag as ThreatDecision;
                if (decision == null || decision.Target == null)
                    continue;

                if (GetUserActionFromRow(row) != GetDefaultUserActionFromFlags(decision))
                {
                    hasManualChanges = true;
                    break;
                }
            }

            btnSetRecommended.Enabled = hasManualChanges;
        }

        private void btnSetRecommended_Click(object sender, EventArgs e)
        {
            _suppressActionChanged = true;
            try
            {
                foreach (DataGridViewRow row in dataGridReview.Rows)
                {
                    ThreatDecision decision = row.Tag as ThreatDecision;
                    if (decision == null || decision.Target == null)
                        continue;

                    ScanActionTypeUserSelected recommended = GetDefaultUserActionFromFlags(decision);
                    row.Cells[ColNewAction].Value = GetLocalizedUserActionName(recommended);
                    decision.UserOverrideAction = null;
                }
            }
            finally
            {
                _suppressActionChanged = false;
            }

            dataGridReview.RefreshEdit();
            UpdateSetRecommendedButtonState();
        }

        #endregion
    }
}
