using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSearch
{
    public partial class Finish : FormShadow
    {
        int threatsCount = 0;
        int curedCount = 0;

        public Finish(int _totalThreats, int _neutralizedThreats, int _suspObj, string _elapsedTime)
        {
            InitializeComponent();
            threatsCount = _totalThreats;
            curedCount = _neutralizedThreats;
            LBL_threatsCount.Text = _totalThreats.ToString();
            LBL_curedCount.Text = _neutralizedThreats.ToString();
            Label_SuspiciousObjectsCount.Text = _suspObj.ToString();
            LBL_scanElapsedTime.Text = _elapsedTime;


            if (!Program.ScanOnly)
            {
                if (threatsCount == 0)
                {
                    LBL_totalThreats.ForeColor = Color.Green;
                    LBL_threatsCount.ForeColor = Color.Green;
                    LBL_ScanComplete.ForeColor = Color.Green;
                }
                else if (threatsCount > curedCount)
                {
                    LBL_totalThreats.ForeColor = Color.Crimson;
                    LBL_threatsCount.ForeColor = Color.Crimson;
                    LBL_ScanComplete.ForeColor = Color.DarkRed;
                }
                else if (threatsCount == curedCount)
                {
                    LBL_totalThreats.ForeColor = Color.Green;
                    LBL_threatsCount.ForeColor = Color.Green;
                }
            }
            else
            {
                LBL_totalThreats.ForeColor = Color.DodgerBlue;
                LBL_threatsCount.ForeColor = Color.DodgerBlue;
                LBL_ScanComplete.ForeColor = Color.DodgerBlue;
                FinalStatus_label.ForeColor = Color.DodgerBlue;
            }
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

        private void button1_Click(object sender, EventArgs e)
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
                Hide();
                SplashForm splashForm = new SplashForm();
                splashForm.ShowDialog();
            }
            Environment.Exit(0);
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private async void Finish_Load(object sender, EventArgs e)
        {
            TopMost = true;
            TranslateForm();

            if (Program.no_logs)
            {
                top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                top.Text = "MinerSearch          ";
                button1.Visible = true;
                return;
            }

            string registryPath = @"Software\M1nerSearch";
            string valueName = "allowstatistics";

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
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }
                        catch (System.IO.FileNotFoundException fnf)
                        {
                            Program.LL.LogErrorMessage("_Error", fnf);
                            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_Error", ex);
                            top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }

                    }
                }
                else
                {
                    key.SetValue(valueName, 0, RegistryValueKind.DWord);
                    top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    top.Text = "MinerSearch          ";
                    button1.Visible = true;
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
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;
                    }
                    catch (System.IO.FileNotFoundException fnf)
                    {
                        Program.LL.LogErrorMessage("_Error", fnf);
                        top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                        top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;
                    }
                }
            }
            else
            {
                top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                top.Text = "MinerSearch          ";
                button1.Visible = true;
            }
            key.Close();

        }

        void TranslateForm()
        {

            if (Program.ScanOnly || Program.totalFoundThreats == 0)
            {
                LBL_neutralizedThreats.Visible = false;
                LBL_curedCount.Visible = false;
                LabelSeparator.Visible = false;
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
            Label_suspiciousObjects.Text = Program.LL.GetLocalizedString("_SuspiciousObjects");
            LBL_ScanTime.Text = Program.LL.GetLocalizedString("_Elapse");
            Label_showAllLogs.Text = Program.LL.GetLocalizedString("_ShowFolderLogs");
            top.TextAlign = ContentAlignment.TopRight;
            top.Text = Program.LL.GetLocalizedString("_PleaseWaitMessage");

            Label_OpenLogsFolder.Text = Logger.LogsFolder;

            if (!Program.ScanOnly)
            {
                if (threatsCount > 0)
                {
                    if (curedCount < threatsCount)
                    {
                        FinalStatus_label.Text = Program.LL.GetLocalizedString("_FinishNotAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (curedCount == threatsCount)
                    {
                        FinalStatus_label.Text = Program.LL.GetLocalizedString("_FinishAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.Green;

                    }
                }
                else
                {
                    FinalStatus_label.Text = Program.LL.GetLocalizedString("_NoThreats");
                    FinalStatus_label.ForeColor = System.Drawing.Color.Green;
                }
            }
            else
            {
                FinalStatus_label.Text = Program.LL.GetLocalizedString("_ScanOnlyMode");
            }
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
    }
}
