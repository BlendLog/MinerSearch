using MSearch.Core;
using MSearch.UI;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MSearch
{
    public partial class QuarantineRestoreForm : FormShadow
    {
        public string SelectedCustomPath { get; private set; } = string.Empty;

        public QuarantineRestoreForm()
        {
            InitializeComponent();
            TranslateForm();
        }

        private void QuarantineRestoreForm_Load(object sender, EventArgs e)
        {
            rbOriginalPath.Checked = true;
        }

        private void TranslateForm()
        {
            top.Text = AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormTitle");
            rbOriginalPath.Text = AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionOriginalPath");
            lblOriginalDesc.Text = AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionOriginalDesc");
            rbCustomPath.Text = AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionCustomPath");
            lblCustomDesc.Text = AppConfig.GetInstance.LL.GetLocalizedString("_Q_CLI_RestoreOptionCustomDesc");
            btnBrowse.Text = AppConfig.GetInstance.LL.GetLocalizedString("_BrowseButton");
            btnRestore.Text = AppConfig.GetInstance.LL.GetLocalizedString("_RestoreBtn");
            btnCancel.Text = AppConfig.GetInstance.LL.GetLocalizedString("_CancelBtn");
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void rbOriginalPath_CheckedChanged(object sender, EventArgs e)
        {
            UpdateRestoreButtonState();
        }

        private void rbCustomPath_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanel2.Visible = rbCustomPath.Checked;
            UpdateRestoreButtonState();
        }

        private void UpdateRestoreButtonState()
        {
            if (rbOriginalPath.Checked)
            {
                btnRestore.Enabled = true;
                btnRestore.ForeColor = Color.RoyalBlue;
                btnRestore.FlatAppearance.BorderColor = Color.RoyalBlue;
            }
            else if (rbCustomPath.Checked)
            {
                bool valid = !string.IsNullOrWhiteSpace(txtCustomPath.Text) && Directory.Exists(txtCustomPath.Text);
                btnRestore.Enabled = valid;
                btnRestore.ForeColor = valid ? Color.RoyalBlue : Color.Gray;
                btnRestore.FlatAppearance.BorderColor = valid ? Color.RoyalBlue : Color.Gray;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFolderDialog");
                dialog.ShowNewFolderButton = false;
                dialog.RootFolder = Environment.SpecialFolder.MyComputer;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtCustomPath.Text = dialog.SelectedPath;
                    UpdateRestoreButtonState();
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (rbCustomPath.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtCustomPath.Text))
                {
                    DialogDispatcher.Show(
                        AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormPathRequired"),
                        AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(txtCustomPath.Text))
                {
                    DialogDispatcher.Show(
                        AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormPathInvalid"),
                        AppConfig.GetInstance.LL.GetLocalizedString("_RestoreFormTitle"),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                SelectedCustomPath = txtCustomPath.Text;
            }

            DialogResult = DialogResult.OK;
            Close();
        }


    }
}
