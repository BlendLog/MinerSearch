using MSearch.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MSearch
{
    public partial class HostsDeletionForm : FormShadow
    {

        List<string> linesToDelete;
        List<string> selectedLinesToDelete = new List<string>();

        public HostsDeletionForm(List<string> linesToDelete)
        {
            InitializeComponent();
            this.linesToDelete = linesToDelete;

            foreach (var line in linesToDelete)
            {
                checkedListBox1.Items.Add(line, true);
            }

            TranslateForm();
        }

        private void TranslateForm()
        {
            label_message.Text = AppConfig.Instance.LL.GetLocalizedString("_HostsCleanupWarning");
            selectAllButton.Text = AppConfig.Instance.LL.GetLocalizedString("_SelectAllButton");
            deselectAllButton.Text = AppConfig.Instance.LL.GetLocalizedString("_DeselectAllButton");
            continueButton.Text = AppConfig.Instance.LL.GetLocalizedString("_ContinueButton");
            skipBtn.Text = AppConfig.Instance.LL.GetLocalizedString("_HostsCleanupSkipBtn");
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

        private void deselectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            selectedLinesToDelete = checkedListBox1.CheckedItems.OfType<string>().ToList();

            DialogResult = DialogResult.OK;
            Close();
        }

        public List<string> GetSelectedLinesToDelete()
        {
            return selectedLinesToDelete;
        }

        private void top_MouseDown(object sender, MouseEventArgs e)
        {
            top.Capture = false;
            Message m = Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deselectAllButton.PerformClick();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            var index = checkedListBox1.IndexFromPoint(checkedListBox1.PointToClient(Cursor.Position));

            if (index != ListBox.NoMatches)
            {
                bool currentCheckState = checkedListBox1.GetItemChecked(index);
                checkedListBox1.SetItemChecked(index, !currentCheckState);
            }
        }

        private void skipBtn_Click(object sender, EventArgs e)
        {
            closeBtn.PerformClick();
        }

        private void HostsDeletionForm_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.silent)
            {
                selectAllButton.PerformClick();
                continueButton.PerformClick();
            }
        }
    }
}
