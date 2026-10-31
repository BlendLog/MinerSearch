using Microsoft.Win32;
using MSearch.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSearch.UI
{
    /// <summary>
    /// Страница карантина: список объектов, восстановление и удаление.
    /// </summary>
    public partial class QuarantinePage : UserControl
    {
        public event EventHandler RefreshRequested;

        private const int MaxSummaryPaths = 10;
        private const string ColSelect = "colSelect";
        private const string ColPath = "colPath";
        private const string ColSize = "colSize";
        private const string ColType = "colType";
        private const string ColHash = "colHash";

        private readonly List<QuarantineItem> _items = new List<QuarantineItem>();
        private bool _registryUnblocked;
        private bool _loading;
        private bool _busy;
        private bool _scanRunning;

        private sealed class QuarantineOperationSummary
        {
            public List<QuarantineItem> CompletedItems { get; private set; }
            public List<string> AffectedPaths { get; private set; }
            public int FailedCount { get; set; }

            public QuarantineOperationSummary()
            {
                CompletedItems = new List<QuarantineItem>();
                AffectedPaths = new List<string>();
            }
        }

        public bool IsBusy
        {
            get { return _busy; }
        }

        public QuarantinePage()
        {
            InitializeComponent();
            ConfigureGrid();

            if (!IsInDesignMode)
            {
                ApplyLanguage();
                UpdateUi();
            }
        }

        private static bool IsInDesignMode
        {
            get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
        }

        private void ConfigureGrid()
        {
            dataGridQuarantine.Columns.Clear();

            DataGridViewCheckBoxColumn select = new DataGridViewCheckBoxColumn
            {
                Name = ColSelect,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 90,
                Resizable = DataGridViewTriState.False,
                Frozen = true
            };

            DataGridViewCheckBoxHeaderCell header = new DataGridViewCheckBoxHeaderCell();
            header.CheckBoxClicked += Header_CheckBoxClicked;
            select.HeaderCell = header;
            dataGridQuarantine.Columns.Add(select);

            dataGridQuarantine.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColPath,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 42F,
                MinimumWidth = 500,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            DataGridViewColumn quarantinePathColumn = dataGridQuarantine.Columns[ColPath];
            quarantinePathColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            quarantinePathColumn.Width = 500;
            quarantinePathColumn.MinimumWidth = 300;
            quarantinePathColumn.Resizable = DataGridViewTriState.True;

            dataGridQuarantine.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColSize,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 10F,
                MinimumWidth = 100,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dataGridQuarantine.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColType,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 16F,
                MinimumWidth = 130,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dataGridQuarantine.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = ColHash,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 28F,
                MinimumWidth = 180,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dataGridQuarantine.ScrollBars = ScrollBars.Both;

            dataGridQuarantine.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 251),
                ForeColor = Theme.Muted,
                Font = Theme.BodyBold,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                WrapMode = DataGridViewTriState.True
            };

            dataGridQuarantine.DefaultCellStyle.Font = Theme.Body;
            dataGridQuarantine.DefaultCellStyle.BackColor = Color.White;
            dataGridQuarantine.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            dataGridQuarantine.DefaultCellStyle.SelectionForeColor = Theme.Text;
        }

        private void panelHost_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Theme.Border))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panelHost.Width - 1, panelHost.Height - 1);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            EventHandler handler = RefreshRequested;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Перечитывает список карантина из реестра.
        /// </summary>
        public async Task RefreshItemsAsync()
        {
            if (_busy || _scanRunning)
                return;

            Exception refreshError = null;
            SetBusy(true);
            try
            {
                List<QuarantineItem> items = await Task.Run(delegate
                {
                    EnsureRegistryAccessible();
                    return QuarantineManager.List();
                });

                _loading = true;
                try
                {
                    dataGridQuarantine.Rows.Clear();
                    _items.Clear();

                    foreach (QuarantineItem item in items)
                    {
                        if (item == null)
                            continue;

                        _items.Add(item);

                        int index = dataGridQuarantine.Rows.Add(
                            false,
                            item.OriginalPath,
                            item.FileSize,
                            GetTypeLabel(item.ItemType),
                            item.FileHash);

                        dataGridQuarantine.Rows[index].Tag = item;
                    }
                }
                finally
                {
                    _loading = false;
                }

                dataGridQuarantine.ClearSelection();
                UpdateHeaderState();
            }
            catch (Exception ex)
            {
                refreshError = ex;
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
            finally
            {
                SetBusy(false);
            }

            if (refreshError != null)
            {
                MessageBoxCustom.Show(
                    L("_Error") + "\n\n" + refreshError.Message,
                    L("_Quarantine"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void SetScanRunning(bool running)
        {
            _scanRunning = running;
            dataGridQuarantine.Enabled = !_busy && !_scanRunning;
            buttonRefresh.Enabled = !_busy && !_scanRunning;
            UpdateUi();
        }

        private void SetBusy(bool busy)
        {
            _busy = busy;
            UseWaitCursor = busy;
            dataGridQuarantine.Enabled = !busy && !_scanRunning;
            buttonRefresh.Enabled = !busy && !_scanRunning;
            UpdateUi();
        }

        private void EnsureRegistryAccessible()
        {
            if (_registryUnblocked)
                return;

            const string mainKey = @"Software\M1nerSearch";

            try
            {
                if (UnlockObjectClass.IsRegistryKeyBlocked(mainKey))
                    UnlockObjectClass.UnblockRegistry(mainKey);

                if (UnlockObjectClass.IsRegistryKeyBlocked(mainKey, RegistryHive.LocalMachine))
                    UnlockObjectClass.UnblockRegistry(mainKey, RegistryHive.LocalMachine);
            }
            catch { }

            _registryUnblocked = true;
        }

        private void UpdateUi()
        {
            int count = _items.Count;
            labelCount.Text = _busy
                ? L("_PleaseWaitMessage")
                : L("_QuarantineCount").Replace("#COUNT#", count.ToString());
            labelEmpty.Visible = count == 0;

            int checkedCount = GetCheckedItems().Count;
            bool canMutate = !_busy && !_scanRunning;
            buttonRestore.Enabled = canMutate && checkedCount > 0;
            buttonDelete.Enabled = canMutate && checkedCount > 0;
        }

        private List<QuarantineItem> GetCheckedItems()
        {
            List<QuarantineItem> result = new List<QuarantineItem>();

            foreach (DataGridViewRow row in dataGridQuarantine.Rows)
            {
                if (row.IsNewRow)
                    continue;

                bool isChecked = row.Cells[ColSelect].Value is bool && (bool)row.Cells[ColSelect].Value;
                if (!isChecked)
                    continue;

                QuarantineItem item = row.Tag as QuarantineItem;
                if (item != null)
                    result.Add(item);
            }

            return result;
        }

        private void dataGridQuarantine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0)
                return;

            if (e.ColumnIndex != dataGridQuarantine.Columns[ColSelect].Index)
                return;

            DataGridViewCell cell = dataGridQuarantine.Rows[e.RowIndex].Cells[ColSelect];
            bool current = cell.Value is bool && (bool)cell.Value;
            cell.Value = !current;

            UpdateHeaderState();
            UpdateUi();
        }

        private void Header_CheckBoxClicked(object sender, EventArgs e)
        {
            DataGridViewCheckBoxHeaderCell header = sender as DataGridViewCheckBoxHeaderCell;
            if (header == null)
                return;

            bool selectAll = header.CheckState == CheckState.Checked;

            foreach (DataGridViewRow row in dataGridQuarantine.Rows)
                row.Cells[ColSelect].Value = selectAll;

            UpdateUi();
        }

        private void UpdateHeaderState()
        {
            DataGridViewCheckBoxHeaderCell header = dataGridQuarantine.Columns[ColSelect].HeaderCell as DataGridViewCheckBoxHeaderCell;
            if (header == null)
                return;

            int total = dataGridQuarantine.Rows.Count;
            int checkedCount = GetCheckedItems().Count;

            if (checkedCount == 0)
                header.CheckState = CheckState.Unchecked;
            else if (checkedCount == total)
                header.CheckState = CheckState.Checked;
            else
                header.CheckState = CheckState.Indeterminate;

            dataGridQuarantine.InvalidateCell(header);
        }

        private async void buttonRestore_Click(object sender, EventArgs e)
        {
            if (_busy || _scanRunning)
                return;

            List<QuarantineItem> selected = GetCheckedItems();
            if (selected.Count == 0)
            {
                MessageBoxCustom.Show(
                    L("_DataGrid_NoSelection"),
                    L("_Quarantine"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool hasNonFile = false;
            foreach (QuarantineItem item in selected)
            {
                if (item.ItemType != QuarantineItemType.File)
                {
                    hasNonFile = true;
                    break;
                }
            }

            string customFolder = null;

            if (!hasNonFile)
            {
                DialogResult choice = MessageBoxCustom.Show(
                    L("_QuarantineRestorePrompt"),
                    L("_RestoreFormTitle"),
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (choice == DialogResult.Cancel)
                    return;

                if (choice == DialogResult.No)
                {
                    using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                    {
                        dialog.Description = L("_RestoreFolderDialog");
                        dialog.ShowNewFolderButton = true;

                        if (dialog.ShowDialog(this) != DialogResult.OK)
                            return;

                        customFolder = dialog.SelectedPath;
                    }
                }
            }

            await RunQuarantineOperationAsync(
                delegate { return RestoreSelectedItems(selected, customFolder); },
                "_QuarantineRestoredFile");
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_busy || _scanRunning)
                return;

            List<QuarantineItem> selected = GetCheckedItems();
            if (selected.Count == 0)
            {
                MessageBoxCustom.Show(
                    L("_DataGrid_NoSelection"),
                    L("_Quarantine"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBoxCustom.Show(
                L("_QuarantineRemoveBtn").Replace("#FILESCOUNT#", selected.Count.ToString()),
                L("_Quarantine"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            await RunQuarantineOperationAsync(
                delegate { return DeleteSelectedItems(selected); },
                "_QuarantineRemovedFiles");
        }

        private async Task RunQuarantineOperationAsync(Func<QuarantineOperationSummary> operation, string messageKey)
        {
            QuarantineOperationSummary summary = null;
            Exception operationError = null;

            SetBusy(true);
            try
            {
                summary = await Task.Run(operation);
                RemoveCompletedItems(summary.CompletedItems);
            }
            catch (Exception ex)
            {
                operationError = ex;
                AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex);
            }
            finally
            {
                SetBusy(false);
            }

            if (operationError != null)
            {
                MessageBoxCustom.Show(
                    L("_Error") + "\n\n" + operationError.Message,
                    L("_Quarantine"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (summary != null && (summary.AffectedPaths.Count > 0 || summary.FailedCount > 0))
                ShowOperationSummary(messageKey, summary.AffectedPaths, summary.FailedCount);
        }

        private static QuarantineOperationSummary RestoreSelectedItems(List<QuarantineItem> selected, string customFolder)
        {
            QuarantineOperationSummary summary = new QuarantineOperationSummary();

            foreach (QuarantineItem item in selected)
            {
                if (item == null)
                    continue;

                string restoredPath = item.OriginalPath;
                bool restored;

                try
                {
                    switch (item.ItemType)
                    {
                        case QuarantineItemType.Service:
                            restored = QuarantineManager.RestoreService(item.FileHash);
                            break;
                        case QuarantineItemType.Task:
                            restored = QuarantineManager.RestoreTask(item.FileHash);
                            break;
                        case QuarantineItemType.Registry:
                            restored = QuarantineManager.RestoreRegistry(item.FileHash);
                            break;
                        default:
                            string targetPath = customFolder == null
                                ? item.OriginalPath
                                : Path.Combine(customFolder, Path.GetFileName(item.OriginalPath));

                            targetPath = ResolveFileNameCollision(targetPath, item.FileHash);
                            restored = QuarantineManager.RestoreFile(item.FileHash, targetPath);
                            restoredPath = targetPath;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, item.OriginalPath);
                    summary.FailedCount++;
                    continue;
                }

                if (!restored)
                {
                    summary.FailedCount++;
                    continue;
                }

                summary.CompletedItems.Add(item);
                summary.AffectedPaths.Add(restoredPath);
            }

            return summary;
        }

        private static QuarantineOperationSummary DeleteSelectedItems(List<QuarantineItem> selected)
        {
            QuarantineOperationSummary summary = new QuarantineOperationSummary();

            foreach (QuarantineItem item in selected)
            {
                if (item == null)
                    continue;

                bool deleted;
                try
                {
                    deleted = QuarantineManager.Delete(item.FileHash);
                }
                catch (Exception ex)
                {
                    AppConfig.GetInstance.LL.LogErrorMessage("_Error", ex, item.OriginalPath);
                    summary.FailedCount++;
                    continue;
                }

                if (!deleted)
                {
                    summary.FailedCount++;
                    continue;
                }

                summary.CompletedItems.Add(item);
                summary.AffectedPaths.Add(item.OriginalPath);
            }

            return summary;
        }

        private void RemoveCompletedItems(List<QuarantineItem> completedItems)
        {
            if (completedItems == null || completedItems.Count == 0)
                return;

            HashSet<QuarantineItem> completed = new HashSet<QuarantineItem>(completedItems);
            for (int i = dataGridQuarantine.Rows.Count - 1; i >= 0; i--)
            {
                QuarantineItem item = dataGridQuarantine.Rows[i].Tag as QuarantineItem;
                if (item != null && completed.Contains(item))
                {
                    dataGridQuarantine.Rows.RemoveAt(i);
                    _items.Remove(item);
                }
            }

            dataGridQuarantine.ClearSelection();
            UpdateHeaderState();
            UpdateUi();
        }

        private static string ResolveFileNameCollision(string targetPath, string quarantineHash)
        {
            if (!File.Exists(targetPath))
                return targetPath;

            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(targetPath))
            {
                byte[] hash = md5.ComputeHash(stream);
                string existingHash = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                if (existingHash == quarantineHash)
                    return targetPath;
            }

            string directory = Path.GetDirectoryName(targetPath);
            string fileName = Path.GetFileNameWithoutExtension(targetPath);
            string extension = Path.GetExtension(targetPath);
            string collisionName = fileName + "_" + quarantineHash + extension;
            string collisionPath = Path.Combine(directory, collisionName);

            int counter = 1;
            while (File.Exists(collisionPath))
            {
                collisionName = fileName + "_" + quarantineHash + "_" + counter + extension;
                collisionPath = Path.Combine(directory, collisionName);
                counter++;
            }

            return collisionPath;
        }

        private void ShowOperationSummary(string messageKey, List<string> affectedFiles, int failedCount = 0)
        {
            if (affectedFiles == null)
                affectedFiles = new List<string>();

            if (affectedFiles.Count == 0)
            {
                MessageBoxCustom.Show(
                    L("_QuarantineOperationFailed").Replace("#COUNT#", failedCount.ToString()),
                    L("_Quarantine"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            StringBuilder text = new StringBuilder(
                L(messageKey).Replace("#FILESCOUNT#", affectedFiles.Count.ToString()) + "\n");

            int shown = Math.Min(affectedFiles.Count, MaxSummaryPaths);
            for (int i = 0; i < shown; i++)
                text.Append("\n" + affectedFiles[i]);

            if (affectedFiles.Count > shown)
            {
                text.Append("\n" + L("_AndMoreItems")
                    .Replace("#COUNT#", (affectedFiles.Count - shown).ToString()));
            }

            if (failedCount > 0)
            {
                text.Append("\n\n" + L("_QuarantineOperationFailed")
                    .Replace("#COUNT#", failedCount.ToString()));
            }

            MessageBoxCustom.Show(
                text.ToString(),
                L("_Quarantine"),
                MessageBoxButtons.OK,
                failedCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }

        private string GetTypeLabel(QuarantineItemType type)
        {
            switch (type)
            {
                case QuarantineItemType.Service:
                    return L("_ObjectType_Service");
                case QuarantineItemType.Task:
                    return L("_ObjectType_ScheduledTask");
                case QuarantineItemType.Registry:
                    return L("_ObjectType_RegistryObject");
                default:
                    return L("_ObjectType_File");
            }
        }

        public void ApplyLanguage()
        {
            labelTitle.Text = L("_Quarantine");
            buttonRefresh.Text = L("_Refresh");
            buttonRestore.Text = L("_Action_Restore");
            buttonDelete.Text = L("_Action_Delete");
            labelEmpty.Text = L("_QuarantineEmpty");

            if (dataGridQuarantine.Columns.Count > 0)
            {
                dataGridQuarantine.Columns[ColPath].HeaderText = L("_DataGridHeader_OriginalPath");
                dataGridQuarantine.Columns[ColSize].HeaderText = L("_DataGridHeader_FileSize");
                dataGridQuarantine.Columns[ColType].HeaderText = L("_DataGridHeader_ObjectType");
                dataGridQuarantine.Columns[ColHash].HeaderText = L("_DataGridHeader_FileHash");

                DataGridViewCheckBoxHeaderCell header = dataGridQuarantine.Columns[ColSelect].HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                    header.Value = L("_DataGridHeader_Select");
            }

            foreach (DataGridViewRow row in dataGridQuarantine.Rows)
            {
                QuarantineItem item = row.Tag as QuarantineItem;
                if (item != null)
                    row.Cells[ColType].Value = GetTypeLabel(item.ItemType);
            }

            UpdateUi();
        }

        private static string L(string key)
        {
            return AppConfig.GetInstance.LL.GetLocalizedString(key);
        }
    }
}
