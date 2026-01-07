using DBase;
using Microsoft.Win32;
using MSearch.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSearch
{
    public partial class FinishEx : FormShadow
    {
        int threatsCount = 0;
        int curedCount = 0;
        string registryPath = @"Software\M1nerSearch";
        string valueName = "allowstatistics";
        string LBL_id_text = "";

        public FinishEx(int _totalThreats, int _neutralizedThreats, int _suspObj, string _elapsedTime)
        {
            InitializeComponent();
            ProcessManager.SetSmallWindowIconRandomHash(Handle);
            Text = Utils.GetRndString();

            ConfigureDataGridView();


            threatsCount = _totalThreats;
            curedCount = _neutralizedThreats;
            LBL_threatsCount.Text = _totalThreats.ToString();
            LBL_curedCount.Text = _neutralizedThreats.ToString();
            LBL_susCount.Text = _suspObj.ToString();
            LBL_ScanTime.Text = $"{AppConfig.Instance.LL.GetLocalizedString("_Elapse")} {_elapsedTime}";
            LBL_id_text = DeviceIdProvider.GetDeviceId();
            LBL_ID.Text = LBL_id_text;
        }

        private void TranslateForm()
        {
            if (AppConfig.Instance.ScanOnly || AppConfig.Instance.totalFoundThreats == 0)
            {
                LBL_neutralizedThreats.Visible = false;
                LBL_curedCount.Visible = false;
            }

            if (AppConfig.Instance.no_logs)
            {
                btnDetails.Enabled = false;
                btnDetails.BackColor = Color.FromArgb(10, 255, 255, 255);
                btnDetails.FlatAppearance.BorderSize = 0;
                btnDetails.Text = AppConfig.Instance.LL.GetLocalizedString("_NoLogBtn");
            }
            else
            {
                btnDetails.Text = AppConfig.Instance.LL.GetLocalizedString("_BtnDetails");
            }

            LBL_ScanComplete.Text = AppConfig.Instance.LL.GetLocalizedString("_End") + $" №{AppConfig.Instance.RunCount}";
            LBL_totalThreats.Text = AppConfig.Instance.LL.GetLocalizedString("_TotalThreatsFound");
            LBL_neutralizedThreats.Text = AppConfig.Instance.LL.GetLocalizedString("_TotalNeutralizedThreats");
            LBL_susObjects.Text = AppConfig.Instance.LL.GetLocalizedString("_SuspiciousObjects");
            Label_allowStatistics.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelAllowStatistics");
            Label_showAllLogs.Text = AppConfig.Instance.LL.GetLocalizedString("_ShowFolderLogs");
            top.TextAlign = ContentAlignment.MiddleRight;
            top.Text = AppConfig.Instance.LL.GetLocalizedString("_PleaseWaitMessage");
            OpenQuarantineBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_OpenQuarantine");
            DonateBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_DonateBtn");
            finishBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_PleaseWaitMessage");
            finishBtn.BackColor = Color.DimGray;

            Label_OpenLogsFolder.Text = Logger.LogsFolder;

            if (!AppConfig.Instance.ScanOnly)
            {
                if (threatsCount > 0)
                {
                    if (curedCount < threatsCount)
                    {
                        FinalStatus_label.Text = AppConfig.Instance.LL.GetLocalizedString("_FinishNotAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.LightSalmon;
                    }
                    else if (curedCount == threatsCount)
                    {
                        FinalStatus_label.Text = AppConfig.Instance.LL.GetLocalizedString("_FinishAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.LightGreen;

                    }
                }
                else
                {
                    FinalStatus_label.Text = AppConfig.Instance.LL.GetLocalizedString("_NoThreats");
                }
            }
            else
            {
                FinalStatus_label.Text = AppConfig.Instance.LL.GetLocalizedString("_ScanOnlyMode");
            }
        }

        void Finish()
        {
            if (curedCount < threatsCount)
            {
                var result = MessageBoxCustom.Show(AppConfig.Instance.LL.GetLocalizedString("_RebootPCNowDialog"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "shutdown",
                        Arguments = "/r /t 0",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    });
                }
                else Environment.Exit(0);
            }
            else if (curedCount == threatsCount && threatsCount > 0)
            {
#if !DEBUG
                if (!AppConfig.Instance.ScanOnly && !AppConfig.Instance.console_mode)
                {
                    Hide();
                    SplashForm splashForm = new SplashForm();
                    splashForm.ShowDialog();
                }
#endif
            }
            Environment.Exit(0);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
        private void FinishBtn_click(object sender, EventArgs e)
        {
            Finish();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            string logpath = Path.Combine(Logger.LogsFolder, Logger.logFileName);
            if (!File.Exists(logpath))
            {
                return;
            }

            string argument = "/c \"" + logpath + "\"";
            if (AppConfig.Instance.RunAsSystem && !AppConfig.Instance.WinPEMode)
            {
                string pname = new StringBuilder("ex").Append("pl").Append("or").Append("er").ToString();
            restart:
                if (Process.GetProcessesByName(pname).Length > 0)
                {
                    Native.RunAsUser(pname, argument);
                }
                else
                {
                    Process.Start(pname);
                    goto restart;
                }
            }
            else
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(Environment.GetEnvironmentVariable("WINDIR"), "System32", "cmd.exe"),
                    Arguments = argument,
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }
        }


        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                return;
            }

            base.WndProc(ref m);
        }

        private void ConfigureDataGridView()
        {
            dataGridThreats.RowHeadersVisible = false;
            dataGridThreats.AutoGenerateColumns = false;

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TypeColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_ObjectType"),
                DataPropertyName = "Type",
                MinimumWidth = 366
            });

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PathColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Path"),
                DataPropertyName = "Path",
                MinimumWidth = 366
            });

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ActionColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Action"),
                DataPropertyName = "Action",
                MinimumWidth = 250

            });

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NoteColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Note"),
                DataPropertyName = "Note",
                MinimumWidth = 116
            });

            if (AppConfig.Instance.totalFoundThreats == 0 &&
                AppConfig.Instance.totalFoundSuspiciousObjects == 0 &&
                !AppConfig.Instance.hasLockedObjectsByAV &&
                !AppConfig.Instance.hasEmptyTasks)
            {
                dataGridThreats.Columns.Clear();
            }
        }

        private void AdjustNoteColumnWidth()
        {
            var noteColumn = dataGridThreats.Columns["NoteColumn"];
            if (noteColumn == null)
                return;

            noteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            bool hasNotes = false;
            foreach (DataGridViewRow row in dataGridThreats.Rows)
            {
                if (row.IsNewRow) continue;

                var value = row.Cells["NoteColumn"]?.Value;
                if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                {
                    hasNotes = true;
                    break;
                }
            }

            noteColumn.Width = hasNotes ? 380 : 116;
        }


        public void LoadResults(List<ScanResult> results)
        {
            dataGridThreats.DataSource = results;
            AdjustNoteColumnWidth();
        }

        private void FinishEx_Load(object sender, EventArgs e)
        {
            dataGridThreats.ClearSelection();
            TranslateForm();
            CollectStatistics(registryPath, valueName);
            UpdateToggle(registryPath, valueName);
        }

        async void CollectStatistics(string registryPath, string valueName)
        {
            if (AppConfig.Instance.no_logs)
            {
                UpdateUIToDefaultState();
                return;
            }


            using (var key = Registry.CurrentUser.OpenSubKey(registryPath, true) ?? Registry.CurrentUser.CreateSubKey(registryPath))
            {
                if (key == null) return;

                object regValue = key.GetValue(valueName);
                if (regValue == null)
                {
                    regValue = OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) >= 0 ? 0 : 2;
                    key.SetValue(valueName, regValue, RegistryValueKind.DWord);

                    if ((int)regValue == 0)
                    {
                        DisableStatisticsUI();
                    }
                }

                int allowStatistics = (int)regValue;

                switch (allowStatistics)
                {
                    case 2:
                        if (MessageBoxCustom.Show(AppConfig.Instance.LL.GetLocalizedString("_AllowSentStatistics"), AppConfig.Instance._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            key.SetValue(valueName, 1, RegistryValueKind.DWord);
                            await SendLogAndHandleErrors();
                        }
                        else
                        {
                            key.SetValue(valueName, 0, RegistryValueKind.DWord);
                            UpdateUIToDefaultState();
                        }
                        break;

                    case 1:
                        await SendLogAndHandleErrors();
                        break;

                    default:
                        UpdateUIToDefaultState();
                        break;
                }
            }
        }

        void UpdateUIToDefaultState()
        {
            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            top.Text = AppConfig.Instance._title;
            CloseBtn.Visible = true;
            MinimizeBtn.Visible = true;
            DonateBtn.Visible = true;
            finishBtn.Enabled = true;
            finishBtn.BackColor = Color.RoyalBlue;
            finishBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_DataGrid_FinishBtn");

            if (OSExtensions.GetWindowsVersion().IndexOf("Windows 7", StringComparison.OrdinalIgnoreCase) == -1)
            {
                ts_AllowCollectStatistics.Enabled = true;
                ts_AllowCollectStatistics.OffBackColor = Color.Gray;
                ts_AllowCollectStatistics.OffToggleColor = Color.White;
                ts_AllowCollectStatistics.OnBackColor = Color.RoyalBlue;
                ts_AllowCollectStatistics.OnToggleColor = Color.White;
            }
        }

        void DisableStatisticsUI()
        {
            ts_AllowCollectStatistics.Enabled = false;
            ts_AllowCollectStatistics.OffBackColor = Color.Gainsboro;
            ts_AllowCollectStatistics.OffToggleColor = Color.Silver;
            ts_AllowCollectStatistics.OnBackColor = Color.Gainsboro;
            ts_AllowCollectStatistics.OnToggleColor = Color.Silver;
        }

        async Task SendLogAndHandleErrors()
        {
            try
            {
                await Task.Run(() =>
                {
                    Task.Delay(new Random().Next(10, 3000)).Wait();
                    MinerSearch.SentLog();
                });
            }
            catch (FileNotFoundException fnf)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", fnf);
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex);
            }
            finally
            {
                UpdateUIToDefaultState();
            }
        }

        private void dataGridThreats_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            foreach (DataGridViewRow row in dataGridThreats.Rows)
            {
                row.Cells[0].Style.Font = new Font("Verdana", 10.2F, FontStyle.Bold);
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (row.DataBoundItem is ScanResult result)
                {

                    switch (result.RawType)
                    {
                        case ScanObjectType.Malware:
                            row.Cells[0].Style.ForeColor = Color.Firebrick;
                            break;
                        case ScanObjectType.Infected:
                        case ScanObjectType.Unsafe:
                            row.Cells[0].Style.ForeColor = Color.Red;
                            break;
                        case ScanObjectType.Suspicious:
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            break;
                        case ScanObjectType.Unknown:
                            row.Cells[0].Style.ForeColor = Color.Black;
                            break;
                        default:
                            break;
                    }

                    switch (result.RawAction)
                    {
                        case ScanActionType.Cured:
                        case ScanActionType.Deleted:
                        case ScanActionType.Quarantine:
                        case ScanActionType.Terminated:
                        case ScanActionType.Disabled:
                            row.Cells[row.Cells.Count - 2].Style.BackColor = Color.PaleGreen;
                            break;
                        case ScanActionType.Skipped:
                            row.Cells[row.Cells.Count - 2].Style.BackColor = Color.White;
                            break;
                        case ScanActionType.Error:
                        case ScanActionType.Active:
                            row.Cells[row.Cells.Count - 2].Style.BackColor = Color.LightSalmon;
                            break;
                        case ScanActionType.Inaccassible:
                            row.Cells[row.Cells.Count - 2].Style.BackColor = Color.FromArgb(255, 248, 140, 248); //LightViolet
                            break;
                        case ScanActionType.LockedByAntivirus:
                            row.Cells[row.Cells.Count - 2].Style.Font = new Font("Segoe UI", 11F);
                            row.Cells[row.Cells.Count - 2].Style.BackColor = Color.FromArgb(255, 206, 190, 250);
                            break;
                    }
                }
            }
        }

        void UpdateToggle(string registryPath, string valueName)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true))
            {
                if (key != null)
                {
                    object regValue = key.GetValue(valueName);

                    if (regValue != null)
                    {
                        ts_AllowCollectStatistics.CheckedChanged -= ts_AllowCollectStatistics_CheckedChanged;
                        ts_AllowCollectStatistics.Checked = (int)regValue == 1;
                        ts_AllowCollectStatistics.CheckedChanged += ts_AllowCollectStatistics_CheckedChanged;

                        LBL_ID.Visible = (int)regValue == 1;
                    }
                }
            }
        }

        private void Label_OpenLogsFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (AppConfig.Instance.no_logs)
            {
                Process.Start("explorer.exe", Logger.LogsFolder);
                return;
            }

            string logpath = Path.Combine(Logger.LogsFolder, Logger.logFileName);
            if (!File.Exists(logpath))
            {
                return;
            }

            string argument = "/select, \"" + logpath + "\"";
            Process.Start("explorer.exe", argument);
        }

        private void OpenQuarantineBtn_Click(object sender, EventArgs e)
        {
            Hide();
            QuarantineForm qForm = new QuarantineForm(this)
            {
                Owner = this
            };
            qForm.ShowDialog();
        }

        private void DonateBtn_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.6;
            Enabled = false;
            using (SplashForm splashForm = new SplashForm(true)
            {
                Owner = this
            })
                splashForm.ShowDialog();
        }

        private void ts_AllowCollectStatistics_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(registryPath);
            }

            if (key != null)
            {
                object regValue = key.GetValue(valueName);

                if (regValue != null)
                {
                    if (ts_AllowCollectStatistics.CheckState == CheckState.Checked)
                    {
                        key.SetValue(valueName, 1, RegistryValueKind.DWord);
                        LBL_ID.Visible = true;
                    }
                    else
                    {
                        key.SetValue(valueName, 0, RegistryValueKind.DWord);
                        LBL_ID.Visible = false;
                    }
                }
                else
                {
                    key.SetValue(valueName, 0, RegistryValueKind.DWord);
                    LBL_ID.Visible = false;
                }
            }

            if (key != null)
            {
                key.Close();
            }

        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            TopMost = false;
        }

        private void LBL_ID_Click(object sender, EventArgs e)
        {
            OnMouseClick(LBL_ID);
        }

        void OnMouseClick(Label control)
        {
            Clipboard.SetText(control.Text);
            control.TextAlign = ContentAlignment.MiddleLeft;
            control.Text = AppConfig.Instance.LL.GetLocalizedString("_EventCopyText");
        }

        private void LBL_ID_MouseEnter(object sender, EventArgs e)
        {
            LBL_ID.ForeColor = Color.Black;
        }

        private void LBL_ID_MouseLeave(object sender, EventArgs e)
        {
            LBL_ID.ForeColor = Color.DarkGray;
            LBL_ID.Text = LBL_id_text;
        }
    }
}
