using Microsoft.Win32;
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
            LBL_Quarantine.Text = Program.LL.GetLocalizedString("_Quarantine");
            LBL_QuarantinedFiles.Text = Program.LL.GetLocalizedString("_LabelQuarantinedFiles");
            finishBtn.Text = Program.LL.GetLocalizedString("_exit");
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
                dataGridQuarantineFiles.Rows.Add(null, null, item.OriginalPath, item.FileSize, item.FileHash);
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

            DataGridViewCellStyle DGVButtonRestore = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                ForeColor = Color.RoyalBlue,
                BackColor = Color.White,

                NullValue = Program.LL.GetLocalizedString("_RestoreBtnText"),
                Padding = new Padding(8, 1, 8, 1)
            };

            DataGridViewCellStyle DGVButtonDelete = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                ForeColor = Color.Crimson,
                BackColor = Color.White,

                NullValue = Program.LL.GetLocalizedString("_DeleteBtnText"),
                Padding = new Padding(8, 1, 8, 1)
            };

            dataGridQuarantineFiles.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Restore",
                HeaderText = "",
                FlatStyle = FlatStyle.Flat,
                FillWeight = 150,
                MinimumWidth = 150,
                UseColumnTextForButtonValue = true,
                DefaultCellStyle = DGVButtonRestore,
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "",
                DefaultCellStyle = DGVButtonDelete,
                FlatStyle = FlatStyle.Flat,
                FillWeight = 150,
                MinimumWidth = 150,
                UseColumnTextForButtonValue = true
            });


            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PathColumn",
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_Path"),
                DataPropertyName = "Path"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSizeColumn",
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_FileSize"),
                DataPropertyName = "Size"
            });

            dataGridQuarantineFiles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HashColumn",
                HeaderText = Program.LL.GetLocalizedString("_DataGridHeader_FileHash"),
                DataPropertyName = "Hash"
            });

            // Calculate all coloumns width
            int totalWidth = dataGridQuarantineFiles.ClientSize.Width;
            int columnCount = dataGridQuarantineFiles.Columns.Count;

            int columnWidth = totalWidth / columnCount;

            foreach (DataGridViewColumn column in dataGridQuarantineFiles.Columns)
            {

                switch (column.Name)
                {
                    case "Delete":
                    case "Restore":
                        column.Width = 150;
                        column.MinimumWidth = 150;
                        break;
                    case "PathColumn":
                        column.Width += 210;
                        column.MinimumWidth += 210;
                        break;
                    default:
                        column.Width = columnWidth;
                        column.MinimumWidth = columnWidth;
                        break;
                }

            }
        }

        List<QuarantineItem> LoadQuarantineItems(string quarantineKeyPath)
        {
            var items = new List<QuarantineItem>();

            using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath))
            {
                if (baseKey == null)
                    return items;

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
                                FileSize = Utils.Sizer(fileData.Length),
                                FileHash = subKeyName,
                            });
                        }
                    }
                }
            }

            return items;
        }

        private void dataGridThreats_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            foreach (DataGridViewRow row in dataGridQuarantineFiles.Rows)
            {
                row.Cells[0].Style.Font = new Font("Verdana", 10.2F, FontStyle.Bold);
                row.Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[row.Cells.Count - 1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        void UpdateQuarantineCount(string quarantineKeyPath)
        {
            int quarantineCount = 0;

            using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath))
            {
                if (baseKey != null)
                {
                    quarantineCount = baseKey.SubKeyCount;
                }
            }

            LBL_QFilesCount.Text = quarantineCount.ToString();

            dataGridQuarantineFiles.ClearSelection();
        }

        void dataGridQuarantineFiles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string registryPath = @"Software\M1nerSearch\Quarantine";
            string registryPathMain = @"Software\M1nerSearch";

            dataGridQuarantineFiles.ClearSelection();

            if (e.RowIndex < 0) return; // Ignore headers click

            if (UnlockObjectClass.IsRegistryKeyBlocked(registryPathMain))
            {
                UnlockObjectClass.UnblockRegistry(registryPathMain);
            }

            // Restore
            if (e.ColumnIndex == 0)
            {
                var fileHash = dataGridQuarantineFiles.Rows[e.RowIndex].Cells[4].Value.ToString();
                var originalPath = dataGridQuarantineFiles.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (RestoreFileFromQuarantine(registryPath, fileHash, originalPath))
                {
                    dataGridQuarantineFiles.Rows.RemoveAt(e.RowIndex);
                    UpdateQuarantineCount(registryPath);
                    MessageBox.Show($"{Program.LL.GetLocalizedString("_RestoredFile")} {originalPath.Replace(@"\\?\", "")}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Delete
            if (e.ColumnIndex == 1)
            {
                var fileHash = dataGridQuarantineFiles.Rows[e.RowIndex].Cells[4].Value.ToString(); 

                if (MessageBox.Show(Program.LL.GetLocalizedString("_DataGridRemoveBtn"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteFileFromQuarantine(registryPath, fileHash);
                    dataGridQuarantineFiles.Rows.RemoveAt(e.RowIndex);
                    UpdateQuarantineCount(registryPath);
                }
            }


        }

        void DeleteFileFromQuarantine(string quarantineKeyPath, string fileHash)
        {
            try
            {
                using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey?.OpenSubKey(fileHash) != null)
                    {
                        baseKey.DeleteSubKey(fileHash);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex, fileHash);
            }
        }

        private bool RestoreFileFromQuarantine(string quarantineKeyPath, string fileHash, string restorePath)
        {
            try
            {
                using (var baseKey = Registry.CurrentUser.OpenSubKey(quarantineKeyPath, true))
                {
                    if (baseKey == null)
                        throw new InvalidOperationException("Quarantine is empty");

                    using (var fileKey = baseKey.OpenSubKey(fileHash))
                    {
                        if (fileKey == null)
                            throw new InvalidOperationException("File not found in quarantine");

                        var encryptedData = fileKey.GetValue("FileData") as byte[];
                        if (encryptedData == null)
                            throw new InvalidOperationException("File data is absent");

                        byte[] decryptedData = DecryptData(encryptedData, Encoding.UTF8.GetBytes(fileHash.Remove(8)));

                        Directory.CreateDirectory(Path.GetDirectoryName(restorePath));
                        File.WriteAllBytes(restorePath, decryptedData);

                        baseKey.DeleteSubKey(fileHash);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Program.LL.LogErrorMessage("_Error", ex, fileHash);
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
    }
}
