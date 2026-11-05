using MSearch.Core;
using MSearch.Core.ThreatDecisions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Win32Wrapper;

namespace MSearch.UI
{
    /// <summary>
    /// Main window: side navigation and content area.
    /// </summary>
    public partial class FormMain : FormShadow
    {
        private const int WM_NCHITTEST = 0x84;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 2;
        private const int HTCLIENT = 1;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int ResizeBorder = 6;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private readonly ScanPage _scanPage;
        private readonly ResultsPage _resultsPage;
        private readonly SettingsPage _settingsPage;
        private readonly AboutPage _aboutPage;
        private readonly QuarantinePage _quarantinePage;
        private readonly ThreatReviewPage _threatReviewPage;
        private readonly Font _closeButtonFont;
        private readonly Font _closeButtonFontRegular;

        private readonly Dictionary<string, Button> _navButtons = new Dictionary<string, Button>();
        private string _activePageKey;

        private bool _scanning;
        private bool _environmentPrepared;
        private bool _closeAfterScan;
        private CancellationTokenSource _cts;

        private bool _reviewActive;
        private ManualResetEventSlim _reviewGate;

        private readonly object _countersLock = new object();
        private bool _countersScheduled;
        private int _pendingFound;
        private int _pendingNeutralized;
        private int _pendingSuspicious;
        private int _pendingSkipped;

        private readonly object _itemLock = new object();
        private bool _itemScheduled;
        private string _pendingItem;
        private int _pendingDone;
        private int _pendingTotal;

        public FormMain()
        {
            InitializeComponent();

            // Отключённая «X» отличается от активной только начертанием и цветом.
            _closeButtonFont = buttonClose.Font;
            _closeButtonFontRegular = new Font(buttonClose.Font, FontStyle.Regular);

            ApplyAdaptiveWindowSize();

            Text = Utils.GetRndString();

            // Логотип должен быть поверх панели навигации: она занимает всю
            // высоту сайдбара и иначе закрывает его своим фоном.
            buttonDonateSidebar.BringToFront();
            labelLogo.BringToFront();
            buttonDonateSidebar.Text = AppConfig.GetInstance.LL.GetLocalizedString("_DonateBtn");

            _scanPage = new ScanPage();
            _resultsPage = new ResultsPage();
            _settingsPage = new SettingsPage();
            _aboutPage = new AboutPage();
            _quarantinePage = new QuarantinePage();
            _threatReviewPage = new ThreatReviewPage();

            _navButtons["scan"] = buttonNavScan;
            _navButtons["results"] = buttonNavResults;
            _navButtons["quarantine"] = buttonNavQuarantine;
            _navButtons["settings"] = buttonNavSettings;
            _navButtons["about"] = buttonNavAbout;

            WirePageEvents();
            ShowPage(LaunchOptions.GetInstance.QuarantineMode ? "quarantine" : "scan");
            ApplyLanguage();
        }

        /// <summary>
        /// Picks the startup window size for the monitor under the cursor,
        /// taking display scaling into account. The app is DPI-unaware, so the
        /// monitor work area is converted to its logical units.
        /// </summary>
        private void ApplyAdaptiveWindowSize()
        {
            Screen screen = Screen.FromPoint(Cursor.Position);
            if (screen == null)
                screen = Screen.PrimaryScreen;

            Rectangle work = screen.WorkingArea;
            float scale = GetMonitorScale(screen);

            int logicalWidth = Math.Max(1, (int)Math.Round(work.Width / scale));
            int logicalHeight = Math.Max(1, (int)Math.Round(work.Height / scale));

            int width = Math.Max(MinimumSize.Width, Math.Min(1400, logicalWidth - 80));
            int height = Math.Max(MinimumSize.Height, Math.Min(900, logicalHeight - 80));

            ClientSize = new Size(width, height);

            StartPosition = FormStartPosition.Manual;
            int logicalLeft = (int)Math.Round(work.Left / scale);
            int logicalTop = (int)Math.Round(work.Top / scale);
            Location = new Point(
                logicalLeft + Math.Max(0, (logicalWidth - width) / 2),
                logicalTop + Math.Max(0, (logicalHeight - height) / 2));
        }

