namespace MSearch.UI
{
    partial class SettingsPage
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
            this.rowsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panelLanguageText = new System.Windows.Forms.Panel();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.panelLanguageControl = new System.Windows.Forms.Panel();
            this.comboLanguage = new System.Windows.Forms.ComboBox();
            this.panelStatisticsText = new System.Windows.Forms.Panel();
            this.labelStatistics = new System.Windows.Forms.Label();
            this.labelStatisticsHint = new System.Windows.Forms.Label();
            this.panelStatisticsControl = new System.Windows.Forms.Panel();
            this.toggleStatistics = new MSearch.ToggleSwitch();
            this.panelAutoAcceptText = new System.Windows.Forms.Panel();
            this.labelAutoAccept = new System.Windows.Forms.Label();
            this.labelAutoAcceptHint = new System.Windows.Forms.Label();
            this.panelAutoAcceptControl = new System.Windows.Forms.Panel();
            this.toggleAutoAccept = new MSearch.ToggleSwitch();
            this.panelEulaText = new System.Windows.Forms.Panel();
            this.labelEula = new System.Windows.Forms.Label();
            this.panelEulaControl = new System.Windows.Forms.Panel();
            this.buttonEula = new MSearch.UI.RoundButton();
            this.rootLayout.SuspendLayout();
            this.panelCard.SuspendLayout();
            this.rowsLayout.SuspendLayout();
            this.panelLanguageText.SuspendLayout();
            this.panelLanguageControl.SuspendLayout();
            this.panelStatisticsText.SuspendLayout();
            this.panelStatisticsControl.SuspendLayout();
            this.panelAutoAcceptText.SuspendLayout();
            this.panelAutoAcceptControl.SuspendLayout();
            this.panelEulaText.SuspendLayout();
            this.panelEulaControl.SuspendLayout();
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
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 420F));
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
            this.labelTitle.Text = "labelTitle";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.rowsLayout);
            this.panelCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard.Location = new System.Drawing.Point(0, 54);
            this.panelCard.Margin = new System.Windows.Forms.Padding(0);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(24);
            this.panelCard.Size = new System.Drawing.Size(842, 420);
            this.panelCard.TabIndex = 1;
            // 
            // rowsLayout
            // 
            this.rowsLayout.BackColor = System.Drawing.Color.White;
            this.rowsLayout.ColumnCount = 2;
            this.rowsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rowsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.rowsLayout.Controls.Add(this.panelLanguageText, 0, 0);
            this.rowsLayout.Controls.Add(this.panelLanguageControl, 1, 0);
            this.rowsLayout.Controls.Add(this.panelStatisticsText, 0, 1);
            this.rowsLayout.Controls.Add(this.panelStatisticsControl, 1, 1);
            this.rowsLayout.Controls.Add(this.panelAutoAcceptText, 0, 2);
            this.rowsLayout.Controls.Add(this.panelAutoAcceptControl, 1, 2);
            this.rowsLayout.Controls.Add(this.panelEulaText, 0, 3);
            this.rowsLayout.Controls.Add(this.panelEulaControl, 1, 3);
            this.rowsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowsLayout.Location = new System.Drawing.Point(24, 24);
            this.rowsLayout.Name = "rowsLayout";
            this.rowsLayout.RowCount = 4;
            this.rowsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.rowsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.rowsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.rowsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.rowsLayout.Size = new System.Drawing.Size(794, 372);
            this.rowsLayout.TabIndex = 0;
            // 
            // panelLanguageText
            // 
            this.panelLanguageText.BackColor = System.Drawing.Color.White;
            this.panelLanguageText.Controls.Add(this.labelLanguage);
            this.panelLanguageText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLanguageText.Location = new System.Drawing.Point(0, 0);
            this.panelLanguageText.Margin = new System.Windows.Forms.Padding(0);
            this.panelLanguageText.Name = "panelLanguageText";
            this.panelLanguageText.Size = new System.Drawing.Size(614, 90);
            this.panelLanguageText.TabIndex = 0;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelLanguage.Location = new System.Drawing.Point(0, 30);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(130, 19);
            this.labelLanguage.TabIndex = 0;
            this.labelLanguage.Text = "labelLanguage";
            // 
            // panelLanguageControl
            // 
            this.panelLanguageControl.BackColor = System.Drawing.Color.White;
            this.panelLanguageControl.Controls.Add(this.comboLanguage);
            this.panelLanguageControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLanguageControl.Location = new System.Drawing.Point(614, 0);
            this.panelLanguageControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelLanguageControl.Name = "panelLanguageControl";
            this.panelLanguageControl.Size = new System.Drawing.Size(180, 90);
            this.panelLanguageControl.TabIndex = 1;
            // 
            // comboLanguage
            // 
            this.comboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboLanguage.FormattingEnabled = true;
            this.comboLanguage.Items.AddRange(new object[] {
            "Русский",
            "English"});
            this.comboLanguage.Location = new System.Drawing.Point(10, 27);
            this.comboLanguage.Name = "comboLanguage";
            this.comboLanguage.Size = new System.Drawing.Size(170, 25);
            this.comboLanguage.TabIndex = 0;
            // 
            // panelStatisticsText
            // 
            this.panelStatisticsText.BackColor = System.Drawing.Color.White;
            this.panelStatisticsText.Controls.Add(this.labelStatistics);
            this.panelStatisticsText.Controls.Add(this.labelStatisticsHint);
            this.panelStatisticsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatisticsText.Location = new System.Drawing.Point(0, 90);
            this.panelStatisticsText.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatisticsText.Name = "panelStatisticsText";
            this.panelStatisticsText.Size = new System.Drawing.Size(614, 90);
            this.panelStatisticsText.TabIndex = 2;
            // 
            // labelStatistics
            // 
            this.labelStatistics.AutoSize = true;
            this.labelStatistics.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelStatistics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelStatistics.Location = new System.Drawing.Point(0, 20);
            this.labelStatistics.Name = "labelStatistics";
            this.labelStatistics.Size = new System.Drawing.Size(230, 19);
            this.labelStatistics.TabIndex = 0;
            this.labelStatistics.Text = "labelStatistics";
            // 
            // labelStatisticsHint
            // 
            this.labelStatisticsHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatisticsHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelStatisticsHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelStatisticsHint.Location = new System.Drawing.Point(0, 46);
            this.labelStatisticsHint.Name = "labelStatisticsHint";
            this.labelStatisticsHint.Size = new System.Drawing.Size(560, 20);
            this.labelStatisticsHint.TabIndex = 1;
            this.labelStatisticsHint.Text = "labelStatisticsHint";
            // 
            // panelStatisticsControl
            // 
            this.panelStatisticsControl.BackColor = System.Drawing.Color.White;
            this.panelStatisticsControl.Controls.Add(this.toggleStatistics);
            this.panelStatisticsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatisticsControl.Location = new System.Drawing.Point(614, 90);
            this.panelStatisticsControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatisticsControl.Name = "panelStatisticsControl";
            this.panelStatisticsControl.Size = new System.Drawing.Size(180, 90);
            this.panelStatisticsControl.TabIndex = 3;
            // 
            // toggleStatistics
            // 
            this.toggleStatistics.Location = new System.Drawing.Point(120, 33);
            this.toggleStatistics.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toggleStatistics.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleStatistics.Name = "toggleStatistics";
            this.toggleStatistics.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.toggleStatistics.OffToggleColor = System.Drawing.Color.White;
            this.toggleStatistics.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.toggleStatistics.Size = new System.Drawing.Size(48, 24);
            this.toggleStatistics.TabIndex = 0;
            this.toggleStatistics.UseVisualStyleBackColor = true;
            // 
            // panelAutoAcceptText
            // 
            this.panelAutoAcceptText.BackColor = System.Drawing.Color.White;
            this.panelAutoAcceptText.Controls.Add(this.labelAutoAccept);
            this.panelAutoAcceptText.Controls.Add(this.labelAutoAcceptHint);
            this.panelAutoAcceptText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutoAcceptText.Location = new System.Drawing.Point(0, 180);
            this.panelAutoAcceptText.Margin = new System.Windows.Forms.Padding(0);
            this.panelAutoAcceptText.Name = "panelAutoAcceptText";
            this.panelAutoAcceptText.Size = new System.Drawing.Size(614, 90);
            this.panelAutoAcceptText.TabIndex = 4;
            // 
            // labelAutoAccept
            // 
            this.labelAutoAccept.AutoSize = true;
            this.labelAutoAccept.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelAutoAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelAutoAccept.Location = new System.Drawing.Point(0, 20);
            this.labelAutoAccept.Name = "labelAutoAccept";
            this.labelAutoAccept.Size = new System.Drawing.Size(300, 19);
            this.labelAutoAccept.TabIndex = 0;
            this.labelAutoAccept.Text = "labelAutoAccept";
            // 
            // labelAutoAcceptHint
            // 
            this.labelAutoAcceptHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAutoAcceptHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelAutoAcceptHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelAutoAcceptHint.Location = new System.Drawing.Point(0, 46);
            this.labelAutoAcceptHint.Name = "labelAutoAcceptHint";
            this.labelAutoAcceptHint.Size = new System.Drawing.Size(560, 20);
            this.labelAutoAcceptHint.TabIndex = 1;
            this.labelAutoAcceptHint.Text = "labelAutoAcceptHint";
            // 
            // panelAutoAcceptControl
            // 
            this.panelAutoAcceptControl.BackColor = System.Drawing.Color.White;
            this.panelAutoAcceptControl.Controls.Add(this.toggleAutoAccept);
            this.panelAutoAcceptControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAutoAcceptControl.Location = new System.Drawing.Point(614, 180);
            this.panelAutoAcceptControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelAutoAcceptControl.Name = "panelAutoAcceptControl";
            this.panelAutoAcceptControl.Size = new System.Drawing.Size(180, 90);
            this.panelAutoAcceptControl.TabIndex = 5;
            // 
            // toggleAutoAccept
            // 
            this.toggleAutoAccept.Location = new System.Drawing.Point(120, 33);
            this.toggleAutoAccept.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.toggleAutoAccept.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleAutoAccept.Name = "toggleAutoAccept";
            this.toggleAutoAccept.OffBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(32)))), ((int)(((byte)(44)))));
            this.toggleAutoAccept.OffToggleColor = System.Drawing.Color.White;
            this.toggleAutoAccept.OnBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.toggleAutoAccept.Size = new System.Drawing.Size(48, 24);
            this.toggleAutoAccept.TabIndex = 0;
            this.toggleAutoAccept.UseVisualStyleBackColor = true;
            // 
            // panelEulaText
            // 
            this.panelEulaText.BackColor = System.Drawing.Color.White;
            this.panelEulaText.Controls.Add(this.labelEula);
            this.panelEulaText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEulaText.Location = new System.Drawing.Point(0, 270);
            this.panelEulaText.Margin = new System.Windows.Forms.Padding(0);
            this.panelEulaText.Name = "panelEulaText";
            this.panelEulaText.Size = new System.Drawing.Size(614, 90);
            this.panelEulaText.TabIndex = 6;
            // 
            // labelEula
            // 
            this.labelEula.AutoSize = true;
            this.labelEula.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelEula.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelEula.Location = new System.Drawing.Point(0, 30);
            this.labelEula.Name = "labelEula";
            this.labelEula.Size = new System.Drawing.Size(190, 19);
            this.labelEula.TabIndex = 0;
            this.labelEula.Text = "labelEula";
            // 
            // panelEulaControl
            // 
            this.panelEulaControl.BackColor = System.Drawing.Color.White;
            this.panelEulaControl.Controls.Add(this.buttonEula);
            this.panelEulaControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEulaControl.Location = new System.Drawing.Point(614, 270);
            this.panelEulaControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelEulaControl.Name = "panelEulaControl";
            this.panelEulaControl.Size = new System.Drawing.Size(180, 90);
            this.panelEulaControl.TabIndex = 7;
            // 
            // buttonEula
            // 
            this.buttonEula.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.buttonEula.CornerRadius = 6;
            this.buttonEula.FlatAppearance.BorderSize = 0;
            this.buttonEula.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEula.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonEula.ForeColor = System.Drawing.Color.White;
            this.buttonEula.Location = new System.Drawing.Point(30, 28);
            this.buttonEula.Name = "buttonEula";
            this.buttonEula.Size = new System.Drawing.Size(140, 34);
            this.buttonEula.TabIndex = 0;
            this.buttonEula.Text = "buttonEula";
            this.buttonEula.UseVisualStyleBackColor = false;

            this.panelCard.Paint += new System.Windows.Forms.PaintEventHandler(this.panelCard_Paint);
            this.comboLanguage.SelectedIndexChanged += new System.EventHandler(this.comboLanguage_SelectedIndexChanged);
            this.toggleStatistics.CheckedChanged += new System.EventHandler(this.toggleStatistics_CheckedChanged);
            this.toggleAutoAccept.CheckedChanged += new System.EventHandler(this.toggleAutoAccept_CheckedChanged);
            this.buttonEula.Click += new System.EventHandler(this.buttonEula_Click);
            this.panelLanguageControl.Resize += new System.EventHandler(this.ControlPanel_Resize);
            this.panelStatisticsControl.Resize += new System.EventHandler(this.ControlPanel_Resize);
            this.panelAutoAcceptControl.Resize += new System.EventHandler(this.ControlPanel_Resize);
            this.panelEulaControl.Resize += new System.EventHandler(this.ControlPanel_Resize);
            // 
            // SettingsPage
                        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.rootLayout);
            this.Name = "SettingsPage";
            this.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.Size = new System.Drawing.Size(894, 674);
            this.rootLayout.ResumeLayout(false);
            this.panelCard.ResumeLayout(false);
            this.rowsLayout.ResumeLayout(false);
            this.panelLanguageText.ResumeLayout(false);
            this.panelLanguageText.PerformLayout();
            this.panelLanguageControl.ResumeLayout(false);
            this.panelStatisticsText.ResumeLayout(false);
            this.panelStatisticsText.PerformLayout();
            this.panelStatisticsControl.ResumeLayout(false);
            this.panelAutoAcceptText.ResumeLayout(false);
            this.panelAutoAcceptText.PerformLayout();
            this.panelAutoAcceptControl.ResumeLayout(false);
            this.panelEulaText.ResumeLayout(false);
            this.panelEulaText.PerformLayout();
            this.panelEulaControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.TableLayoutPanel rowsLayout;
        private System.Windows.Forms.Panel panelLanguageText;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.Panel panelLanguageControl;
        private System.Windows.Forms.ComboBox comboLanguage;
        private System.Windows.Forms.Panel panelStatisticsText;
        private System.Windows.Forms.Label labelStatistics;
        private System.Windows.Forms.Label labelStatisticsHint;
        private System.Windows.Forms.Panel panelStatisticsControl;
        private MSearch.ToggleSwitch toggleStatistics;
        private System.Windows.Forms.Panel panelAutoAcceptText;
        private System.Windows.Forms.Label labelAutoAccept;
        private System.Windows.Forms.Label labelAutoAcceptHint;
        private System.Windows.Forms.Panel panelAutoAcceptControl;
        private MSearch.ToggleSwitch toggleAutoAccept;
        private System.Windows.Forms.Panel panelEulaText;
        private System.Windows.Forms.Label labelEula;
        private System.Windows.Forms.Panel panelEulaControl;
        private MSearch.UI.RoundButton buttonEula;
    }
}
