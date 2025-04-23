
namespace MSearch
{
    partial class HostsDeletionForm
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
            this.top = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.deselectAllButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.skipBtn = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_message = new System.Windows.Forms.Label();
            this.closeBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.RoyalBlue;
            this.top.Dock = System.Windows.Forms.DockStyle.Top;
            this.top.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.top.ForeColor = System.Drawing.Color.White;
            this.top.Location = new System.Drawing.Point(0, 0);
            this.top.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.top.Name = "top";
            this.top.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.top.Size = new System.Drawing.Size(494, 41);
            this.top.TabIndex = 4;
            this.top.Text = "MinerSearch - Hosts";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 626);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.64F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.6F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(494, 626);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.selectAllButton);
            this.panel3.Controls.Add(this.deselectAllButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(6, 86);
            this.panel3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(482, 53);
            this.panel3.TabIndex = 5;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectAllButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.selectAllButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.selectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selectAllButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.selectAllButton.ForeColor = System.Drawing.Color.White;
            this.selectAllButton.Location = new System.Drawing.Point(3, 9);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(242, 34);
            this.selectAllButton.TabIndex = 1;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = false;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // deselectAllButton
            // 
            this.deselectAllButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.deselectAllButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.deselectAllButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.deselectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deselectAllButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.deselectAllButton.ForeColor = System.Drawing.Color.White;
            this.deselectAllButton.Location = new System.Drawing.Point(249, 9);
            this.deselectAllButton.Name = "deselectAllButton";
            this.deselectAllButton.Size = new System.Drawing.Size(228, 34);
            this.deselectAllButton.TabIndex = 2;
            this.deselectAllButton.Text = "Deselect All";
            this.deselectAllButton.UseVisualStyleBackColor = false;
            this.deselectAllButton.Click += new System.EventHandler(this.deselectAllButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.skipBtn);
            this.panel2.Controls.Add(this.continueButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(6, 524);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 1, 5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(482, 96);
            this.panel2.TabIndex = 1;
            // 
            // skipBtn
            // 
            this.skipBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(200)))));
            this.skipBtn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.skipBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.skipBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.skipBtn.ForeColor = System.Drawing.Color.White;
            this.skipBtn.Location = new System.Drawing.Point(112, 53);
            this.skipBtn.Name = "skipBtn";
            this.skipBtn.Size = new System.Drawing.Size(271, 34);
            this.skipBtn.TabIndex = 4;
            this.skipBtn.Text = "Skip";
            this.skipBtn.UseVisualStyleBackColor = false;
            this.skipBtn.Click += new System.EventHandler(this.skipBtn_Click);
            // 
            // continueButton
            // 
            this.continueButton.BackColor = System.Drawing.Color.RoyalBlue;
            this.continueButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(112, 10);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(271, 37);
            this.continueButton.TabIndex = 3;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.White;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.Font = new System.Drawing.Font("Verdana", 9F);
            this.checkedListBox1.ForeColor = System.Drawing.Color.Black;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(6, 141);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(5, 1, 5, 0);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(482, 381);
            this.checkedListBox1.Sorted = true;
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.Click += new System.EventHandler(this.checkedListBox1_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label_message);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(6, 6);
            this.panel4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(482, 78);
            this.panel4.TabIndex = 4;
            // 
            // label_message
            // 
            this.label_message.BackColor = System.Drawing.Color.White;
            this.label_message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_message.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label_message.ForeColor = System.Drawing.Color.Black;
            this.label_message.Location = new System.Drawing.Point(0, 0);
            this.label_message.Name = "label_message";
            this.label_message.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.label_message.Size = new System.Drawing.Size(480, 76);
            this.label_message.TabIndex = 2;
            this.label_message.Text = "label_message";
            this.label_message.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.RoyalBlue;
            this.closeBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.closeBtn.Location = new System.Drawing.Point(460, 6);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(30, 30);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "X";
            this.closeBtn.UseVisualStyleBackColor = false;
            this.closeBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // HostsDeletionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(494, 667);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HostsDeletionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HostsDeletionForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.HostsDeletionForm_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label top;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button deselectAllButton;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Label label_message;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button skipBtn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
    }
}