namespace MSearch.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.labelLogo = new System.Windows.Forms.Label();
            this.panelNav = new System.Windows.Forms.Panel();
            this.buttonNavAbout = new System.Windows.Forms.Button();
            this.buttonNavSettings = new System.Windows.Forms.Button();
            this.buttonNavQuarantine = new System.Windows.Forms.Button();
            this.buttonNavResults = new System.Windows.Forms.Button();
            this.buttonNavScan = new System.Windows.Forms.Button();
            this.buttonDonateSidebar = new MSearch.UI.RoundButton();
            this.panelMain.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelMain.Controls.Add(this.panelContent);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(210, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(910, 720);
            this.panelMain.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 46);
            this.panelContent.Margin = new System.Windows.Forms.Padding(0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(910, 674);
            this.panelContent.TabIndex = 1;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.panelHeader.Controls.Add(this.buttonClose);
            this.panelHeader.Controls.Add(this.buttonMinimize);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(910, 46);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Header_MouseDown);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.buttonClose.Location = new System.Drawing.Point(862, 8);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 30);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "✕";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.WindowButton_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.WindowButton_MouseLeave);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.buttonMinimize.FlatAppearance.BorderSize = 0;
            this.buttonMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMinimize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonMinimize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.buttonMinimize.Location = new System.Drawing.Point(818, 8);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(40, 30);
            this.buttonMinimize.TabIndex = 0;
            this.buttonMinimize.TabStop = false;
            this.buttonMinimize.Text = "—";
            this.buttonMinimize.UseVisualStyleBackColor = false;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            this.buttonMinimize.MouseEnter += new System.EventHandler(this.WindowButton_MouseEnter);
            this.buttonMinimize.MouseLeave += new System.EventHandler(this.WindowButton_MouseLeave);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.panelSidebar.Controls.Add(this.buttonDonateSidebar);
            this.panelSidebar.Controls.Add(this.labelLogo);
            this.panelSidebar.Controls.Add(this.panelNav);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.panelSidebar.Size = new System.Drawing.Size(210, 720);
            this.panelSidebar.TabIndex = 1;
            // buttonDonateSidebar
            this.buttonDonateSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDonateSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.buttonDonateSidebar.CornerRadius = 6;
            this.buttonDonateSidebar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDonateSidebar.FlatAppearance.BorderSize = 0;
            this.buttonDonateSidebar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(92)))), ((int)(((byte)(194)))));
            this.buttonDonateSidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDonateSidebar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDonateSidebar.ForeColor = System.Drawing.Color.White;
            this.buttonDonateSidebar.Location = new System.Drawing.Point(12, 664);
            this.buttonDonateSidebar.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDonateSidebar.Name = "buttonDonateSidebar";
            this.buttonDonateSidebar.Size = new System.Drawing.Size(186, 42);
            this.buttonDonateSidebar.TabIndex = 2;
            this.buttonDonateSidebar.TabStop = false;
            this.buttonDonateSidebar.Text = "buttonDonateSidebar";
            this.buttonDonateSidebar.UseVisualStyleBackColor = false;
            this.buttonDonateSidebar.Click += new System.EventHandler(this.buttonDonateSidebar_Click);
            // labelLogo
            this.labelLogo.AutoSize = true;
            this.labelLogo.BackColor = System.Drawing.Color.Transparent;
            this.labelLogo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.labelLogo.ForeColor = System.Drawing.Color.White;
            this.labelLogo.Location = new System.Drawing.Point(24, 18);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(124, 25);
            this.labelLogo.TabIndex = 0;
            this.labelLogo.Text = "MinerSearch";
            // panelNav
            this.panelNav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.panelNav.Controls.Add(this.buttonNavAbout);
            this.panelNav.Controls.Add(this.buttonNavSettings);
            this.panelNav.Controls.Add(this.buttonNavQuarantine);
            this.panelNav.Controls.Add(this.buttonNavResults);
            this.panelNav.Controls.Add(this.buttonNavScan);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNav.Location = new System.Drawing.Point(0, 14);
            this.panelNav.Margin = new System.Windows.Forms.Padding(0);
            this.panelNav.Name = "panelNav";
            this.panelNav.Padding = new System.Windows.Forms.Padding(0, 58, 0, 0);
            this.panelNav.Size = new System.Drawing.Size(210, 706);
            this.panelNav.TabIndex = 1;
            // 
            // buttonNavAbout
            // 
            this.buttonNavAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.buttonNavAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNavAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNavAbout.FlatAppearance.BorderSize = 0;
            this.buttonNavAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(61)))));
            this.buttonNavAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavAbout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonNavAbout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(204)))), ((int)(((byte)(214)))));
            this.buttonNavAbout.Location = new System.Drawing.Point(0, 234);
            this.buttonNavAbout.Name = "buttonNavAbout";
            this.buttonNavAbout.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.buttonNavAbout.Size = new System.Drawing.Size(210, 44);
            this.buttonNavAbout.TabIndex = 4;
            this.buttonNavAbout.TabStop = false;
            this.buttonNavAbout.Tag = "_About";
            this.buttonNavAbout.Text = "buttonNavAbout";
            this.buttonNavAbout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNavAbout.UseVisualStyleBackColor = false;
            this.buttonNavAbout.Click += new System.EventHandler(this.buttonNavAbout_Click);
            // 
            // buttonNavSettings
            // 
            this.buttonNavSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.buttonNavSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNavSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNavSettings.FlatAppearance.BorderSize = 0;
            this.buttonNavSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(61)))));
            this.buttonNavSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavSettings.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonNavSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(204)))), ((int)(((byte)(214)))));
            this.buttonNavSettings.Location = new System.Drawing.Point(0, 190);
            this.buttonNavSettings.Name = "buttonNavSettings";
            this.buttonNavSettings.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.buttonNavSettings.Size = new System.Drawing.Size(210, 44);
            this.buttonNavSettings.TabIndex = 3;
            this.buttonNavSettings.TabStop = false;
            this.buttonNavSettings.Tag = "_FormSettings_Title";
            this.buttonNavSettings.Text = "buttonNavSettings";
            this.buttonNavSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNavSettings.UseVisualStyleBackColor = false;
            this.buttonNavSettings.Click += new System.EventHandler(this.buttonNavSettings_Click);
            // 
            // buttonNavQuarantine
            // 
            this.buttonNavQuarantine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.buttonNavQuarantine.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNavQuarantine.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNavQuarantine.FlatAppearance.BorderSize = 0;
            this.buttonNavQuarantine.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(61)))));
            this.buttonNavQuarantine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavQuarantine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonNavQuarantine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(204)))), ((int)(((byte)(214)))));
            this.buttonNavQuarantine.Location = new System.Drawing.Point(0, 146);
            this.buttonNavQuarantine.Name = "buttonNavQuarantine";
            this.buttonNavQuarantine.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.buttonNavQuarantine.Size = new System.Drawing.Size(210, 44);
            this.buttonNavQuarantine.TabIndex = 2;
            this.buttonNavQuarantine.TabStop = false;
            this.buttonNavQuarantine.Tag = "_Quarantine";
            this.buttonNavQuarantine.Text = "buttonNavQuarantine";
            this.buttonNavQuarantine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNavQuarantine.UseVisualStyleBackColor = false;
            this.buttonNavQuarantine.Click += new System.EventHandler(this.buttonNavQuarantine_Click);
            // 
            // buttonNavResults
            // 
            this.buttonNavResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.buttonNavResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNavResults.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNavResults.FlatAppearance.BorderSize = 0;
            this.buttonNavResults.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(61)))));
            this.buttonNavResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavResults.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonNavResults.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(204)))), ((int)(((byte)(214)))));
            this.buttonNavResults.Location = new System.Drawing.Point(0, 102);
            this.buttonNavResults.Name = "buttonNavResults";
            this.buttonNavResults.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.buttonNavResults.Size = new System.Drawing.Size(210, 44);
            this.buttonNavResults.TabIndex = 1;
            this.buttonNavResults.TabStop = false;
            this.buttonNavResults.Tag = "_Results";
            this.buttonNavResults.Text = "buttonNavResults";
            this.buttonNavResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNavResults.UseVisualStyleBackColor = false;
            this.buttonNavResults.Click += new System.EventHandler(this.buttonNavResults_Click);
            // 
            // buttonNavScan
            // 
            this.buttonNavScan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.buttonNavScan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNavScan.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNavScan.FlatAppearance.BorderSize = 0;
            this.buttonNavScan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(46)))), ((int)(((byte)(61)))));
            this.buttonNavScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNavScan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonNavScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(204)))), ((int)(((byte)(214)))));
            this.buttonNavScan.Location = new System.Drawing.Point(0, 58);
            this.buttonNavScan.Name = "buttonNavScan";
            this.buttonNavScan.Padding = new System.Windows.Forms.Padding(24, 0, 0, 0);
            this.buttonNavScan.Size = new System.Drawing.Size(210, 44);
            this.buttonNavScan.TabIndex = 0;
            this.buttonNavScan.TabStop = false;
            this.buttonNavScan.Tag = "_Nav_Scan";
            this.buttonNavScan.Text = "buttonNavScan";
            this.buttonNavScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNavScan.UseVisualStyleBackColor = false;
            this.buttonNavScan.Click += new System.EventHandler(this.buttonNavScan_Click);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(980, 640);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelMain.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button buttonNavAbout;
        private System.Windows.Forms.Button buttonNavSettings;
        private System.Windows.Forms.Button buttonNavQuarantine;
        private System.Windows.Forms.Button buttonNavResults;
        private System.Windows.Forms.Button buttonNavScan;
        private System.Windows.Forms.Label labelLogo;
        private MSearch.UI.RoundButton buttonDonateSidebar;
    }
}
