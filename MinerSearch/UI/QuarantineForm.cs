using Microsoft.Win32;
using MSearch.Core;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MSearch
{
    public partial class QuarantineForm : FormShadow
    {
        FinishEx FinishEx = null;
        int SELECTED_ROWS_COUNT = 0;
        const int MAX_SUMMARY_PATHS = 10;

        readonly string REGISTRY_PATH_QUARANTINE = @"Software\M1nerSearch\Quarantine";
        readonly string REGISTRY_PATH_MAIN = @"Software\M1nerSearch";

        public QuarantineForm()
        {
            InitializeComponent();
            ConfigureDataGridView();
        }

        public QuarantineForm(FinishEx _finishForm)
        {
            InitializeComponent();
            ConfigureDataGridView();

            FinishEx = _finishForm;
        }

        void Finish()
        {
            if (this.FinishEx == null)
            {
                Environment.Exit(0);
            }

            this.FinishEx.Show();
            Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Finish();
        }
        private void FinishBtn_click(object sender, EventArgs e)
        {
            Finish();
        }

        private void TranslateForm()
        {
            LBL_Quarantine.Text = AppConfig.GetInstance.LL.GetLocalizedString("_Quarantine");
            LBL_QuarantinedFiles.Text = AppConfig.GetInstance.LL.GetLocalizedString("_LabelQuarantinedFiles");
            RestoreSelectedBtn.Text = AppConfig.GetInstance.LL.GetLocalizedString("_RestoreBtnText");
            DeleteSelectedBtn.Text = AppConfig.GetInstance.LL.GetLocalizedString("_DeleteBtnText");
            finishBtn.Text = AppConfig.GetInstance.LL.GetLocalizedString(LaunchOptions.GetInstance.QuarantineMode == true ? "_exit" : "_BtnBack");
            top.Text = AppConfig.GetInstance._title;
        }

        private void QuarantineForm_Load_1(object sender, EventArgs e)
        {
            string registryPathMain = @"Software\M1nerSearch";
            string registryPath = @"Software\M1nerSearch\Quarantine";
            TranslateForm();

            if (UnlockObjectClass.IsRegistryKeyBlocked(registryPathMain))
            {
                UnlockObjectClass.UnblockRegistry(registryPathMain);
            }

            if (UnlockObjectClass.IsRegistryKeyBlocked(registryPathMain, RegistryHive.LocalMachine))
            {
                UnlockObjectClass.UnblockRegistry(registryPathMain, RegistryHive.LocalMachine);
            }

            UpdateQuarantineCount(registryPath);

            foreach (QuarantineItem item in QuarantineManager.List())
            {
                int rowIndex = dataGridQuarantineFiles.Rows.Add(null, item.OriginalPath, item.FileSize, GetTypeLabel(item.ItemType), item.FileHash);
                dataGridQuarantineFiles.Rows[rowIndex].Cells["TypeColumn"].Tag = item.ItemType;
            }
            dataGridQuarantineFiles.ClearSelection();

        }

        static QuarantineItemType? GetRowType(DataGridViewRow row)
        {
            return row.Cells["TypeColumn"].Tag as QuarantineItemType?;
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
            dataGridQuarantineFiles.RowHeadersVisible = false;
            dataGridQuarantineFiles.AutoGenerateColumns = false;
            dataGridQuarantineFiles.EditMode = DataGridViewEditMode.EditOnEnter;

            var checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                DataPropertyName = "SelectColumn",
                TrueValue = true,
                FalseValue = false,
                IndeterminateValue = null,
                Width = 50,
                Resizable = DataGridViewTriState.False
            };

            DataGridViewCheckBoxHeaderCell header = new DataGridViewCheckBoxHeaderCell();
            header.CheckBoxClicked += Header_CheckBoxClicked;
            header.Value = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_Select");

            checkColumn.HeaderCell = header;
            dataGridQuarantineFiles.Columns.Add(checkColumn);

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PathColumn",
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_OriginalPath"),
                DataPropertyName = "Path"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSizeColumn",
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_FileSize"),
                DataPropertyName = "Size"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TypeColumn",
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_ObjectType"),
                DataPropertyName = "Type"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HashColumn",
                HeaderText = AppConfig.GetInstance.LL.GetLocalizedString("_DataGridHeader_FileHash"),
                DataPropertyName = "Hash"
            });

            foreach (DataGridViewColumn column in dataGridQuarantineFiles.Columns)
            {

                switch (column.Name)
                {
                    case "Selected":
                        column.Width = 32;
                        column.MinimumWidth = 100;
                        break;
                    case "PathColumn":
                        column.Width += 220;
                        column.MinimumWidth += 220;
                        break;
                    case "FileSizeColumn":
                        column.Width = 120;
                        column.MinimumWidth = 100;
                        break;
                    case "TypeColumn":
                        column.Width = 100;
                        column.MinimumWidth = 80;
                        break;
                    default:
                        column.Width = 520;
                        column.MinimumWidth = 520;
                        break;
                }

            }
        }

        void Header_CheckBoxClicked(object sender, EventArgs e)
        {
            var header = sender as DataGridViewCheckBoxHeaderCell;
            var state = header.CheckState;

            foreach (DataGridViewRow row in dataGridQuarantineFiles.Rows)
            {
                row.Cells["Selected"].Value = state == CheckState.Checked;
            }

            dataGridQuarantineFiles.RefreshEdit();
        }

        void UpdateQuarantineCount(string quarantineKeyPath)
        {
            int quarantineCount = QuarantineManager.List().Count;

            LBL_QFilesCount.Text = quarantineCount.ToString();

            dataGridQuarantineFiles.ClearSelection();

            UpdateUI(quarantineCount);
        }

        void UpdateUI(int quarantineCount)
        {
            if (quarantineCount == 0)
            {
                RestoreSelectedBtn.Enabled = DeleteSelectedBtn.Enabled = dataGridQuarantineFiles.Enabled = false;
                RestoreSelectedBtn.ForeColor = DeleteSelectedBtn.ForeColor = dataGridQuarantineFiles.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
                RestoreSelectedBtn.FlatAppearance.BorderColor = DeleteSelectedBtn.FlatAppearance.BorderColor = Color.Gray;
            }
        }

        /// <summary>
        /// Показывает итоговое сообщение со списком затронутых объектов.
        /// Список путей ограничен, чтобы окно не растягивалось на весь экран.
        /// </summary>
        void ShowOperationSummary(string messageKey, List<string> affectedFiles)
        {
            StringBuilder pathList = new StringBuilder(
                AppConfig.GetInstance.LL.GetLocalizedString(messageKey)
                    .Replace("#FILESCOUNT#", affectedFiles.Count.ToString()) + "\n");

            int shownCount = Math.Min(affectedFiles.Count, MAX_SUMMARY_PATHS);
            for (int i = 0; i < shownCount; i++)
            {
                pathList.Append($"\n{affectedFiles[i]}");
            }

            if (affectedFiles.Count > shownCount)
            {
                pathList.Append("\n" + AppConfig.GetInstance.LL.GetLocalizedString("_AndMoreItems")
                    .Replace("#COUNT#", (affectedFiles.Count - shownCount).ToString()));
            }

            MessageBoxCustom.Show(pathList.ToString(), AppConfig.GetInstance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void dataGridQuarantineFiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridQuarantineFiles.ClearSelection();

            if (e.RowIndex < 0) return; // Ignore headers click

            if (e.ColumnIndex == 0)
            {
                var cell = dataGridQuarantineFiles.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                bool currentValue = Convert.ToBoolean(cell.Value);
                cell.Value = !currentValue;
                return;
            }

        }

        CheckState GetCheckState(DataGridViewCheckBoxCell cell)
        {
            if (cell == null)
                return CheckState.Indeterminate;

            if (cell.Value == null || cell.Value == DBNull.Value)
                return CheckState.Indeterminate;

            try
            {
                if ((bool)cell.Value)
                    return CheckState.Checked;
                else
                    return CheckState.Unchecked;
            }
            catch
            {
                return CheckState.Indeterminate;
            }
        }

        string ResolveFileNameCollision(string targetPath, string quarantineHash)
        {
            if (!File.Exists(targetPath))
                return targetPath;

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(targetPath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    string existingHash = BitConverter.ToString(hash).Replace("-", "").ToLower();

                    if (existingHash == quarantineHash)
                        return targetPath;
                }
            }

            string directory = Path.GetDirectoryName(targetPath);
            string fileName = Path.GetFileNameWithoutExtension(targetPath);
            string extension = Path.GetExtension(targetPath);
            string collisionName = $"{fileName}_{quarantineHash}{extension}";
            string collisionPath = Path.Combine(directory, collisionName);

            int counter = 1;
            while (File.Exists(collisionPath))
            {
                collisionName = $"{fileName}_{quarantineHash}_{counter}{extension}";
                collisionPath = Path.Combine(directory, collisionName);
                counter++;
            }

            return collisionPath;
        }

        void dataGridQuarantineFiles_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            LBL_QFilesCount.Text = dataGridQuarantineFiles.Columns.GetColumnsWidth(DataGridViewElementStates.Visible).ToString();
        }

        void dataGridQuarantineFiles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridQuarantineFiles.Columns[e.ColumnIndex].Name == "Selected")
            {
                UpdateHeaderCheckBoxState();
            }
        }

        void UpdateHeaderCheckBoxState()
        {
            int totalCount = dataGridQuarantineFiles.Rows.Count;
            SELECTED_ROWS_COUNT = 0;

            foreach (DataGridViewRow row in dataGridQuarantineFiles.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Selected"].Value);
                if (isChecked) SELECTED_ROWS_COUNT++;
            }

            var header = dataGridQuarantineFiles.Columns["Selected"].HeaderCell as DataGridViewCheckBoxHeaderCell;

            if (header != null)
            {
                if (SELECTED_ROWS_COUNT == 0)
                    header.CheckState = CheckState.Unchecked;
                else if (SELECTED_ROWS_COUNT == totalCount)
                    header.CheckState = CheckState.Checked;
                else
                    header.CheckState = CheckState.Indeterminate;

                dataGridQuarantineFiles.InvalidateCell(header);
            }
        }

        void DeleteOrRestoreAction(bool isDeleteFilesAction, string message)
        {
            if (SELECTED_ROWS_COUNT == 0)
            {
                MessageBoxCustom.Show(AppConfig.GetInstance.LL.GetLocalizedString("_DataGrid_NoSelection"), AppConfig.GetInstance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isDeleteFilesAction)
            {
                if (MessageBoxCustom.Show(AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineRemoveBtn").Replace("#FILESCOUNT#", SELECTED_ROWS_COUNT.ToString()), AppConfig.GetInstance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            UnlockObjectClass.EnsureOwnSettingsKeyAccessible();

            List<string> affectedFiles = new List<string>();

            for (int i = dataGridQuarantineFiles.Rows.Count - 1; i >= 0; i--)
            {
                DataGridViewRow row = dataGridQuarantineFiles.Rows[i];
                var cell = row.Cells["Selected"] as DataGridViewCheckBoxCell;

                if (GetCheckState(cell) == CheckState.Checked)
                {
                    string path = row.Cells["PathColumn"].Value?.ToString();
                    string hash = row.Cells["HashColumn"].Value?.ToString();

                    if (!isDeleteFilesAction)
                    {
                        QuarantineItemType? rowType = GetRowType(row);
                        bool ok;

                        if (rowType == QuarantineItemType.Service)
                            ok = QuarantineManager.RestoreService(hash);
                        else if (rowType == QuarantineItemType.Task)
                            ok = QuarantineManager.RestoreTask(hash);
                        else
                            ok = QuarantineManager.RestoreFile(hash, path);

                        if (ok)
                        {
                            dataGridQuarantineFiles.Rows.RemoveAt(i);
                            UpdateQuarantineCount(REGISTRY_PATH_QUARANTINE);
                            affectedFiles.Add(path);
                        }
                    }
                    else
                    {
                        if (QuarantineManager.Delete(hash))
                        {
                            dataGridQuarantineFiles.Rows.RemoveAt(i);
                            UpdateQuarantineCount(REGISTRY_PATH_QUARANTINE);
                            affectedFiles.Add(path);
                        }
                    }
                }
            }


            if (affectedFiles.Count > 0)
            {
                UpdateHeaderCheckBoxState();
                ShowOperationSummary(message, affectedFiles);
            }

            SELECTED_ROWS_COUNT = 0;
        }

        void RestoreSelectedBtn_Click(object sender, EventArgs e)
        {
            if (SELECTED_ROWS_COUNT == 0)
            {
                MessageBoxCustom.Show(AppConfig.GetInstance.LL.GetLocalizedString("_DataGrid_NoSelection"), AppConfig.GetInstance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UnlockObjectClass.EnsureOwnSettingsKeyAccessible();

            bool hasNonFile = false;

            for (int i = 0; i < dataGridQuarantineFiles.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridQuarantineFiles.Rows[i];
                var cell = row.Cells["Selected"] as DataGridViewCheckBoxCell;
                if (GetCheckState(cell) == CheckState.Checked)
                {
                    QuarantineItemType? t = GetRowType(row);
                    if (t == QuarantineItemType.Service || t == QuarantineItemType.Task)
                    {
                        hasNonFile = true;
                    }
                }
            }

            if (hasNonFile)
            {
                List<string> affectedFiles = new List<string>();

                for (int i = dataGridQuarantineFiles.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridQuarantineFiles.Rows[i];
                    var cell = row.Cells["Selected"] as DataGridViewCheckBoxCell;
                    if (GetCheckState(cell) == CheckState.Checked)
                    {
                        QuarantineItemType? rowType = GetRowType(row);
                        string hash = row.Cells["HashColumn"].Value?.ToString();
                        string path = row.Cells["PathColumn"].Value?.ToString();
                        bool ok;

                        if (rowType == QuarantineItemType.Service)
                            ok = QuarantineManager.RestoreService(hash);
                        else if (rowType == QuarantineItemType.Task)
                            ok = QuarantineManager.RestoreTask(hash);
                        else
                            ok = QuarantineManager.RestoreFile(hash, path);

                        if (ok)
                        {
                            dataGridQuarantineFiles.Rows.RemoveAt(i);
                            UpdateQuarantineCount(REGISTRY_PATH_QUARANTINE);
                            affectedFiles.Add(path);
                        }
                    }
                }

                if (affectedFiles.Count > 0)
                {
                    UpdateHeaderCheckBoxState();
                    ShowOperationSummary("_QuarantineRestoredFile", affectedFiles);
                }

                SELECTED_ROWS_COUNT = 0;
                return;
            }

            using (var restoreForm = new QuarantineRestoreForm())
            {
                if (restoreForm.ShowDialog(this) != DialogResult.OK)
                    return;

                bool useCustomPath = restoreForm.rbCustomPath.Checked;
                string customPath = restoreForm.SelectedCustomPath;

                List<string> affectedFiles = new List<string>();

                for (int i = dataGridQuarantineFiles.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridQuarantineFiles.Rows[i];
                    var cell = row.Cells["Selected"] as DataGridViewCheckBoxCell;

                    if (GetCheckState(cell) == CheckState.Checked)
                    {
                        string originalPath = row.Cells["PathColumn"].Value?.ToString();
                        string hash = row.Cells["HashColumn"].Value?.ToString();

                        string targetPath = useCustomPath
                            ? Path.Combine(customPath, Path.GetFileName(originalPath))
                            : originalPath;

                        targetPath = ResolveFileNameCollision(targetPath, hash);

                        if (QuarantineManager.RestoreFile(hash, targetPath))
                        {
                            dataGridQuarantineFiles.Rows.RemoveAt(i);
                            UpdateQuarantineCount(REGISTRY_PATH_QUARANTINE);
                            affectedFiles.Add(targetPath);
                        }
                    }
                }

                if (affectedFiles.Count > 0)
                {
                    UpdateHeaderCheckBoxState();
                    ShowOperationSummary("_QuarantineRestoredFile", affectedFiles);
                }

                SELECTED_ROWS_COUNT = 0;
            }
        }

        void DeleteSelectedBtn_Click(object sender, EventArgs e)
        {
            DeleteOrRestoreAction(true, "_QuarantineRemovedFiles");
        }

        string GetTypeLabel(QuarantineItemType type)
        {
            switch (type)
            {
                case QuarantineItemType.Service:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_Service");
                case QuarantineItemType.Task:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_Task");
                default:
                    return AppConfig.GetInstance.LL.GetLocalizedString("_QuarantineType_File");
            }
        }
    }
}
