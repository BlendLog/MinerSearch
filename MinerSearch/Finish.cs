﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSearch
{
    public partial class Finish : Form
    {
        int threatsCount = 0;
        int curedCount = 0;

        private int targetHeight = 415;
        private int step = 2;

        public Finish(int _totalThreats, int _neutralizedThreats, string _elapsedTime)
        {
            InitializeComponent();
            threatsCount = _totalThreats;
            curedCount = _neutralizedThreats;
            LBL_threatsCount.Text = _totalThreats.ToString();
            LBL_curedCount.Text = _neutralizedThreats.ToString();  
            LBL_scanElapsedTime.Text = _elapsedTime;
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

        void OpenExternalLink(string link)
        {
            if (link.StartsWith("http"))
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = "explorer",
                    Arguments = $"\"{link}\""
                });
            }
        }

        private void pb_QR_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://boosty.to/blendlog/donate");
        }

        private void pb_telegram_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_blog");
        }

        private void pb_M1nerSearch_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_chat");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (curedCount < threatsCount)
            {
                var result = MessageBox.Show(Program.LL.GetLocalizedString("_FinishRebootPCNow"), Utils.GetRndString(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

            if (curedCount == threatsCount && !Program.WinPEMode && !Program.ScanOnly && threatsCount != 0)
            {
                top.Enabled = false;
                await Task.Delay(1000);
                AnimationTimer.Start();
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
                            top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }
                        catch (System.IO.FileNotFoundException fnf)
                        {
                            Program.LL.LogErrorMessage("_Error", fnf);
                            top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }
                        catch (Exception ex)
                        {
                            Program.LL.LogErrorMessage("_Error", ex);
                            top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                            top.Text = "MinerSearch          ";
                            button1.Visible = true;
                        }

                    }
                }
                else
                {
                    key.SetValue(valueName, 0, RegistryValueKind.DWord);
                    top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
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
                        top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;
                    }
                    catch (System.IO.FileNotFoundException fnf)
                    {
                        Program.LL.LogErrorMessage("_Error", fnf);
                        top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        Program.LL.LogErrorMessage("_Error", ex);
                        top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                        top.Text = "MinerSearch          ";
                        button1.Visible = true;
                    }
                }
            }
            else
            {
                top.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
                top.Text = "MinerSearch          ";
                button1.Visible = true;
            }
            key.Close();

        }

        void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (this.Height < targetHeight)
            {
                this.Height += step;

                Location = new System.Drawing.Point(Location.X, Location.Y - 1);
            }
            else
            {
                AnimationTimer.Stop();
                top.Enabled = true;
            }
        }

        void TranslateForm()
        {
            if (Program.ActiveLanguage != "RU")
            {
                textBox1.Select(0, 0);
                yoomoney_tb.Visible = false;
            }
            else yoomoney_tb.Select(0, 0);

            if (Program.totalFoundThreats > 0)
            {
                LBL_totalThreats.ForeColor = System.Drawing.Color.DarkRed;
                LBL_threatsCount.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                LBL_totalThreats.ForeColor = System.Drawing.Color.DarkGreen;
                LBL_threatsCount.ForeColor = System.Drawing.Color.DarkGreen;
            }

            if (Program.ScanOnly || Program.totalFoundThreats == 0)
            {
                LBL_neutralizedThreats.Visible = false;
                LBL_curedCount.Visible = false;
            }

            if (Program.no_logs)
            {
                btnDetails.Visible = false;
            }

            LBL_ScanComplete.Text = Program.LL.GetLocalizedString("_End");
            LBL_totalThreats.Text = Program.LL.GetLocalizedString("_TotalThreatsFound");
            LBL_neutralizedThreats.Text = Program.LL.GetLocalizedString("_TotalNeutralizedThreats");
            LBL_JoinTelegram.Text = Program.LL.GetLocalizedString("_JoinToTelegram");
            LBL_ScanTime.Text = Program.LL.GetLocalizedString("_Elapse");
            LBL_Support.Text = Program.LL.GetLocalizedString("_LabelSupport");
            btnDetails.Text = Program.LL.GetLocalizedString("_BtnDetails");
            top.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            top.Text = Program.LL.GetLocalizedString("_PleaseWaitMessage");

            if (!Program.ScanOnly)
            {
                if (threatsCount > 0)
                {
                    if (curedCount < threatsCount)
                    {
                        FinalStatus_label.Text = Program.LL.GetLocalizedString("_FinishNotAllThreatsNeutralized");
                        FinalStatus_label.ForeColor = System.Drawing.Color.Red;
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
                FinalStatus_label.ForeColor = System.Drawing.Color.Blue;
            }
        }

        private void pb_QR_MouseEnter(object sender, EventArgs e)
        {
            pb_QR.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pb_QR_MouseLeave(object sender, EventArgs e)
        {
            pb_QR.BorderStyle = BorderStyle.None;
        }

        private void pb_telegram_MouseEnter(object sender, EventArgs e)
        {
            pb_telegram.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pb_telegram_MouseLeave(object sender, EventArgs e)
        {
            pb_telegram.BorderStyle = BorderStyle.None;
        }

        private void pb_M1nerSearch_MouseLeave(object sender, EventArgs e)
        {
            pb_M1nerSearch.BorderStyle = BorderStyle.None;
        }

        private void pb_M1nerSearch_MouseEnter(object sender, EventArgs e)
        {
            pb_M1nerSearch.BorderStyle = BorderStyle.FixedSingle;
        }

        private void LBL_link1_MouseEnter(object sender, EventArgs e)
        {
            LBL_link1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link1_MouseLeave(object sender, EventArgs e)
        {
            LBL_link1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link2_MouseEnter(object sender, EventArgs e)
        {
            LBL_link2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link2_MouseLeave(object sender, EventArgs e)
        {
            LBL_link2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
        }

        private void LBL_link1_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_blog");
        }

        private void LBL_link2_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://t.me/MinerSearch_chat");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OpenExternalLink("https://boosty.to/BlendLog/donate");
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
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