        private static float GetMonitorScale(Screen screen)
        {
            try
            {
                Native.MONITOR_POINT point = new Native.MONITOR_POINT
                {
                    X = screen.Bounds.Left + screen.Bounds.Width / 2,
                    Y = screen.Bounds.Top + screen.Bounds.Height / 2
                };

                IntPtr monitor = Native.MonitorFromPoint(point, Native.MONITOR_DEFAULTTONEAREST);
                uint dpiX;
                uint dpiY;

                if (monitor != IntPtr.Zero &&
                    Native.GetDpiForMonitor(monitor, Native.MDT_EFFECTIVE_DPI, out dpiX, out dpiY) == 0 &&
                    dpiX > 0)
                {
                    return dpiX / 96f;
                }
            }
            catch (DllNotFoundException) { }
            catch (EntryPointNotFoundException) { }

            return 1f;
        }

        private void buttonNavScan_Click(object sender, EventArgs e)
        {
            ShowPage("scan");
        }

        private void buttonNavResults_Click(object sender, EventArgs e)
        {
            ShowPage("results");
        }

        private void buttonNavQuarantine_Click(object sender, EventArgs e)
        {
            ShowPage("quarantine");
        }

        private void buttonNavSettings_Click(object sender, EventArgs e)
        {
            ShowPage("settings");
        }

        private void buttonNavAbout_Click(object sender, EventArgs e)
        {
            ShowPage("about");
        }

        private void buttonDonateSidebar_Click(object sender, EventArgs e)
        {
            Utils.OpenUrlInBrowser("https://blendlog.github.io/donate.html");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void WindowButton_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;

            // Недоступная «X» не должна подсвечиваться.
            if (control != null && !(_reviewActive && control == buttonClose))
                control.BackColor = Theme.Track;
        }

        private void WindowButton_MouseLeave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
                control.BackColor = Theme.Background;
        }

        private void WirePageEvents()
        {
            _scanPage.ScanRequested += OnScanRequested;
            _scanPage.StopRequested += delegate
            {
                if (_cts != null && !_cts.IsCancellationRequested)
                    _cts.Cancel();
            };
            _resultsPage.OpenQuarantineRequested += delegate { ShowPage("quarantine"); };
            _quarantinePage.RefreshRequested += delegate { RefreshQuarantine(); };
            _settingsPage.LanguageChanged += delegate { ApplyLanguage(); };

            _threatReviewPage.Applied += delegate { CompleteThreatReview(); };
            _threatReviewPage.ContinueRequested += delegate { CompleteThreatReview(); };
            _threatReviewPage.SkipAllRequested += delegate { CompleteThreatReview(); };
        }

        private void ShowPage(string key)
        {
            if (_reviewActive && key != "review")
                return;

            if (_activePageKey == key)
            {
                if (key == "quarantine" && IsHandleCreated)
                    RefreshQuarantine();
                return;
            }

            _activePageKey = key;
            panelContent.SuspendLayout();
            panelContent.Controls.Clear();

            Control page;
            switch (key)
            {
                case "results":
                    page = _resultsPage;
                    break;
                case "review":
                    page = _threatReviewPage;
                    break;
                case "quarantine":
                    page = _quarantinePage;
                    break;
                case "settings":
                    page = _settingsPage;
                    break;
                case "about":
                    page = _aboutPage;
                    break;
                default:
                    page = _scanPage;
                    break;
            }

            // Страницы должны растягиваться вместе с рабочей областью,
            // иначе их содержимое обрезается при уменьшении окна.
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
            panelContent.ResumeLayout();

            if (key == "quarantine" && IsHandleCreated)
                RefreshQuarantine();

            foreach (KeyValuePair<string, Button> pair in _navButtons)
            {
                bool active = pair.Key == key;
                pair.Value.BackColor = active ? Theme.SidebarActive : Theme.Sidebar;
                pair.Value.ForeColor = active ? Color.White : Theme.SidebarText;
                pair.Value.FlatAppearance.MouseOverBackColor = active ? Theme.SidebarActive : Theme.SidebarHover;
            }
        }

