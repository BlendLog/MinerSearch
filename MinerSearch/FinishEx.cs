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

        public FinishEx(int _totalThreats, int _neutralizedThreats, int _suspObj, string _elapsedTime)
        {
            InitializeComponent();
            ConfigureDataGridView();

            threatsCount = _totalThreats;
            curedCount = _neutralizedThreats;
            LBL_threatsCount.Text = _totalThreats.ToString();
            LBL_curedCount.Text = _neutralizedThreats.ToString();
            LBL_susCount.Text = _suspObj.ToString();
            LBL_scanElapsedTime.Text = _elapsedTime;
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
            LBL_ScanTime.Text = Program.LL.GetLocalizedString("_Elapse");
            Label_showAllLogs.Text = Program.LL.GetLocalizedString("_ShowFolderLogs");
            top.TextAlign = ContentAlignment.MiddleRight;
            top.Text = Program.LL.GetLocalizedString("_PleaseWaitMessage");
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
                var result = MessageBox.Show(Program.LL.GetLocalizedString("_RebootPCNowDialog"), Utils.GetRndString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            Process.Start(new ProcessStartInfo()
            {
                FileName = "cmd",
                Arguments = argument,
                UseShellExecute = false,
                CreateNoWindow = true
            });
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
#if !DEBUG
            if (Program.totalFoundThreats == 0 && Program.totalFoundSuspiciousObjects == 0)
            {
                dataGridThreats.Visible = false;
            }
#endif
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
        }

        public void LoadResults(List<ScanResult> results)
        {
            dataGridThreats.DataSource = results;
        }

        private void FinishEx_Load(object sender, EventArgs e)
        {
            dataGridThreats.ClearSelection();
            TranslateForm();
            string registryPath = @"Software\M1nerSearch";
            string valueName = "allowstatistics";
            CollectStatistics(registryPath, valueName);
        }

        async void CollectStatistics(string registryPath, string valueName)
        {
            if (Program.no_logs)
            {
                top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                top.Text = "MinerSearch";
                CloseBtn.Visible = true;
                finishBtn.Enabled = true;
                finishBtn.BackColor = Color.RoyalBlue;
                finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                return;
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(registryPath);
            }

            object regValue = key.GetValue(valueName);

            if (regValue == null)
            {
                if (Utils.GetWindowsVersion().Contains("Windows 7"))
                {
                    key.SetValue(valueName, 0, RegistryValueKind.DWord);
                    regValue = 0;
                }
                else
                {
                    key.SetValue(valueName, 2, RegistryValueKind.DWord);
                    regValue = 2;
                }
            }

            int allowStatistics = (int)regValue;

            if (allowStatistics == 2)
            {
                var result = MessageBox.Show(Program.LL.GetLocalizedString("_AllowSentStatistics"), Utils.GetRndString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    key.SetValue(valueName, 1, RegistryValueKind.DWord);

                    if (!Program.no_logs)
                    {
                        try
                        {
                            await Task.Run(() =>
                            {
                                Task.Delay(new Random().Next(10, 3000));
                                MinerSearch.SentLog();
                            });
                            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            top.Text = "MinerSearch";
                            CloseBtn.Visible = true;
                            finishBtn.Enabled = true;
                            finishBtn.BackColor = Color.RoyalBlue;
                            finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                        }
                        catch (System.IO.FileNotFoundException fnf)
                        {
                            Program.LL.LogErrorMessage("_Error", fnf);
                            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            top.Text = "MinerSearch";
                            CloseBtn.Visible = true;
                            finishBtn.Enabled = true;
                            finishBtn.BackColor = Color.RoyalBlue;
                            finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_Error", ex);
                            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            top.Text = "MinerSearch";
                            CloseBtn.Visible = true;
                            finishBtn.Enabled = true;
                            finishBtn.BackColor = Color.RoyalBlue;
                            finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                        }

                    }
                }
                else
                {
                    key.SetValue(valueName, 0, RegistryValueKind.DWord);
                    top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    top.Text = "MinerSearch";
                    CloseBtn.Visible = true;
                    finishBtn.Enabled = true;
                    finishBtn.BackColor = Color.RoyalBlue;
                    finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                }
            }
            else if (allowStatistics == 1)
            {
                if (!Program.no_logs)
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            Task.Delay(new Random().Next(10, 3000));
                            MinerSearch.SentLog();
                        });
                        top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        top.Text = "MinerSearch";
                        CloseBtn.Visible = true;
                        finishBtn.Enabled = true;
                        finishBtn.BackColor = Color.RoyalBlue;
                        finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                    }
                    catch (System.IO.FileNotFoundException fnf)
                    {
                        Program.LL.LogErrorMessage("_Error", fnf);
                        top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        top.Text = "MinerSearch";
                        CloseBtn.Visible = true;
                        finishBtn.Enabled = true;
                        finishBtn.BackColor = Color.RoyalBlue;
                        finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");

                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                        top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        top.Text = "MinerSearch";
                        CloseBtn.Visible = true;
                        finishBtn.Enabled = true;
                        finishBtn.BackColor = Color.RoyalBlue;
                        finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
                    }
                }
            }
            else
            {
                top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                top.Text = "MinerSearch";
                CloseBtn.Visible = true;
                finishBtn.Enabled = true;
                finishBtn.BackColor = Color.RoyalBlue;
                finishBtn.Text = Program.LL.GetLocalizedString("_DataGrid_FinishBtn");
            }
            key.Close();
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
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.PaleGreen;
                            break;
                        case ScanActionType.Skipped:
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.White;
                            break;
                        case ScanActionType.Error:
                        case ScanActionType.Active:
                            row.Cells[row.Cells.Count - 1].Style.BackColor = Color.LightSalmon;
                            break;
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
    }
}
