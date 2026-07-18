namespace MSearch.UI
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseBtn = new MSearch.UI.RoundButton();
            this.top = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Label_autoApplyThreatAction = new System.Windows.Forms.Label();
            this.ts_AutoApplyThreatActions = new MSearch.ToggleSwitch();
            this.Label_allowStatistics = new System.Windows.Forms.Label();
            this.ts_AllowCollectStatistics = new MSearch.ToggleSwitch();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.68085F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.31915F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(750, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 36);
            this.panel1.TabIndex = 0;
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
            this.CloseBtn.Location = new System.Drawing.Point(719, 7);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(22, 24);
            this.CloseBtn.TabIndex = 101;
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
            this.top.Size = new System.Drawing.Size(750, 36);
            this.top.TabIndex = 3;
            this.top.Text = "_title";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Label_autoApplyThreatAction);
            this.panel2.Controls.Add(this.ts_AutoApplyThreatActions);
            this.panel2.Controls.Add(this.Label_allowStatistics);
            this.panel2.Controls.Add(this.ts_AllowCollectStatistics);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(750, 152);
            this.panel2.TabIndex = 1;
            // 
            // Label_autoApplyThreatAction
            // 
            this.Label_autoApplyThreatAction.BackColor = System.Drawing.Color.Transparent;
            this.Label_autoApplyThreatAction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label_autoApplyThreatAction.ForeColor = System.Drawing.Color.Black;
            this.Label_autoApplyThreatAction.Location = new System.Drawing.Point(49, 37);
            this.Label_autoApplyThreatAction.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_autoApplyThreatAction.Name = "Label_autoApplyThreatAction";
            this.Label_autoApplyThreatAction.Size = new System.Drawing.Size(692, 19);
            this.Label_autoApplyThreatAction.TabIndex = 18;
            this.Label_autoApplyThreatAction.Text = "_label_autoApplyThreatAction";
            this.Label_autoApplyThreatAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ts_AutoApplyThreatActions
            // 
            this.ts_AutoApplyThreatActions.AutoSize = true;
            this.ts_AutoApplyThreatActions.BackColor = System.Drawing.Color.Transparent;
            this.ts_AutoApplyThreatActions.Location = new System.Drawing.Point(11, 37);
            this.ts_AutoApplyThreatActions.Margin = new System.Windows.Forms.Padding(2);
            this.ts_AutoApplyThreatActions.MinimumSize = new System.Drawing.Size(34, 18);
            this.ts_AutoApplyThreatActions.Name = "ts_AutoApplyThreatActions";
            this.ts_AutoApplyThreatActions.OffBackColor = System.Drawing.Color.Gray;
            this.ts_AutoApplyThreatActions.OffToggleColor = System.Drawing.Color.White;
            this.ts_AutoApplyThreatActions.OnBackColor = System.Drawing.Color.RoyalBlue;
            this.ts_AutoApplyThreatActions.OnToggleColor = System.Drawing.Color.White;
            this.ts_AutoApplyThreatActions.Size = new System.Drawing.Size(34, 18);
            this.ts_AutoApplyThreatActions.TabIndex = 17;
            this.ts_AutoApplyThreatActions.UseVisualStyleBackColor = false;
            this.ts_AutoApplyThreatActions.CheckedChanged += new System.EventHandler(this.ts_AutoApplyThreatActions_CheckedChanged);
            // 
            // Label_allowStatistics
            // 
            this.Label_allowStatistics.BackColor = System.Drawing.Color.Transparent;
            this.Label_allowStatistics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label_allowStatistics.ForeColor = System.Drawing.Color.Black;
            this.Label_allowStatistics.Location = new System.Drawing.Point(49, 11);
            this.Label_allowStatistics.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_allowStatistics.Name = "Label_allowStatistics";
            this.Label_allowStatistics.Size = new System.Drawing.Size(690, 19);
            this.Label_allowStatistics.TabIndex = 16;
            this.Label_allowStatistics.Text = "_label_allowStatistics";
            this.Label_allowStatistics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ts_AllowCollectStatistics
            // 
            this.ts_AllowCollectStatistics.AutoSize = true;
            this.ts_AllowCollectStatistics.BackColor = System.Drawing.Color.Transparent;
            this.ts_AllowCollectStatistics.Location = new System.Drawing.Point(11, 11);
            this.ts_AllowCollectStatistics.Margin = new System.Windows.Forms.Padding(2);
            this.ts_AllowCollectStatistics.MinimumSize = new System.Drawing.Size(34, 18);
            this.ts_AllowCollectStatistics.Name = "ts_AllowCollectStatistics";
            this.ts_AllowCollectStatistics.OffBackColor = System.Drawing.Color.Gray;
            this.ts_AllowCollectStatistics.OffToggleColor = System.Drawing.Color.White;
            this.ts_AllowCollectStatistics.OnBackColor = System.Drawing.Color.RoyalBlue;
            this.ts_AllowCollectStatistics.OnToggleColor = System.Drawing.Color.White;
            this.ts_AllowCollectStatistics.Size = new System.Drawing.Size(34, 18);
            this.ts_AllowCollectStatistics.TabIndex = 1;
            this.ts_AllowCollectStatistics.UseVisualStyleBackColor = false;
            this.ts_AllowCollectStatistics.CheckedChanged += new System.EventHandler(this.ts_AllowCollectStatistics_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 188);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSettings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label top;
        private RoundButton CloseBtn;
        private System.Windows.Forms.Panel panel2;
        private ToggleSwitch ts_AllowCollectStatistics;
        private System.Windows.Forms.Label Label_autoApplyThreatAction;
        private ToggleSwitch ts_AutoApplyThreatActions;
        private System.Windows.Forms.Label Label_allowStatistics;
    }
}