        private void SetNavigationEnabled(bool enabled)
        {
            foreach (KeyValuePair<string, Button> pair in _navButtons)
            {
                bool active = pair.Key == _activePageKey;

                // Enabled = false заставляет WinForms рисовать текст своим цветом
                // (ForeColor игнорируется), поэтому недоступность вкладок визуальная:
                // переход всё равно отсекает ShowPage при _reviewActive.
                pair.Value.ForeColor = enabled
                    ? (active ? Color.White : Theme.SidebarText)
                    : Color.FromArgb(64, 64, 64);

                pair.Value.FlatAppearance.MouseOverBackColor = enabled
                    ? (active ? Theme.SidebarActive : Theme.SidebarHover)
                    : Theme.Sidebar;
            }

            buttonDonateSidebar.Enabled = enabled;
        }

        private void SetCloseButtonEnabled(bool enabled)
        {
            // Enabled = false тоже рисует текст своим цветом, поэтому блокируем
            // логически: закрытие отменяет OnFormClosing при _reviewActive.
            buttonClose.Font = enabled ? _closeButtonFont : _closeButtonFontRegular;
            buttonClose.ForeColor = enabled ? Theme.Sidebar : Color.Silver;
            // Сброс hover-подсветки: у недоступной кнопки она не нужна.
            buttonClose.BackColor = Theme.Background;
        }

        private async void RefreshQuarantine()
        {
            await _quarantinePage.RefreshItemsAsync();
        }

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            ReleaseCapture();
            Native.SendMessage(Handle, WM_NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
        }

