namespace MSearch
{
    partial class QuarantineRestoreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseBtn = new MSearch.UI.RoundButton();
            this.top = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbOriginalPath = new System.Windows.Forms.RadioButton();
            this.lblOriginalDesc = new System.Windows.Forms.Label();
            this.rbCustomPath = new System.Windows.Forms.RadioButton();
            this.lblCustomDesc = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtCustomPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new MSearch.UI.RoundButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancel = new MSearch.UI.RoundButton();
            this.btnRestore = new MSearch.UI.RoundButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 269);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 26);
            this.panel1.TabIndex = 0;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.CloseBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(559, 3);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(24, 23);
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
            this.top.Name = "top";
            this.top.Padding = new System.Windows.Forms.Padding(12, 0, 30, 0);
            this.top.Size = new System.Drawing.Size(589, 26);
            this.top.TabIndex = 2;
            this.top.Text = "_RestoreFormTitle";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 33);
            this.panel2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 174);
            this.panel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.rbOriginalPath);
            this.flowLayoutPanel1.Controls.Add(this.lblOriginalDesc);
            this.flowLayoutPanel1.Controls.Add(this.rbCustomPath);
            this.flowLayoutPanel1.Controls.Add(this.lblCustomDesc);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(7, 16);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(578, 177);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // rbOriginalPath
            // 
            this.rbOriginalPath.AutoSize = true;
            this.rbOriginalPath.Checked = true;
            this.rbOriginalPath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbOriginalPath.Location = new System.Drawing.Point(8, 8);
            this.rbOriginalPath.Name = "rbOriginalPath";
            this.rbOriginalPath.Size = new System.Drawing.Size(214, 23);
            this.rbOriginalPath.TabIndex = 0;
            this.rbOriginalPath.TabStop = true;
            this.rbOriginalPath.Text = "_RestoreOptionOriginalPath";
            this.rbOriginalPath.UseVisualStyleBackColor = true;
            this.rbOriginalPath.CheckedChanged += new System.EventHandler(this.rbOriginalPath_CheckedChanged);
            // 
            // lblOriginalDesc
            // 
            this.lblOriginalDesc.AutoSize = true;
            this.lblOriginalDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblOriginalDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblOriginalDesc.Location = new System.Drawing.Point(8, 34);
            this.lblOriginalDesc.Name = "lblOriginalDesc";
            this.lblOriginalDesc.Size = new System.Drawing.Size(154, 13);
            this.lblOriginalDesc.TabIndex = 1;
            this.lblOriginalDesc.Text = "_RestoreOptionOriginalDesc";
            // 
            // rbCustomPath
            // 
            this.rbCustomPath.AutoSize = true;
            this.rbCustomPath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbCustomPath.Location = new System.Drawing.Point(8, 50);
            this.rbCustomPath.Name = "rbCustomPath";
            this.rbCustomPath.Size = new System.Drawing.Size(210, 23);
            this.rbCustomPath.TabIndex = 2;
            this.rbCustomPath.Text = "_RestoreOptionCustomPath";
            this.rbCustomPath.UseVisualStyleBackColor = true;
            this.rbCustomPath.CheckedChanged += new System.EventHandler(this.rbCustomPath_CheckedChanged);
            // 
            // lblCustomDesc
            // 
            this.lblCustomDesc.AutoSize = true;
            this.lblCustomDesc.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCustomDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblCustomDesc.Location = new System.Drawing.Point(8, 76);
            this.lblCustomDesc.Name = "lblCustomDesc";
            this.lblCustomDesc.Size = new System.Drawing.Size(151, 13);
            this.lblCustomDesc.TabIndex = 4;
            this.lblCustomDesc.Text = "_RestoreOptionCustomDesc";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.txtCustomPath);
            this.flowLayoutPanel2.Controls.Add(this.btnBrowse);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(8, 92);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(567, 56);
            this.flowLayoutPanel2.TabIndex = 3;
            this.flowLayoutPanel2.Visible = false;
            // 
            // txtCustomPath
            // 
            this.txtCustomPath.BackColor = System.Drawing.Color.White;
            this.txtCustomPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCustomPath.Location = new System.Drawing.Point(8, 8);
            this.txtCustomPath.Name = "txtCustomPath";
            this.txtCustomPath.ReadOnly = true;
            this.txtCustomPath.Size = new System.Drawing.Size(415, 23);
            this.txtCustomPath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.Location = new System.Drawing.Point(429, 8);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(107, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "_BrowseButton";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnRestore);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.panel3.Location = new System.Drawing.Point(1, 208);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(593, 60);
            this.panel3.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(297, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "_CancelBtn";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.AutoSize = true;
            this.btnRestore.BackColor = System.Drawing.Color.White;
            this.btnRestore.FlatAppearance.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.btnRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRestore.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnRestore.Location = new System.Drawing.Point(444, 13);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(141, 36);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "_RestoreBtn";
            this.btnRestore.UseVisualStyleBackColor = false;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // QuarantineRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(595, 269);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "QuarantineRestoreForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.QuarantineRestoreForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label top;
        private MSearch.UI.RoundButton CloseBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbOriginalPath;
        private System.Windows.Forms.Label lblOriginalDesc;
        public System.Windows.Forms.RadioButton rbCustomPath;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox txtCustomPath;
        private MSearch.UI.RoundButton btnBrowse;
        private System.Windows.Forms.Label lblCustomDesc;
        private MSearch.UI.RoundButton btnRestore;
        private MSearch.UI.RoundButton btnCancel;
    }
}
