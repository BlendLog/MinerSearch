namespace MSearch.UI
{
    partial class AboutPage
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rootLayout = new System.Windows.Forms.TableLayoutPanel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelCard = new System.Windows.Forms.Panel();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelDeviceIdCaption = new System.Windows.Forms.Label();
            this.linkDeviceId = new System.Windows.Forms.LinkLabel();
            this.linkTelegram = new System.Windows.Forms.LinkLabel();
            this.labelDisclaimer = new System.Windows.Forms.Label();
            this.rootLayout.SuspendLayout();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.labelTitle, 0, 0);
            this.rootLayout.Controls.Add(this.panelCard, 0, 1);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(26, 20);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 3;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Size = new System.Drawing.Size(842, 634);
            this.rootLayout.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(842, 54);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "О программе";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.labelAppName);
            this.panelCard.Controls.Add(this.labelVersion);
            this.panelCard.Controls.Add(this.labelDeviceIdCaption);
            this.panelCard.Controls.Add(this.linkDeviceId);
            this.panelCard.Controls.Add(this.linkTelegram);
            this.panelCard.Controls.Add(this.labelDisclaimer);
            this.panelCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard.Location = new System.Drawing.Point(0, 54);
            this.panelCard.Margin = new System.Windows.Forms.Padding(0);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(842, 340);
            this.panelCard.TabIndex = 1;
            // 
            // labelAppName
            // 
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.labelAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelAppName.Location = new System.Drawing.Point(24, 22);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(121, 25);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "MinerSearch";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelVersion.Location = new System.Drawing.Point(24, 56);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(90, 19);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Версия: 0.0.0";
            // 
            // labelDeviceIdCaption
            // 
            this.labelDeviceIdCaption.AutoSize = true;
            this.labelDeviceIdCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDeviceIdCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelDeviceIdCaption.Location = new System.Drawing.Point(24, 96);
            this.labelDeviceIdCaption.Name = "labelDeviceIdCaption";
            this.labelDeviceIdCaption.Size = new System.Drawing.Size(260, 15);
            this.labelDeviceIdCaption.TabIndex = 2;
            this.labelDeviceIdCaption.Text = "ID устройства (нажмите, чтобы скопировать)";
            // 
            // linkDeviceId
            // 
            this.linkDeviceId.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.linkDeviceId.AutoSize = true;
            this.linkDeviceId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.linkDeviceId.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.linkDeviceId.Location = new System.Drawing.Point(24, 116);
            this.linkDeviceId.Name = "linkDeviceId";
            this.linkDeviceId.Size = new System.Drawing.Size(150, 19);
            this.linkDeviceId.TabIndex = 3;
            this.linkDeviceId.TabStop = true;
            this.linkDeviceId.Text = "00000000-0000-0000-0000-000000000000";
            // 
            // linkTelegram
            // 
            this.linkTelegram.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.linkTelegram.AutoSize = true;
            this.linkTelegram.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.linkTelegram.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.linkTelegram.Location = new System.Drawing.Point(24, 158);
            this.linkTelegram.Name = "linkTelegram";
            this.linkTelegram.Size = new System.Drawing.Size(150, 19);
            this.linkTelegram.TabIndex = 4;
            this.linkTelegram.TabStop = true;
            this.linkTelegram.Text = "t.me/MinerSearch_blog";
            // 
            // 
            // labelDisclaimer
            // 
            this.labelDisclaimer.AutoSize = true;
            this.labelDisclaimer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDisclaimer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelDisclaimer.Location = new System.Drawing.Point(24, 200);
            this.labelDisclaimer.MaximumSize = new System.Drawing.Size(760, 0);
            this.labelDisclaimer.Name = "labelDisclaimer";
            this.labelDisclaimer.Size = new System.Drawing.Size(760, 30);
            this.labelDisclaimer.TabIndex = 6;

            this.panelCard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCard_Paint);
            this.linkDeviceId.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDeviceId_LinkClicked);
            this.linkTelegram.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTelegram_LinkClicked);
            this.labelDisclaimer.Text = "MinerSearch не является антивирусом.";
            // 
            // AboutPage
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.rootLayout);
            this.Name = "AboutPage";
            this.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.Size = new System.Drawing.Size(894, 674);
            this.rootLayout.ResumeLayout(false);
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelDeviceIdCaption;
        private System.Windows.Forms.LinkLabel linkDeviceId;
        private System.Windows.Forms.LinkLabel linkTelegram;
        private System.Windows.Forms.Label labelDisclaimer;
    }
}
