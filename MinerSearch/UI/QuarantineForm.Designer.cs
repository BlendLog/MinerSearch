
namespace MSearch
{
    partial class QuarantineForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridQuarantineFiles = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.finishBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.LBL_Quarantine = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pb_quarantine = new System.Windows.Forms.PictureBox();
            this.LBL_QFilesCount = new System.Windows.Forms.Label();
            this.LBL_QuarantinedFiles = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuarantineFiles)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_quarantine)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridQuarantineFiles, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1260, 535);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dataGridQuarantineFiles
            // 
            this.dataGridQuarantineFiles.AllowUserToAddRows = false;
            this.dataGridQuarantineFiles.AllowUserToDeleteRows = false;
            this.dataGridQuarantineFiles.AllowUserToResizeRows = false;
            this.dataGridQuarantineFiles.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridQuarantineFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridQuarantineFiles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridQuarantineFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridQuarantineFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridQuarantineFiles.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridQuarantineFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridQuarantineFiles.GridColor = System.Drawing.Color.LightGray;
            this.dataGridQuarantineFiles.Location = new System.Drawing.Point(0, 159);
            this.dataGridQuarantineFiles.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridQuarantineFiles.MultiSelect = false;
            this.dataGridQuarantineFiles.Name = "dataGridQuarantineFiles";
            this.dataGridQuarantineFiles.ReadOnly = true;
            this.dataGridQuarantineFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridQuarantineFiles.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridQuarantineFiles.RowHeadersWidth = 51;
            this.dataGridQuarantineFiles.RowTemplate.Height = 24;
            this.dataGridQuarantineFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridQuarantineFiles.Size = new System.Drawing.Size(1260, 316);
            this.dataGridQuarantineFiles.TabIndex = 1;
            this.dataGridQuarantineFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridQuarantineFiles_CellClick);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.finishBtn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.panel4.Location = new System.Drawing.Point(0, 475);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1260, 60);
            this.panel4.TabIndex = 3;
            // 
            // finishBtn
            // 
            this.finishBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.finishBtn.BackColor = System.Drawing.Color.White;
            this.finishBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.finishBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.finishBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.finishBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.finishBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.finishBtn.ForeColor = System.Drawing.Color.Black;
            this.finishBtn.Location = new System.Drawing.Point(539, 8);
            this.finishBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.finishBtn.Name = "finishBtn";
            this.finishBtn.Size = new System.Drawing.Size(220, 44);
            this.finishBtn.TabIndex = 11;
            this.finishBtn.Text = "exitBtn";
            this.finishBtn.UseVisualStyleBackColor = false;
            this.finishBtn.Click += new System.EventHandler(this.FinishBtn_click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1260, 119);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.99335F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.00665F));
            this.tableLayoutPanel2.Controls.Add(this.LBL_Quarantine, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1258, 117);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // LBL_Quarantine
            // 
            this.LBL_Quarantine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(85)))), ((int)(((byte)(205)))));
            this.LBL_Quarantine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LBL_Quarantine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_Quarantine.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Bold);
            this.LBL_Quarantine.ForeColor = System.Drawing.Color.White;
            this.LBL_Quarantine.Location = new System.Drawing.Point(0, 0);
            this.LBL_Quarantine.Margin = new System.Windows.Forms.Padding(0);
            this.LBL_Quarantine.Name = "LBL_Quarantine";
            this.LBL_Quarantine.Size = new System.Drawing.Size(452, 117);
            this.LBL_Quarantine.TabIndex = 5;
            this.LBL_Quarantine.Text = "label_quarantine";
            this.LBL_Quarantine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(95)))), ((int)(((byte)(215)))));
            this.panel6.Controls.Add(this.pb_quarantine);
            this.panel6.Controls.Add(this.LBL_QFilesCount);
            this.panel6.Controls.Add(this.LBL_QuarantinedFiles);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(452, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(806, 117);
            this.panel6.TabIndex = 1;
            // 
            // pb_quarantine
            // 
            this.pb_quarantine.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pb_quarantine.BackColor = System.Drawing.Color.Transparent;
            this.pb_quarantine.Image = global::MSearch.Properties.Resources.quarantine_logo;
            this.pb_quarantine.Location = new System.Drawing.Point(662, -4);
            this.pb_quarantine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pb_quarantine.Name = "pb_quarantine";
            this.pb_quarantine.Size = new System.Drawing.Size(131, 119);
            this.pb_quarantine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_quarantine.TabIndex = 6;
            this.pb_quarantine.TabStop = false;
            // 
            // LBL_QFilesCount
            // 
            this.LBL_QFilesCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_QFilesCount.AutoSize = true;
            this.LBL_QFilesCount.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Bold);
            this.LBL_QFilesCount.ForeColor = System.Drawing.Color.White;
            this.LBL_QFilesCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LBL_QFilesCount.Location = new System.Drawing.Point(310, 44);
            this.LBL_QFilesCount.Name = "LBL_QFilesCount";
            this.LBL_QFilesCount.Size = new System.Drawing.Size(27, 29);
            this.LBL_QFilesCount.TabIndex = 8;
            this.LBL_QFilesCount.Text = "0";
            this.LBL_QFilesCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBL_QuarantinedFiles
            // 
            this.LBL_QuarantinedFiles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_QuarantinedFiles.AutoSize = true;
            this.LBL_QuarantinedFiles.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Bold);
            this.LBL_QuarantinedFiles.ForeColor = System.Drawing.Color.White;
            this.LBL_QuarantinedFiles.Location = new System.Drawing.Point(26, 44);
            this.LBL_QuarantinedFiles.Name = "LBL_QuarantinedFiles";
            this.LBL_QuarantinedFiles.Size = new System.Drawing.Size(256, 29);
            this.LBL_QuarantinedFiles.TabIndex = 6;
            this.LBL_QuarantinedFiles.Text = "label_quarantineFiles";
            this.LBL_QuarantinedFiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1260, 40);
            this.panel1.TabIndex = 0;
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.CloseBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.CloseBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.CloseBtn.Location = new System.Drawing.Point(1218, 9);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(29, 30);
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
            this.top.Padding = new System.Windows.Forms.Padding(8, 0, 40, 0);
            this.top.Size = new System.Drawing.Size(1260, 40);
            this.top.TabIndex = 2;
            this.top.Text = "_title";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            // 
            // QuarantineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1260, 535);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "QuarantineForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.QuarantineForm_Load_1);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuarantineFiles)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_quarantine)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label top;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button finishBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.DataGridView dataGridQuarantineFiles;
        private System.Windows.Forms.Label LBL_QuarantinedFiles;
        private System.Windows.Forms.Label LBL_QFilesCount;
        private System.Windows.Forms.PictureBox pb_quarantine;
        private System.Windows.Forms.Label LBL_Quarantine;
    }
}