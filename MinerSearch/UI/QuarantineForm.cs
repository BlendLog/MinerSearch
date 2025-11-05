using Microsoft.Win32;
using MSearch.Core;
using MSearch.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MSearch
{
    public partial class QuarantineForm : FormShadow
    {
        FinishEx FinishEx = null;
        int SELECTED_ROWS_COUNT = 0;

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
            LBL_Quarantine.Text = AppConfig.Instance.LL.GetLocalizedString("_Quarantine");
            LBL_QuarantinedFiles.Text = AppConfig.Instance.LL.GetLocalizedString("_LabelQuarantinedFiles");
            RestoreSelectedBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_RestoreBtnText");
            DeleteSelectedBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_DeleteBtnText");
            finishBtn.Text = AppConfig.Instance.LL.GetLocalizedString(AppConfig.Instance.QuarantineMode == true ? "_exit" : "_BtnBack");
            top.Text = AppConfig.Instance._title;
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

            UpdateQuarantineCount(registryPath);


            foreach (QuarantineItem item in LoadQuarantineItems(registryPath))
            {
                dataGridQuarantineFiles.Rows.Add(null, item.OriginalPath, item.FileSize, item.FileHash);
            }
            dataGridQuarantineFiles.ClearSelection();

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
            header.Value = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Select");

            checkColumn.HeaderCell = header;
            dataGridQuarantineFiles.Columns.Add(checkColumn);

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PathColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_Path"),
                DataPropertyName = "Path"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSizeColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_FileSize"),
                DataPropertyName = "Size"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HashColumn",
                HeaderText = AppConfig.Instance.LL.GetLocalizedString("_DataGridHeader_FileHash"),
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
                        column.Width = 150;
                        column.MinimumWidth = 120;
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

        List<QuarantineItem> LoadQuarantineItems(string quarantineKeyPath)
        {
            var items = new List<QuarantineItem>();

            using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath))
            {
                if (baseKey != null)
                {
                    foreach (var subKeyName in baseKey.GetSubKeyNames())
                    {
                        using (var subKey = baseKey.OpenSubKey(subKeyName))
                        {
                            if (subKey == null) continue;

                            var originalPath = subKey.GetValue("OriginalPath") as string;
                            var fileData = subKey.GetValue("FileData") as byte[];

                            if (originalPath != null && fileData != null)
                            {
                                items.Add(new QuarantineItem
                                {
                                    OriginalPath = originalPath,
                                    FileSize = FileChecker.GetFileSize(fileData.Length),
                                    FileHash = subKeyName,
                                });
                            }
                        }
                    }
                }
            }

            using (var baseKey = Registry.LocalMachine.OpenSubKey(quarantineKeyPath))
            {
                if (baseKey != null)
                {
                    foreach (var subKeyName in baseKey.GetSubKeyNames())
                    {
                        using (var subKey = baseKey.OpenSubKey(subKeyName))
                        {
                            if (subKey == null) continue;

                            var originalPath = subKey.GetValue("OriginalPath") as string;
                            var totalParts = subKey.GetValue("TotalParts") as int?;

                            if (originalPath != null && totalParts != null)
                            {
                                long totalSize = 0;

                                for (int i = 0; i < totalParts; i++)
                                {
                                    var partData = subKey.GetValue($"FileData_Part{i}") as byte[];
                                    if (partData != null)
                                    {
                                        totalSize += partData.Length;
                                    }
                                }

                                items.Add(new QuarantineItem
                                {
                                    OriginalPath = originalPath,
                                    FileSize = FileChecker.GetFileSize(totalSize),
                                    FileHash = subKeyName,
                                });
                            }
                        }
                    }
                }
            }

            return items;
        }

        void UpdateQuarantineCount(string quarantineKeyPath)
        {
            int quarantineCount = 0;

            using (var HKCU_baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath))
            {
                if (HKCU_baseKey != null)
                {
                    foreach (var subKey in HKCU_baseKey.GetSubKeyNames())
                    {
                        if (HKCU_baseKey.OpenSubKey(subKey).GetValueNames().Length > 0)
                        {
                            quarantineCount += 1;
                        }
                    }
                }
            }

            using (var HKLM_baseKey = Registry.LocalMachine.OpenSubKey(quarantineKeyPath))
            {
                if (HKLM_baseKey != null)
                {
                    foreach (string subKey in HKLM_baseKey.GetSubKeyNames())
                    {
                        if (HKLM_baseKey.OpenSubKey(subKey).GetValueNames().Length > 0)
                        {
                            quarantineCount += 1;
                        }
                    }
                }
            }

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

        bool DeleteFileFromQuarantine(string quarantineKeyPath, string fileHash)
        {
            try
            {
                using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey?.OpenSubKey(fileHash) != null)
                    {
                        baseKey.DeleteSubKey(fileHash);
                        return true;
                    }
                }

                using (var baseKey = Registry.LocalMachine.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey?.OpenSubKey(fileHash) != null)
                    {
                        baseKey.DeleteSubKey(fileHash);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, fileHash);
                return false;
            }
        }

        bool RestoreFileFromQuarantine(string quarantineKeyPath, string fileHash, string restorePath)
        {
            try
            {
                using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey != null)
                    {
                        using (var fileKey = baseKey.OpenSubKey(fileHash))
                        {
                            if (fileKey != null)
                            {
                                var encryptedData = fileKey.GetValue("FileData") as byte[];
                                if (encryptedData != null)
                                {
                                    byte[] decryptedData = DecryptData(encryptedData, Encoding.UTF8.GetBytes(fileHash.Remove(8)));

                                    Directory.CreateDirectory(Path.GetDirectoryName(restorePath));
                                    File.WriteAllBytes(restorePath, decryptedData);

                                    baseKey.DeleteSubKey(fileHash);
                                    return true;
                                }
                            }
                        }
                    }
                }

                using (var baseKey = Registry.LocalMachine.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey == null)
                        return false;

                    using (var fileKey = baseKey.OpenSubKey(fileHash))
                    {
                        if (fileKey != null)
                        {
                            int? totalParts = fileKey.GetValue("TotalParts") as int?;
                            List<byte> DataList = new List<byte>();

                            if (totalParts.HasValue)
                            {
                                for (int i = 0; i < totalParts.Value; i++)
                                {
                                    byte[] part = fileKey.GetValue($"FileData_Part{i}") as byte[];
                                    if (part == null) return false;
                                    DataList.AddRange(part);
                                }

                                Directory.CreateDirectory(Path.GetDirectoryName(restorePath));
                                File.WriteAllBytes(restorePath, DataList.ToArray());

                                baseKey.DeleteSubKey(fileHash);
                                return true;
                            }
                            else return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                AppConfig.Instance.LL.LogErrorMessage("_Error", ex, fileHash);
                return false;
            }
        }

        byte[] DecryptData(byte[] encryptedData, byte[] key)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                throw new ArgumentException("Decryption data is invalid");

            byte[] decryptedData = new byte[encryptedData.Length];

            for (int i = 0; i < encryptedData.Length; i++)
            {
                decryptedData[i] = (byte)(encryptedData[i] ^ key[i % key.Length]);
            }

            return decryptedData;
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
                MessageBoxCustom.Show(AppConfig.Instance.LL.GetLocalizedString("_DataGrid_NoSelection"), AppConfig.Instance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isDeleteFilesAction)
            {
                if (MessageBoxCustom.Show(AppConfig.Instance.LL.GetLocalizedString("_QuarantineRemoveBtn").Replace("#FILESCOUNT#", SELECTED_ROWS_COUNT.ToString()), AppConfig.Instance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            if (UnlockObjectClass.IsRegistryKeyBlocked(REGISTRY_PATH_MAIN))
            {
                UnlockObjectClass.UnblockRegistry(REGISTRY_PATH_MAIN);
            }

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
                        if (RestoreFileFromQuarantine(REGISTRY_PATH_QUARANTINE, hash, path))
                        {
                            dataGridQuarantineFiles.Rows.RemoveAt(i);
                            UpdateQuarantineCount(REGISTRY_PATH_QUARANTINE);
                            affectedFiles.Add(path);
                        }
                    }
                    else
                    {
                        if (DeleteFileFromQuarantine(REGISTRY_PATH_QUARANTINE, hash))
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
                StringBuilder pathList = new StringBuilder(AppConfig.Instance.LL.GetLocalizedString(message).Replace("#FILESCOUNT#", affectedFiles.Count.ToString()) + "\n");
                foreach (string filePath in affectedFiles)
                {
                    pathList.Append($"\n{filePath}");
                }

                UpdateHeaderCheckBoxState();
                MessageBoxCustom.Show(pathList.ToString(), AppConfig.Instance.LL.GetLocalizedString("_Quarantine"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            SELECTED_ROWS_COUNT = 0;
        }

        void RestoreSelectedBtn_Click(object sender, EventArgs e)
        {
            DeleteOrRestoreAction(false, "_QuarantineRestoredFile");
        }

        void DeleteSelectedBtn_Click(object sender, EventArgs e)
        {
            DeleteOrRestoreAction(true, "_QuarantineRemovedFiles");
        }
    }
}
