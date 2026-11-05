using DBase;
using MSearch.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Results page of the last scan.
    /// </summary>
    public partial class ResultsPage : UserControl
    {
        public event EventHandler OpenQuarantineRequested;

        private ScanStatistics _statistics;
        private bool _canceled;
        private ScanMode? _scanMode;

        private DataGridViewTextBoxColumn _colType;
        private DataGridViewTextBoxColumn _colPath;
        private DataGridViewTextBoxColumn _colClass;
        private DataGridViewTextBoxColumn _colAction;
        private DataGridViewTextBoxColumn _colNote;

        public ResultsPage()
        {
            InitializeComponent();
            ConfigureGrid();

            if (!IsInDesignMode)
            {
                ApplyLanguage();
                Clear();
            }
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void ConfigureGrid()
        {
            // Колонки создаются кодом: дизайнер не должен быть единственным источником истины.
            dataGridResults.AutoGenerateColumns = false;
            dataGridResults.Columns.Clear();

            // Минимальные ширины дают горизонтальную прокрутку, когда колонки
            // перестают помещаться в окно; на широком окне Fill растягивает их.
            _colType = CreateColumn("colType", "Type", 13F, 160);
            _colPath = CreateColumn("colPath", "Path", 44F, 500);
            _colClass = CreateColumn("colClass", "Class", 13F, 130);
            _colAction = CreateColumn("colAction", "Action", 15F, 190);
            _colNote = CreateColumn("colNote", "Note", 15F, 150);

            // Путь должен тянуться мышью: в Fill-режиме колонка не может стать
            // шире окна, поэтому полный путь было не увидеть.
            _colPath.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _colPath.Width = 500;
            _colPath.MinimumWidth = 300;
            _colPath.Resizable = DataGridViewTriState.True;

            // Примечание тоже должно читаться целиком — фиксированная ширина и перетаскивание.
            _colNote.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            _colNote.Width = 300;
            _colNote.MinimumWidth = 120;
            _colNote.Resizable = DataGridViewTriState.True;

            dataGridResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridResults.Columns.AddRange(new DataGridViewColumn[]
            {
                _colType, _colPath, _colClass, _colAction, _colNote
            });

            dataGridResults.ScrollBars = ScrollBars.Both;

            dataGridResults.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 251),
                ForeColor = Theme.Muted,
                Font = Theme.BodyBold,
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };
        }

        private static DataGridViewTextBoxColumn CreateColumn(string name, string property, float weight, int minimumWidth)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = property,
                ReadOnly = true,
                FillWeight = weight,
                MinimumWidth = minimumWidth,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
        }

        private void panelGridHost_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelGridHost.Width - 1, panelGridHost.Height - 1);
            }
        }

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            OpenLogFile();
        }

        private void buttonLogsFolder_Click(object sender, EventArgs e)
        {
            OpenLogsFolder();
        }

        private void buttonQuarantine_Click(object sender, EventArgs e)
        {
            EventHandler handler = OpenQuarantineRequested;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void buttonReboot_Click(object sender, EventArgs e)
        {
            PromptReboot();
        }

        private void Grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int end = Math.Min(e.RowIndex + e.RowCount, dataGridResults.Rows.Count);
            for (int index = Math.Max(0, e.RowIndex); index < end; index++)
                StyleRow(dataGridResults.Rows[index]);
        }

        private void StyleRow(DataGridViewRow row)
        {
            if (row == null)
                return;

            row.Cells[0].Style.Font = Theme.BodyBold;
            row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            ScanResult result = row.DataBoundItem as ScanResult;
            if (result == null)
                return;

            // Полный путь по наведению независимо от ширины колонки.
            if (_colPath != null && _colPath.Index >= 0 && _colPath.Index < row.Cells.Count)
                row.Cells[_colPath.Index].ToolTipText = result.Path ?? string.Empty;

            // Примечание может не влезть в ячейку — показываем его целиком в подсказке.
            if (!string.IsNullOrEmpty(result.Note) && _colNote != null &&
                _colNote.Index >= 0 && _colNote.Index < row.Cells.Count)
                row.Cells[_colNote.Index].ToolTipText = result.Note;

            switch (result.RawType)
            {
                case ScanObjectType.Malware:
                    row.Cells[0].Style.ForeColor = Theme.Danger;
                    break;
                case ScanObjectType.Infected:
                case ScanObjectType.Unsafe:
                    row.Cells[0].Style.ForeColor = Color.Red;
                    break;
                case ScanObjectType.Suspicious:
                    row.Cells[0].Style.ForeColor = Color.Orange;
                    break;
                case ScanObjectType.Rootkit:
                    row.Cells[0].Style.ForeColor = Color.DarkRed;
                    break;
                default:
                    row.Cells[0].Style.ForeColor = Theme.Text;
                    break;
            }

            DataGridViewCell actionCell = row.Cells[row.Cells.Count - 2];
            switch (result.RawAction)
            {
                case ScanActionType.Cured:
                case ScanActionType.Deleted:
                case ScanActionType.Quarantine:
                case ScanActionType.Terminated:
                case ScanActionType.Disabled:
                    actionCell.Style.BackColor = Color.PaleGreen;
                    break;
                case ScanActionType.Skipped:
                    actionCell.Style.BackColor = Color.White;
                    break;
                case ScanActionType.Error:
                case ScanActionType.Active:
                    actionCell.Style.BackColor = Color.LightSalmon;
                    break;
                case ScanActionType.Inaccessible:
                    actionCell.Style.BackColor = Color.FromArgb(255, 248, 140, 248);
                    break;
                case ScanActionType.LockedByAntivirus:
                    actionCell.Style.BackColor = Color.FromArgb(255, 206, 190, 250);
                    break;
                case ScanActionType.RebootPending:
                    actionCell.Style.BackColor = Color.Khaki;
                    break;
            }
        }

        public void Clear()
        {
            _statistics = null;
            _canceled = false;
            dataGridResults.DataSource = null;
            labelSummary.Text = L("_Results_Empty");
            labelSummary.ForeColor = Theme.Text;
            labelDetails.Text = string.Empty;
            labelEmpty.Text = L("_Results_EmptyHint");
            labelEmpty.Visible = true;
            dataGridResults.Visible = false;
            buttonReboot.Visible = false;
            buttonOpenLog.Enabled = Logger.HasScanLog;
        }

        public void ShowResults(ScanWorkflowResult result, ScanMode mode)
        {
            if (result == null || result.Statistics == null)
                return;

            _scanMode = mode;
            labelTitle.Text = BuildTitle();

            _statistics = result.Statistics;
            _canceled = result.Canceled;

            List<ScanResult> items = new List<ScanResult>();
            if (result.Results != null)
            {
                foreach (ScanResult item in result.Results)
                {
                    if (item != null)
                        items.Add(item);
                }
            }

            dataGridResults.DataSource = null;
            dataGridResults.DataSource = items;
            dataGridResults.Visible = items.Count > 0;

            labelEmpty.Visible = items.Count == 0;
            labelEmpty.Text = L("_NoThreats");

            UpdateSummary();
            buttonOpenLog.Enabled = Logger.HasScanLog;
        }

        private void UpdateSummary()
        {
            ScanStatistics stats = _statistics;
            int notNeutralized = stats.Found - stats.Neutralized - stats.Skipped;

            if (_canceled)
            {
                labelSummary.Text = L("_ScanCanceled");
                labelSummary.ForeColor = Theme.Warning;
            }
            else if (LaunchOptions.GetInstance.ScanOnly)
            {
                labelSummary.Text = L("_ScanOnlyMode");
                labelSummary.ForeColor = Theme.Text;
            }
            else if (stats.Found > 0)
            {
                if (stats.Neutralized == stats.Found)
                {
                    if (stats.LockedByAv > 0)
                    {
                        labelSummary.Text = L("_FinishThreatsBlockedByAV").Replace("#COUNT#", stats.LockedByAv.ToString());
                        labelSummary.ForeColor = Theme.Success;
                    }
                    else
                    {
                        labelSummary.Text = L("_FinishAllThreatsNeutralized");
                        labelSummary.ForeColor = Theme.Success;
                    }
                }
                else if (stats.Skipped > 0 && notNeutralized <= 0)
                {
                    labelSummary.Text = L("_FinishSomeThreatsSkipped").Replace("#COUNT#", stats.Skipped.ToString());
                    labelSummary.ForeColor = Theme.Warning;
                }
                else
                {
                    labelSummary.Text = L("_FinishNotAllThreatsNeutralized");
                    labelSummary.ForeColor = Color.LightSalmon;
                }
            }
            else
            {
                labelSummary.Text = L("_NoThreats");
                labelSummary.ForeColor = Theme.Success;
            }

            labelDetails.Text = L("_Counter_Found") + ": " + stats.Found
                + "   ·   " + L("_Counter_Neutralized") + ": " + stats.Neutralized
                + "   ·   " + L("_Counter_Suspicious") + ": " + stats.Suspicious
                + "   ·   " + L("_Elapse") + " " + stats.ElapsedTime;

            buttonReboot.Visible = !_canceled && !LaunchOptions.GetInstance.ScanOnly && notNeutralized > 0;
        }

        private void OpenLogFile()
        {
            string path = Logger.LogFilePath;

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                MessageBoxCustom.Show(
                    L("_LogFileNotFound"),
                    AppConfig.GetInstance._title,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                OpenLogsFolder();
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = MSData.GetInstance.consts[MSKeys.ExplorerExe],
                    Arguments = "\"" + path + "\""
                });
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
                OpenLogsFolder();
            }
        }

        private void OpenLogsFolder()
        {
            try
            {
                string path = Logger.LogFilePath;
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    Process.Start(MSData.GetInstance.consts[MSKeys.ExplorerExe], "/select, \"" + path + "\"");
                else if (Directory.Exists(Logger.LogsFolder))
                    Process.Start(MSData.GetInstance.consts[MSKeys.ExplorerExe], Logger.LogsFolder);
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }

        private void PromptReboot()
        {
            DialogResult result = MessageBoxCustom.Show(
                L("_RebootPCNowDialog"),
                AppConfig.GetInstance._title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown",
                    Arguments = "/r /t 0",
                    UseShellExecute = false,
                    CreateNoWindow = true
                });
            }
            catch (Exception ex)
            {
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = BuildTitle();

            if (_colType != null) _colType.HeaderText = L("_DataGridHeader_ObjectType");
            if (_colPath != null) _colPath.HeaderText = L("_DataGridHeader_Path");
            if (_colClass != null) _colClass.HeaderText = L("_DataGridHeader_Class");
            if (_colAction != null) _colAction.HeaderText = L("_DataGridHeader_Action");
            if (_colNote != null) _colNote.HeaderText = L("_DataGridHeader_Note");

            buttonOpenLog.Text = L("_Results_OpenLog");
            buttonLogsFolder.Text = L("_ShowFolderLogs");
            buttonQuarantine.Text = L("_OpenQuarantine");
            buttonReboot.Text = L("_Results_Reboot");

            if (_statistics == null)
            {
                labelSummary.Text = L("_Results_Empty");
                labelEmpty.Text = L("_Results_EmptyHint");
            }
            else
            {
                UpdateSummary();
            }
        }

        private string BuildTitle()
        {
            string title = L("_Results");
            if (_scanMode.HasValue)
                title += " | " + GetScanModeTitle(_scanMode.Value);
            return title;
        }

        private static string GetScanModeTitle(ScanMode mode)
        {
            switch (mode)
            {
                case ScanMode.Full:
                    return L("_ScanMode_Full_Title");
                case ScanMode.Selective:
                    return L("_ScanMode_Selective_Title");
                default:
                    return L("_ScanMode_Quick_Title");
            }
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
