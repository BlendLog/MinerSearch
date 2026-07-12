
namespace MSearch.UI
{
    partial class FormThreatReview
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridReviewThreats = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseBtn = new MSearch.UI.RoundButton();
            this.top = new System.Windows.Forms.Label();
            this.panelBulkAction = new System.Windows.Forms.Panel();
            this.bulkApplyBtn = new MSearch.UI.RoundButton();
            this.bulkActionComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new MSearch.UI.RoundButton();
            this.btnApply = new MSearch.UI.RoundButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReviewThreats)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelBulkAction.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridReviewThreats, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelBulkAction, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.63636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1100, 515);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridReviewThreats
            // 
            this.dataGridReviewThreats.AllowUserToAddRows = false;
            this.dataGridReviewThreats.AllowUserToDeleteRows = false;
            this.dataGridReviewThreats.AllowUserToResizeRows = false;
            this.dataGridReviewThreats.BackgroundColor = System.Drawing.Color.White;
            this.dataGridReviewThreats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReviewThreats.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReviewThreats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridReviewThreats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridReviewThreats.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridReviewThreats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridReviewThreats.GridColor = System.Drawing.Color.LightGray;
            this.dataGridReviewThreats.Location = new System.Drawing.Point(1, 104);
            this.dataGridReviewThreats.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.dataGridReviewThreats.MultiSelect = false;
            this.dataGridReviewThreats.Name = "dataGridReviewThreats";
            this.dataGridReviewThreats.ReadOnly = true;
            this.dataGridReviewThreats.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridReviewThreats.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridReviewThreats.RowHeadersWidth = 51;
            this.dataGridReviewThreats.RowTemplate.Height = 24;
            this.dataGridReviewThreats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridReviewThreats.Size = new System.Drawing.Size(1098, 363);
            this.dataGridReviewThreats.TabIndex = 4;
            this.dataGridReviewThreats.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridReviewThreats_CellValueChanged);
            this.dataGridReviewThreats.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridReviewThreats_CurrentCellDirtyStateChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 56);
            this.panel1.TabIndex = 5;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.CloseBtn.CornerRadius = 4;
            this.CloseBtn.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(1069, 9);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(22, 24);
            this.CloseBtn.TabIndex = 100;
            this.CloseBtn.Text = "X";
            this.CloseBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.RoyalBlue;
            this.top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.top.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.top.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.top.ForeColor = System.Drawing.Color.White;
            this.top.Location = new System.Drawing.Point(0, 0);
            this.top.Margin = new System.Windows.Forms.Padding(0);
            this.top.Name = "top";
            this.top.Padding = new System.Windows.Forms.Padding(15, 0, 30, 0);
            this.top.Size = new System.Drawing.Size(1100, 56);
            this.top.TabIndex = 2;
            this.top.Text = "_title";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelBulkAction
            // 
            this.panelBulkAction.BackColor = System.Drawing.SystemColors.Control;
            this.panelBulkAction.Controls.Add(this.bulkApplyBtn);
            this.panelBulkAction.Controls.Add(this.bulkActionComboBox);
            this.panelBulkAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBulkAction.Location = new System.Drawing.Point(1, 56);
            this.panelBulkAction.Margin = new System.Windows.Forms.Padding(1, 0, 1, 3);
            this.panelBulkAction.Name = "panelBulkAction";
            this.panelBulkAction.Size = new System.Drawing.Size(1098, 45);
            this.panelBulkAction.TabIndex = 7;
            // 
            // bulkApplyBtn
            // 
            this.bulkApplyBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.bulkApplyBtn.CornerRadius = 4;
            this.bulkApplyBtn.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.bulkApplyBtn.FlatAppearance.BorderSize = 0;
            this.bulkApplyBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.bulkApplyBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bulkApplyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bulkApplyBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bulkApplyBtn.ForeColor = System.Drawing.Color.White;
            this.bulkApplyBtn.Location = new System.Drawing.Point(890, 8);
            this.bulkApplyBtn.Name = "bulkApplyBtn";
            this.bulkApplyBtn.Size = new System.Drawing.Size(200, 32);
            this.bulkApplyBtn.TabIndex = 2;
            this.bulkApplyBtn.Text = "_ReviewBulkApplyBtn";
            this.bulkApplyBtn.UseVisualStyleBackColor = false;
            // 
            // bulkActionComboBox
            // 
            this.bulkActionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bulkActionComboBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.bulkActionComboBox.FormattingEnabled = true;
            this.bulkActionComboBox.Location = new System.Drawing.Point(695, 13);
            this.bulkActionComboBox.Name = "bulkActionComboBox";
            this.bulkActionComboBox.Size = new System.Drawing.Size(189, 25);
            this.bulkActionComboBox.Sorted = true;
            this.bulkActionComboBox.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnApply);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 467);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1100, 48);
            this.panel2.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.CornerRadius = 4;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(793, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "_CancelBtn";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.AutoSize = true;
            this.btnApply.BackColor = System.Drawing.Color.White;
            this.btnApply.CornerRadius = 4;
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnApply.Location = new System.Drawing.Point(940, 8);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(141, 36);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "_ApplyBtn";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            //
            // btnCancel
            //
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // bulkApplyBtn
            //
            this.bulkApplyBtn.Click += new System.EventHandler(this.bulkApplyBtn_Click);
            //
            // top
            //
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            //
            // FormThreatReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 515);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormThreatReview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormThreatReview";
            this.Load += new System.EventHandler(this.FormThreatReview_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormThreatReview_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReviewThreats)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelBulkAction.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.DataGridView dataGridReviewThreats;
        private System.Windows.Forms.Panel panel1;
        private RoundButton CloseBtn;
        private System.Windows.Forms.Label top;
        private System.Windows.Forms.Panel panelBulkAction;
        private System.Windows.Forms.ComboBox bulkActionComboBox;
        private RoundButton bulkApplyBtn;
        private System.Windows.Forms.Panel panel2;
        private RoundButton btnApply;
        private RoundButton btnCancel;
    }
}