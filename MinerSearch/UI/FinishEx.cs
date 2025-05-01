using DBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            LBL_ScanTime.Text = $"{Program.LL.GetLocalizedString("_Elapse")} {_elapsedTime}";
            LBL_id_text = DeviceIdProvider.GetDeviceId();
            LBL_ID.Text = LBL_id_text;
        }

        private void TranslateForm()
        {
            if (Program.ScanOnly || Program.totalFoundThreats == 0)
            {
                LBL_neutralizedThreats.Visible = false;
                LBL_curedCount.Visible = false;
            }

            if (Program.no_logs)
            {
                btnDetails.Enabled = false;
                btnDetails.BackColor = Color.FromArgb(10, 255, 255, 255);
                btnDetails.FlatAppearance.BorderSize = 0;
                btnDetails.Text = Program.LL.GetLocalizedString("_NoLogBtn");
            }
            else
            {
                btnDetails.Text = Program.LL.GetLocalizedString("_BtnDetails");
            }

            LBL_ScanComplete.Text = Program.LL.GetLocalizedString("_End");
            LBL_totalThreats.Text = Program.LL.GetLocalizedString("_TotalThreatsFound");
            LBL_neutralizedThreats.Text = Program.LL.GetLocalizedString("_TotalNeutralizedThreats");
            LBL_susObjects.Text = Program.LL.GetLocalizedString("_SuspiciousObjects");
            Label_allowStatistics.Text = Program.LL.GetLocalizedString("_LabelAllowStatistics");
            Label_showAllLogs.Text = Program.LL.GetLocalizedString("_ShowFolderLogs");
            top.TextAlign = ContentAlignment.MiddleRight;
            top.Text = Program.LL.GetLocalizedString("_PleaseWaitMessage");
            OpenQuarantineBtn.Text = Program.LL.GetLocalizedString("_OpenQuarantine");
            DonateBtn.Text = Program.LL.GetLocalizedString("_DonateBtn");
            finishBtn.Text = Program.LL.GetLocalizedString("_PleaseWaitMessage");
            finishBtn.BackColor = Color.DimGray;

            Label_OpenLogsFolder.Text = Logger.LogsFolder;

            if (!Program.ScanOnly)
            {
                if (threatsCount > 0)
                {
                    if (curedCount < threatsCount)
                    {
                        FinalStatus_label.Text = Program.LL.GetLocalizedString("_FinishNotAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.LightSalmon;
                    }
                    else if (curedCount == threatsCount)
                    {
                        FinalStatus_label.Text = Program.LL.GetLocalizedString("_FinishAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.LightGreen;

                    }
                }
                else
                {
                    FinalStatus_label.Text = Program.LL.GetLocalizedString("_NoThreats");
                }
            }
            else
            {
                FinalStatus_label.Text = Program.LL.GetLocalizedString("_ScanOnlyMode");
            }
        }

        void Finish()
        {
            if (curedCount < threatsCount)
            {
                var result = MessageBoxCustom.Show(Program.LL.GetLocalizedString("_RebootPCNowDialog"), Program._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                if (!Program.ScanOnly)
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
            if (Program.RunAsSystem && !Program.WinPEMode)
            {
                string pname = Bfs.Create("XowZueZ4My9sRztk+8mdpA==", "IQ70zViMhrk1BBJC+eBfce4X9xr/dKa73zS+8a8DgdQ=", "Mxx86B7zZeNAUe7VPZMT/w==");
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
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_ObjectType"),
                DataPropertyName = "Type"
            });

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PathColumn",
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_Path"),
                DataPropertyName = "Path"
            });

            dataGridThreats.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ActionColumn",
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_Action"),
                DataPropertyName = "Action"
            });

            // Calculate all coloumns width
            int totalWidth = dataGridThreats.ClientSize.Width;
            int columnCount = dataGridThreats.Columns.Count;

            int columnWidth = totalWidth / columnCount;

            foreach (DataGridViewColumn column in dataGridThreats.Columns)
            {
                column.Width = columnWidth;
                column.MinimumWidth = columnWidth;
            }

            if (Program.totalFoundThreats == 0 && Program.totalFoundSuspiciousObjects == 0)
            {
                dataGridThreats.Columns.Clear();
            }
        }

        public void LoadResults(List<ScanResult> results)
        {
            dataGridThreats.DataSource = results;
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
            if (Program.no_logs)
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
                        if (MessageBoxCustom.Show(Program.LL.GetLocalizedString("_AllowSentStatistics"), Program._title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            top.Text = Program._title;
            CloseBtn.Visible = true;
            MinimizeBtn.Visible = true;
            DonateBtn.Visible = true;
            finishBtn.Enabled = true;
            finishBtn.BackColor = Color.RoyalBlue;
            finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");

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
                    Task.Delay(new Random().Next(10, 5000)).Wait();
                    MinerSearch.SentLog();
                });
            }
            catch (FileNotFoundException fnf)
            {
                Program.LL.LogErrorMessage("_Error", fnf);
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex);
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
                        case ScanObjectType.Infected:
                            row.Cells[0].Style.ForeColor = Color.Red;
                            break;
                        case ScanObjectType.Suspicious:
                            row.Cells[0].Style.ForeColor = Color.Orange;
                            break;
                        case ScanObjectType.Unknown:
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
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.PaleGreen;
                            break;
                        case ScanActionType.Skipped:
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.White;
                            break;
                        case ScanActionType.Error:
                        case ScanActionType.Active:
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.LightSalmon;
                            break;
                        case ScanActionType.Inaccassible:
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.FromArgb(255, 248, 140, 248); //LightViolet
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
            if (Program.no_logs)
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
            control.Text = Program.LL.GetLocalizedString("_EventCopyText");
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
