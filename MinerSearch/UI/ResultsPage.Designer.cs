namespace MSearch.UI
{
    partial class ResultsPage
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
            this.panelSummary = new System.Windows.Forms.Panel();
            this.labelSummary = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.panelGridHost = new System.Windows.Forms.Panel();
            this.dataGridResults = new System.Windows.Forms.DataGridView();
            this.labelEmpty = new System.Windows.Forms.Label();
            this.buttonsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonOpenLog = new MSearch.UI.RoundButton();
            this.buttonLogsFolder = new MSearch.UI.RoundButton();
            this.buttonQuarantine = new MSearch.UI.RoundButton();
            this.buttonReboot = new MSearch.UI.RoundButton();
            this.rootLayout.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelGridHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).BeginInit();
            this.buttonsFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.labelTitle, 0, 0);
            this.rootLayout.Controls.Add(this.panelSummary, 0, 1);
            this.rootLayout.Controls.Add(this.panelGridHost, 0, 2);
            this.rootLayout.Controls.Add(this.buttonsFlow, 0, 3);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(26, 20);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 4;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
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
            this.labelTitle.Text = "Результаты";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelSummary
            // 
            this.panelSummary.Controls.Add(this.labelSummary);
            this.panelSummary.Controls.Add(this.labelDetails);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSummary.Location = new System.Drawing.Point(0, 54);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(0);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(842, 76);
            this.panelSummary.TabIndex = 1;
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.labelSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelSummary.Location = new System.Drawing.Point(3, 0);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(111, 25);
            this.labelSummary.TabIndex = 0;
            this.labelSummary.Text = "Результаты";
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelDetails.Location = new System.Drawing.Point(0, 36);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(0, 15);
            this.labelDetails.TabIndex = 1;
            // 
            // panelGridHost
            // 
            this.panelGridHost.BackColor = System.Drawing.Color.White;
            this.panelGridHost.Controls.Add(this.dataGridResults);
            this.panelGridHost.Controls.Add(this.labelEmpty);
            this.panelGridHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridHost.Location = new System.Drawing.Point(0, 130);
            this.panelGridHost.Margin = new System.Windows.Forms.Padding(0);
            this.panelGridHost.Name = "panelGridHost";
            this.panelGridHost.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridHost.Size = new System.Drawing.Size(842, 448);
            this.panelGridHost.TabIndex = 2;
            this.panelGridHost.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGridHost_Paint);
            // 
            // dataGridResults
            // 
            this.dataGridResults.AllowUserToAddRows = false;
            this.dataGridResults.AllowUserToDeleteRows = false;
            this.dataGridResults.AllowUserToResizeRows = false;
            this.dataGridResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridResults.ColumnHeadersHeight = 36;
            this.dataGridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dataGridResults.Location = new System.Drawing.Point(1, 1);
            this.dataGridResults.MultiSelect = false;
            this.dataGridResults.Name = "dataGridResults";
            this.dataGridResults.ReadOnly = true;
            this.dataGridResults.RowHeadersVisible = false;
            this.dataGridResults.RowTemplate.Height = 30;
            this.dataGridResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridResults.Size = new System.Drawing.Size(840, 446);
            this.dataGridResults.TabIndex = 0;
            this.dataGridResults.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Grid_RowsAdded);
            // 
            // labelEmpty
            // 
            this.labelEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEmpty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelEmpty.Location = new System.Drawing.Point(1, 1);
            this.labelEmpty.Name = "labelEmpty";
            this.labelEmpty.Size = new System.Drawing.Size(840, 446);
            this.labelEmpty.TabIndex = 1;
            this.labelEmpty.Text = "Проверка ещё не выполнялась";
            this.labelEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonsFlow
            // 
            this.buttonsFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.buttonsFlow.Controls.Add(this.buttonOpenLog);
            this.buttonsFlow.Controls.Add(this.buttonLogsFolder);
            this.buttonsFlow.Controls.Add(this.buttonQuarantine);
            this.buttonsFlow.Controls.Add(this.buttonReboot);
            this.buttonsFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsFlow.Location = new System.Drawing.Point(0, 578);
            this.buttonsFlow.Margin = new System.Windows.Forms.Padding(0);
            this.buttonsFlow.Name = "buttonsFlow";
            this.buttonsFlow.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.buttonsFlow.Size = new System.Drawing.Size(842, 56);
            this.buttonsFlow.TabIndex = 3;
            this.buttonsFlow.WrapContents = false;
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.BackColor = System.Drawing.Color.White;
            this.buttonOpenLog.CornerRadius = 6;
            this.buttonOpenLog.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonOpenLog.FlatAppearance.BorderSize = 0;
            this.buttonOpenLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpenLog.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonOpenLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.buttonOpenLog.Location = new System.Drawing.Point(0, 8);
            this.buttonOpenLog.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(160, 38);
            this.buttonOpenLog.TabIndex = 0;
            this.buttonOpenLog.Text = "Открыть лог";
            this.buttonOpenLog.UseVisualStyleBackColor = false;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // buttonLogsFolder
            // 
            this.buttonLogsFolder.BackColor = System.Drawing.Color.White;
            this.buttonLogsFolder.CornerRadius = 6;
            this.buttonLogsFolder.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonLogsFolder.FlatAppearance.BorderSize = 0;
            this.buttonLogsFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogsFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonLogsFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.buttonLogsFolder.Location = new System.Drawing.Point(170, 8);
            this.buttonLogsFolder.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.buttonLogsFolder.Name = "buttonLogsFolder";
            this.buttonLogsFolder.Size = new System.Drawing.Size(210, 38);
            this.buttonLogsFolder.TabIndex = 1;
            this.buttonLogsFolder.Text = "Показать все отчёты";
            this.buttonLogsFolder.UseVisualStyleBackColor = false;
            this.buttonLogsFolder.Click += new System.EventHandler(this.buttonLogsFolder_Click);
            // 
            // buttonQuarantine
            // 
            this.buttonQuarantine.BackColor = System.Drawing.Color.White;
            this.buttonQuarantine.CornerRadius = 6;
            this.buttonQuarantine.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonQuarantine.FlatAppearance.BorderSize = 0;
            this.buttonQuarantine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuarantine.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonQuarantine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.buttonQuarantine.Location = new System.Drawing.Point(390, 8);
            this.buttonQuarantine.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.buttonQuarantine.Name = "buttonQuarantine";
            this.buttonQuarantine.Size = new System.Drawing.Size(180, 38);
            this.buttonQuarantine.TabIndex = 2;
            this.buttonQuarantine.Text = "Открыть карантин";
            this.buttonQuarantine.UseVisualStyleBackColor = false;
            this.buttonQuarantine.Click += new System.EventHandler(this.buttonQuarantine_Click);
            // 
            // buttonReboot
            // 
            this.buttonReboot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(118)))), ((int)(((byte)(8)))));
            this.buttonReboot.CornerRadius = 6;
            this.buttonReboot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(118)))), ((int)(((byte)(8)))));
            this.buttonReboot.FlatAppearance.BorderSize = 0;
            this.buttonReboot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReboot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.buttonReboot.ForeColor = System.Drawing.Color.White;
            this.buttonReboot.Location = new System.Drawing.Point(580, 8);
            this.buttonReboot.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.buttonReboot.Name = "buttonReboot";
            this.buttonReboot.Size = new System.Drawing.Size(160, 38);
            this.buttonReboot.TabIndex = 3;
            this.buttonReboot.Text = "Перезагрузить";
            this.buttonReboot.UseVisualStyleBackColor = false;
            this.buttonReboot.Visible = false;
            this.buttonReboot.Click += new System.EventHandler(this.buttonReboot_Click);
            // 
            // ResultsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.rootLayout);
            this.Name = "ResultsPage";
            this.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.Size = new System.Drawing.Size(894, 674);
            this.rootLayout.ResumeLayout(false);
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panelGridHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResults)).EndInit();
            this.buttonsFlow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.Label labelDetails;
        private System.Windows.Forms.Panel panelGridHost;
        private System.Windows.Forms.DataGridView dataGridResults;
        private System.Windows.Forms.Label labelEmpty;
        private System.Windows.Forms.FlowLayoutPanel buttonsFlow;
        private MSearch.UI.RoundButton buttonOpenLog;
        private MSearch.UI.RoundButton buttonLogsFolder;
        private MSearch.UI.RoundButton buttonQuarantine;
        private MSearch.UI.RoundButton buttonReboot;
    }
}