        private async void OnScanRequested(ScanRequest request)
        {
            if (_scanning || request == null)
                return;

            if (_quarantinePage.IsBusy)
            {
                MessageBoxCustom.Show(
                    AppConfig.GetInstance.LL.GetLocalizedString("_PleaseWaitMessage"),
                    AppConfig.GetInstance._title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            _scanning = true;
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            ShowPage("scan");
            _scanPage.SetScanning(true);
            _quarantinePage.SetScanRunning(true);

            try
            {
                if (!_environmentPrepared)
                {
                    await Task.Run(delegate { ScanFactory.PrepareEnvironment(LaunchOptions.GetInstance); });
                    _environmentPrepared = true;
                }

                ScanWorkflowResult result = await Task.Run(delegate
                {
                    ScanInfrastructure infrastructure = ScanFactory.CreateInfrastructure();
                    ScanWorkflow workflow = new ScanWorkflow(infrastructure, request, new GuiScanInteraction(this));
                    workflow.StageChanged += Workflow_StageChanged;
                    workflow.CountersChanged += Workflow_CountersChanged;
                    workflow.ItemProgress += Workflow_ItemProgress;
                    return workflow.Run(token);
                });

                _resultsPage.ShowResults(result, request.Mode);
                ShowPage("results");
                PromptStatistics(result.Statistics);
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                DialogDispatcher.Show(ex.Message, AppConfig.GetInstance._title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _scanning = false;

                if (_cts != null)
                {
                    _cts.Dispose();
                    _cts = null;
                }

                _quarantinePage.SetScanRunning(false);
                _scanPage.SetScanning(false);

                if (_closeAfterScan)
                    Close();
            }
        }

        private void Workflow_StageChanged(object sender, ScanStageEventArgs e)
        {
            RunOnUi(delegate { _scanPage.UpdateStage(e.Stage, e.ScannerId); });
        }

        private void Workflow_ItemProgress(object sender, ScanProgressEventArgs e)
        {
            // Не таймер-троттлинг: не даём очереди BeginInvoke расти,
            // пока предыдущее обновление ещё не обработано.
            lock (_itemLock)
            {
                _pendingItem = e.CurrentItem;
                _pendingDone = e.Done;
                _pendingTotal = e.Total;

                if (_itemScheduled)
                    return;

                _itemScheduled = true;
            }

            RunOnUi(delegate
            {
                string item;
                int done;
                int total;

                lock (_itemLock)
                {
                    _itemScheduled = false;
                    item = _pendingItem;
                    done = _pendingDone;
                    total = _pendingTotal;
                }

                _scanPage.UpdateProgress(item, done, total);
            });
        }

        private void Workflow_CountersChanged(object sender, ScanCountersEventArgs e)
        {
            lock (_countersLock)
            {
                _pendingFound = e.Found;
                _pendingNeutralized = e.Neutralized;
                _pendingSuspicious = e.Suspicious;
                _pendingSkipped = e.Skipped;

                if (_countersScheduled)
                    return;

                _countersScheduled = true;
            }

            RunOnUi(delegate
            {
                lock (_countersLock)
                {
                    _countersScheduled = false;
                }

                _scanPage.UpdateCounters(_pendingFound, _pendingNeutralized, _pendingSuspicious, _pendingSkipped);
            });
        }

        private void RunOnUi(Action action)
        {
            if (IsDisposed || Disposing)
                return;

            try
            {
                if (InvokeRequired)
                    BeginInvoke(action);
                else
                    action();
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        /// <summary>Called from the scan engine (background thread).</summary>
        public bool ConfirmAutoAcceptUnknown()
        {
            DialogResult result = DialogDispatcher.Show(
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAutoAccept_Question"),
                AppConfig.GetInstance.LL.GetLocalizedString("_ReviewAutoAccept_Title"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        /// <summary>Shows the intermediate threat review page (UI thread).</summary>
        public void BeginThreatReview(List<ThreatDecision> decisions, ManualResetEventSlim gate)
        {
            _reviewGate = gate;
            _reviewActive = true;

            _threatReviewPage.InitializeReview(decisions);

            // Сначала страница, потом недоступность: ShowPage перекрашивает вкладки.
            ShowPage("review");
            SetNavigationEnabled(false);
            SetCloseButtonEnabled(false);
            buttonDonateSidebar.Visible = false;
        }

        private void CompleteThreatReview()
        {
            if (!_reviewActive)
                return;

            _reviewActive = false;
            SetNavigationEnabled(true);
            SetCloseButtonEnabled(true);
            buttonDonateSidebar.Visible = true;
            ShowPage("scan");

            ManualResetEventSlim gate = _reviewGate;
            _reviewGate = null;
            if (gate != null)
                gate.Set();
        }

        /// <summary>Called from the scan engine (background thread).</summary>
        public void WaitBeforeCleanup()
        {
            MessageBoxCustom.Show(
                AppConfig.GetInstance.LL.GetLocalizedString("_PleaseWaitMessage"),
                AppConfig.GetInstance._title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void PromptStatistics(ScanStatistics statistics)
        {
            if (statistics == null || LaunchOptions.GetInstance.no_logs)
                return;

            const int ask = 2;
            int value = ReadDword(Registry.CurrentUser, AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.StatisticsValueName);

            if (value == -1)
            {
                WriteDword(Registry.CurrentUser, AppConfig.GetInstance.RegistryPathMain, AppConfig.GetInstance.StatisticsValueName, ask);
                value = ask;
            }

            if (value == 1)
            {
                SendStatistics(statistics);
                return;
            }

            if (value != ask)
                return;

            DialogResult result = MessageBoxCustom.Show(
                AppConfig.GetInstance.LL.GetLocalizedString("_AllowSentStatistics"),
                AppConfig.GetInstance._title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            WriteDword(Registry.CurrentUser, AppConfig.GetInstance.RegistryPathMain,
                AppConfig.GetInstance.StatisticsValueName, result == DialogResult.Yes ? 1 : 0);

            if (result == DialogResult.Yes)
                SendStatistics(statistics);
        }

        private async void SendStatistics(ScanStatistics statistics)
        {
            // Фиксируем путь до фоновой выгрузки: следующий скан откроет новый лог.
            string logPath = Logger.LogFilePath;

            try
            {
                await Task.Run(async delegate
                {
                    await MinerSearch.SentLogAsync(statistics.Found, statistics.Neutralized, statistics.Skipped, logPath);
                });
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }

        private static int ReadDword(RegistryKey hive, string path, string valueName)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path))
                {
                    if (key == null)
                        return -1;

                    object value = key.GetValue(valueName);
                    return value == null ? -1 : (int)value;
                }
            }
            catch
            {
                return -1;
            }
        }

        private static void WriteDword(RegistryKey hive, string path, string valueName, int value)
        {
            try
            {
                using (RegistryKey key = hive.OpenSubKey(path, true) ?? hive.CreateSubKey(path))
                {
                    if (key != null)
                        key.SetValue(valueName, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }

        private void ApplyLanguage()
        {
            buttonDonateSidebar.Text = AppConfig.GetInstance.LL.GetLocalizedString("_DonateBtn");

            foreach (KeyValuePair<string, Button> pair in _navButtons)
            {
                string key = pair.Value.Tag as string;
                if (!string.IsNullOrEmpty(key))
                    pair.Value.Text = AppConfig.GetInstance.LL.GetLocalizedString(key);
            }

            _scanPage.ApplyLanguage();
            _resultsPage.ApplyLanguage();
            _settingsPage.ApplyLanguage();
            _aboutPage.ApplyLanguage();
            _quarantinePage.ApplyLanguage();
            _threatReviewPage.ApplyLanguage();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            DialogDispatcher.SetUiContext(SynchronizationContext.Current);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_activePageKey == "quarantine")
                RefreshQuarantine();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_quarantinePage.IsBusy)
            {
                MessageBoxCustom.Show(
                    AppConfig.GetInstance.LL.GetLocalizedString("_PleaseWaitMessage"),
                    AppConfig.GetInstance._title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }

            // Закрытие во время обзора запрещено: выйти можно только кнопками
            // страницы (Применить / Продолжить с рекомендованными / Пропустить все).
            if (_reviewActive)
            {
                e.Cancel = true;
                return;
            }

            if (_scanning && !_closeAfterScan)
            {
                DialogResult result = MessageBoxCustom.Show(
                    AppConfig.GetInstance.LL.GetLocalizedString("_ExitDuringScan"),
                    AppConfig.GetInstance._title,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                _closeAfterScan = true;
                if (_cts != null && !_cts.IsCancellationRequested)
                    _cts.Cancel();

                e.Cancel = true;
                return;
            }

            Logger.DisposeLogger();
            DialogDispatcher.SetUiContext(null);
            base.OnFormClosing(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST && WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);

                if ((int)m.Result == HTCLIENT)
                {
                    int screenX = (short)(m.LParam.ToInt32() & 0xFFFF);
                    int screenY = (short)((m.LParam.ToInt32() >> 16) & 0xFFFF);
                    Point point = PointToClient(new Point(screenX, screenY));

                    bool left = point.X <= ResizeBorder;
                    bool right = point.X >= ClientSize.Width - ResizeBorder;
                    bool top = point.Y <= ResizeBorder;
                    bool bottom = point.Y >= ClientSize.Height - ResizeBorder;

                    if (left && top) m.Result = (IntPtr)HTTOPLEFT;
                    else if (right && top) m.Result = (IntPtr)HTTOPRIGHT;
                    else if (left && bottom) m.Result = (IntPtr)HTBOTTOMLEFT;
                    else if (right && bottom) m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else if (left) m.Result = (IntPtr)HTLEFT;
                    else if (right) m.Result = (IntPtr)HTRIGHT;
                    else if (top) m.Result = (IntPtr)HTTOP;
                    else if (bottom) m.Result = (IntPtr)HTBOTTOM;
                }

                return;
            }

            base.WndProc(ref m);
        }
    }
}
