using MSearch.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Scan launch page: three modes and the stage panel.
    /// </summary>
    public partial class ScanPage : UserControl
    {
        public event Action<ScanRequest> ScanRequested;
        public event EventHandler StopRequested;

        private readonly Dictionary<string, ToggleSwitch> _stageToggles = new Dictionary<string, ToggleSwitch>();
        private readonly Dictionary<string, Label> _stageLabels = new Dictionary<string, Label>();

        private bool _scanning;
        private bool _canStartSelective;

        public ScanPage()
        {
            InitializeComponent();
            InitializeStageToggles();

            if (!IsInDesignMode)
                ApplyLanguage();
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void cardQuick_Click(object sender, EventArgs e)
        {
            OnModeCardClicked(ScanMode.Quick);
        }

        private void cardFull_Click(object sender, EventArgs e)
        {
            OnModeCardClicked(ScanMode.Full);
        }

        private void cardSelective_Click(object sender, EventArgs e)
        {
            OnModeCardClicked(ScanMode.Selective);
        }

        private void checkFolder_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptionDependencies();
        }

        private void checkScanAllDrives_CheckedChanged(object sender, EventArgs e)
        {
            if (checkScanAllDrives.Checked && checkFolder.Checked)
            {
                checkScanAllDrives.Checked = false;
                return;
            }

            UpdateOptionDependencies();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            BrowseFolder();
        }

        private void buttonStartSelective_Click(object sender, EventArgs e)
        {
            if (!_canStartSelective)
                return;

            StartSelectiveScan();
        }

        private void buttonClearSelection_Click(object sender, EventArgs e)
        {
            if (_scanning)
                return;

            foreach (KeyValuePair<string, ToggleSwitch> pair in _stageToggles)
            {
                if (checkScanAllDrives.Checked && pair.Key == "Signatures")
                    continue;

                pair.Value.Checked = false;
            }

            UpdateOptionDependencies();
        }

        private void StageToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkScanAllDrives.Checked && !GetToggle("Signatures"))
            {
                toggleSignatures.Checked = true;
                return;
            }

            UpdateOptionDependencies();
        }

        private void checkScanOnly_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOptionDependencies();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            OnStopClicked();
        }

        private void contentFlow_SizeChanged(object sender, EventArgs e)
        {
            LayoutContent();
        }

        private void InitializeStageToggles()
        {
            _stageToggles["Processes"] = toggleProcesses;
            _stageToggles["Registry"] = toggleRegistry;
            _stageToggles["Services"] = toggleServices;
            _stageToggles["Tasks"] = toggleTasks;
            _stageToggles["Firewall"] = toggleFirewall;
            _stageToggles["Hosts"] = toggleHosts;
            _stageToggles["UserProfiles"] = toggleUserProfiles;
            _stageToggles["Wmi"] = toggleWmi;
            _stageToggles["Rootkits"] = toggleRootkits;
            _stageToggles["Signatures"] = toggleSignatures;
            _stageToggles["FileSystem"] = toggleFileSystem;
            _stageToggles["ShellStartup"] = toggleShellStartup;

            _stageLabels["_Stage_Processes"] = labelStageProcesses;
            _stageLabels["_Stage_Registry"] = labelStageRegistry;
            _stageLabels["_Stage_Services"] = labelStageServices;
            _stageLabels["_Stage_Tasks"] = labelStageTasks;
            _stageLabels["_Stage_Firewall"] = labelStageFirewall;
            _stageLabels["_Stage_Hosts"] = labelStageHosts;
            _stageLabels["_Stage_UserProfiles"] = labelStageUserProfiles;
            _stageLabels["_Stage_Wmi"] = labelStageWmi;
            _stageLabels["_Stage_Rootkits"] = labelStageRootkits;
            _stageLabels["_Stage_Signatures"] = labelStageSignatures;
            _stageLabels["_Stage_FileSystem"] = labelStageFileSystem;
            _stageLabels["_Stage_ShellStartup"] = labelStageShellStartup;

            // По умолчанию все этапы включены
            foreach (KeyValuePair<string, ToggleSwitch> pair in _stageToggles)
                pair.Value.Checked = true;

            // Подписка после инициализации: иначе каждый Checked = true дёргал бы зависимости.
            foreach (KeyValuePair<string, ToggleSwitch> pair in _stageToggles)
                pair.Value.CheckedChanged += StageToggle_CheckedChanged;

            UpdateOptionDependencies();
        }

        private void CardPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null)
                return;

            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
            }
        }

        private void LayoutContent()
        {
            // ClientSize уже учитывает вертикальный скроллбар, поэтому берём
            // полную ширину: правые края карточек и панелей статуса совпадают.
            int width = contentFlow.ClientSize.Width;
            if (width <= 0)
                return;

            cardsFlow.Width = width;

            // Между карточками два отступа по 14 px: у последней карточки
            // правого отступа нет, остаток ширины отдаём ей.
            int cardWidth = Math.Max(200, (width - 28) / 3);
            cardQuick.Width = cardWidth;
            cardFull.Width = cardWidth;
            cardSelective.Width = cardWidth + Math.Max(0, width - 28 - cardWidth * 3);
            labelQuickDesc.Width = cardWidth - 36;
            labelFullDesc.Width = cardWidth - 36;
            labelSelectiveDesc.Width = cardWidth - 36;

            selectivePanel.Width = width;
            LayoutFolderRow();
        }

        /// <summary>
        /// Places the path field right after the checkbox: its caption changes
        /// with the language, so a fixed coordinate causes overlap.
        /// </summary>
        private void LayoutFolderRow()
        {
            int left = checkFolder.Right + 12;
            int right = buttonBrowse.Left - 10;
            if (right <= left)
                return;

            textFolder.Left = left;
            textFolder.Width = right - left;
        }

        private void OnModeCardClicked(ScanMode mode)
        {
            if (_scanning)
                return;

            if (mode == ScanMode.Selective)
            {
                selectivePanel.Visible = !selectivePanel.Visible;
                labelHint.Visible = !selectivePanel.Visible;
                contentFlow.AutoScrollPosition = new Point(0, 0);
                LayoutFolderRow();
                return;
            }

            RaiseScanRequested(mode == ScanMode.Full ? ScanRequest.CreateFull() : ScanRequest.CreateQuick());
        }

        private void StartSelectiveScan()
        {
            if (_scanning)
                return;

            ScanRequest request = ScanRequest.CreateSelective();
            request.ScanProcesses = GetToggle("Processes");
            request.ScanRegistry = GetToggle("Registry");
            request.ScanServices = GetToggle("Services");
            request.ScanTasks = GetToggle("Tasks");
            request.ScanFirewall = GetToggle("Firewall");
            request.ScanHosts = GetToggle("Hosts");
            request.ScanUserProfiles = GetToggle("UserProfiles");
            request.ScanWmi = GetToggle("Wmi");
            request.ScanRootkits = GetToggle("Rootkits");
            request.ScanSignatures = GetToggle("Signatures");
            request.ScanFileSystem = GetToggle("FileSystem");
            request.ScanShellStartup = GetToggle("ShellStartup");
            request.FullScan = checkScanAllDrives.Checked;

            request.MaxDepth = (int)numericDepth.Value;
            request.ScanOnly = checkScanOnly.Checked;
            request.RemoveEmptyTasks = checkRemoveEmptyTasks.Checked;

            if (checkFolder.Checked && !string.IsNullOrEmpty(textFolder.Text))
                request.SelectedPath = textFolder.Text;

            RaiseScanRequested(request);
        }

        private void RaiseScanRequested(ScanRequest request)
        {
            Action<ScanRequest> handler = ScanRequested;
            if (handler != null)
                handler(request);
        }

        private void OnStopClicked()
        {
            buttonStop.Enabled = false;
            buttonStop.Text = L("_ScanStopping");

            EventHandler handler = StopRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private bool GetToggle(string key)
        {
            ToggleSwitch toggle;
            return _stageToggles.TryGetValue(key, out toggle) && toggle.Checked;
        }

        /// <summary>
        /// Keeps dependent options consistent: ScanOnly disables rootkits and empty-task removal;
        /// empty-task removal is available only when task scanning is enabled; all-drives is
        /// mutually exclusive with a folder and forces signature scanning on.
        /// </summary>
        private void UpdateOptionDependencies()
        {
            bool scanOnly = checkScanOnly.Checked;
            bool tasks = GetToggle("Tasks");
            bool scanAllDrives = checkScanAllDrives.Checked;

            if (scanAllDrives && !GetToggle("Signatures"))
                toggleSignatures.Checked = true;

            if (scanAllDrives)
                checkFolder.Checked = false;

            if (scanOnly && toggleRootkits.Checked)
                toggleRootkits.Checked = false;

            toggleRootkits.Enabled = !_scanning && !scanOnly;
            if (scanOnly || !tasks)
                checkRemoveEmptyTasks.Checked = false;

            checkRemoveEmptyTasks.Enabled = !_scanning && tasks && !scanOnly;

            checkFolder.Enabled = !_scanning && !scanAllDrives;
            checkScanAllDrives.Enabled = !_scanning && !checkFolder.Checked;
            toggleSignatures.Enabled = !scanAllDrives;

            UpdateFolderControls();
            UpdateStartSelectiveState();
        }

        private void UpdateStartSelectiveState()
        {
            bool anyStage = false;
            foreach (ToggleSwitch toggle in _stageToggles.Values)
            {
                if (!toggle.Checked)
                    continue;

                anyStage = true;
                break;
            }

            bool enabled = anyStage && !_scanning;
            _canStartSelective = enabled;

            // Enabled = false рисует текст собственным цветом WinForms (ForeColor
            // игнорируется), поэтому блокируем логически и задаём цвета сами.
            buttonStartSelective.BackColor = enabled ? Theme.Accent : Color.Gray;
            buttonStartSelective.ForeColor = enabled ? Color.White : Color.DarkGray;
            buttonStartSelective.FlatAppearance.MouseOverBackColor = enabled ? Color.Empty : Color.Gray;
        }

        private void UpdateFolderControls()
        {
            bool enabled = checkFolder.Checked && !checkScanAllDrives.Checked;
            textFolder.Enabled = enabled;
            buttonBrowse.Enabled = enabled;
            if (!enabled)
                textFolder.Text = string.Empty;
        }

        private void BrowseFolder()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = L("_SelectFolderDialog");
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                    textFolder.Text = dialog.SelectedPath;
            }
        }

        public void SetScanning(bool scanning)
        {
            _scanning = scanning;

            cardQuick.Enabled = !scanning;
            cardFull.Enabled = !scanning;
            cardSelective.Enabled = !scanning;
            buttonClearSelection.Enabled = !scanning;
            buttonStop.Enabled = scanning;

            numericDepth.Enabled = !scanning;
            checkScanOnly.Enabled = !scanning;
            UpdateOptionDependencies();

            SetIndeterminate(false);

            if (scanning)
            {
                selectivePanel.Visible = false;
                labelHint.Visible = false;
                labelStage.Text = L("_ScanInProgress");
                labelDetail.Text = string.Empty;
                progressScan.Value = 0;
                buttonStop.Text = L("_ScanStop");
            }
            else
            {
                labelDetail.Text = string.Empty;
                buttonStop.Text = L("_ScanStop");
                buttonStop.Enabled = false;
                labelHint.Visible = !selectivePanel.Visible;
            }
        }

        public void SetStopping()
        {
            buttonStop.Enabled = false;
            buttonStop.Text = L("_ScanStopping");
            labelStage.Text = L("_ScanStopping");
        }

        /// <summary>
        /// Switches the indicator to marquee mode: the cleanup stage can
        /// take minutes to stop services and retry deletions.
        /// </summary>
        public void SetIndeterminate(bool indeterminate)
        {
            ProgressBarStyle style = indeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
            if (progressScan.Style == style)
                return;

            progressScan.Style = style;
            progressScan.MarqueeAnimationSpeed = indeterminate ? 30 : 0;
        }

        public void UpdateStage(ScanStage stage, string scannerId = null)
        {
            if (stage == ScanStage.General && !string.IsNullOrEmpty(scannerId))
                labelStage.Text = GetScannerText(scannerId);
            else
                labelStage.Text = GetStageText(stage);

            if (stage == ScanStage.Completed)
            {
                SetIndeterminate(false);
                progressScan.Value = 100;
            }
            else if (stage == ScanStage.Idle)
            {
                SetIndeterminate(false);
                progressScan.Value = 0;
            }
            else if (stage == ScanStage.Cleanup)
            {
                SetIndeterminate(true);
            }
            else
            {
                SetIndeterminate(false);
                progressScan.Value = GetStagePercent(stage);
            }
        }

        public void UpdateProgress(string item, int done, int total)
        {
            if (string.IsNullOrEmpty(item))
            {
                labelDetail.Text = string.Empty;
                return;
            }

            string display = NormalizeItem(item);
            labelDetail.Text = L("_ScanStage_CurrentObject").Replace("#VALUE#", display);

            if (total > 0 && progressScan.Style != ProgressBarStyle.Marquee)
            {
                int percent = (int)(done * 100L / total);
                if (percent >= 0 && percent <= 100)
                    progressScan.Value = percent;
            }
        }

        /// <summary>
        /// Strips service path prefixes for readable display.
        /// </summary>
        private static string NormalizeItem(string item)
        {
            if (string.IsNullOrEmpty(item))
                return item;

            if (item.StartsWith(@"\\?\UNC\", StringComparison.OrdinalIgnoreCase))
                return @"\\" + item.Substring(8);

            if (item.StartsWith(@"\\?\", StringComparison.Ordinal) ||
                item.StartsWith(@"\??\", StringComparison.Ordinal) ||
                item.StartsWith(@"\\.\", StringComparison.Ordinal))
            {
                return item.Substring(4);
            }

            return item;
        }

        public void UpdateCounters(int found, int neutralized, int suspicious, int skipped)
        {
            labelFoundValue.Text = found.ToString();
            labelNeutralizedValue.Text = neutralized.ToString();
            labelSuspiciousValue.Text = suspicious.ToString();
            labelSkippedValue.Text = skipped.ToString();
        }

        private static int GetStagePercent(ScanStage stage)
        {
            switch (stage)
            {
                case ScanStage.Rootkit: return 10;
                case ScanStage.Processes: return 25;
                case ScanStage.General: return 45;
                case ScanStage.Signatures: return 70;
                case ScanStage.Review: return 82;
                case ScanStage.Cleanup: return 90;
                case ScanStage.Completed: return 100;
                default: return 0;
            }
        }

        private string GetStageText(ScanStage stage)
        {
            switch (stage)
            {
                case ScanStage.Rootkit: return L("_ScanStage_Rootkit");
                case ScanStage.Processes: return L("_ScanStage_Processes");
                case ScanStage.General: return L("_ScanStage_General");
                case ScanStage.Signatures: return L("_ScanStage_Signatures");
                case ScanStage.Review: return L("_ScanStage_Review");
                case ScanStage.Cleanup: return L("_ScanStage_Cleanup");
                case ScanStage.Completed: return L("_ScanStage_Completed");
                default: return L("_ScanReady");
            }
        }

        private string GetScannerText(string scannerId)
        {
            switch (scannerId)
            {
                case "FileSystemThreatScanner": return L("_ScanStage_FileSystem");
                case "ShellStartupScanner": return L("_ScanStage_ShellStartup");
                case "RegistryScanner": return L("_ScanStage_Registry");
                case "ServiceScanner": return L("_ScanStage_Services");
                case "TaskScanner": return L("_ScanStage_Tasks");
                case "FirewallScanner": return L("_ScanStage_Firewall");
                case "WmiScanner": return L("_ScanStage_Wmi");
                case "HostsThreatScanner": return L("_ScanStage_Hosts");
                case "UserProfileScanner": return L("_ScanStage_UserProfiles");
                default: return L("_ScanStage_General");
            }
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_Nav_Scan");

            labelQuickTitle.Text = L("_ScanMode_Quick");
            labelQuickDesc.Text = L("_ScanMode_Quick_Desc");
            labelFullTitle.Text = L("_ScanMode_Full");
            labelFullDesc.Text = L("_ScanMode_Full_Desc");
            labelSelectiveTitle.Text = L("_ScanMode_Selective");
            labelSelectiveDesc.Text = L("_ScanMode_Selective_Desc");

            labelSelectiveHeader.Text = L("_ScanSelective_Title");
            labelHint.Text = L("_ScanHint");
            buttonStartSelective.Text = L("_ScanStart");
            buttonClearSelection.Text = L("_Selective_ClearAll");
            checkFolder.Text = L("_Selective_Folder");
            buttonBrowse.Text = L("_Selective_Browse");
            labelDepth.Text = L("_Selective_Depth");
            checkScanOnly.Text = L("_Selective_ScanOnly");
            checkRemoveEmptyTasks.Text = L("_Selective_RemoveEmptyTasks");
            checkScanAllDrives.Text = L("_Selective_ScanAllDrives");

            foreach (KeyValuePair<string, Label> pair in _stageLabels)
                pair.Value.Text = L(pair.Key);

            labelFoundCaption.Text = L("_Counter_Found");
            labelNeutralizedCaption.Text = L("_Counter_Neutralized");
            labelSuspiciousCaption.Text = L("_Counter_Suspicious");
            labelSkippedCaption.Text = L("_Counter_Skipped");

            buttonStop.Text = L("_ScanStop");
            if (!_scanning)
                labelStage.Text = L("_ScanReady");

            LayoutFolderRow();
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
