namespace MSearch.UI
{
    partial class QuarantinePage
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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.buttonRefresh = new MSearch.UI.RoundButton();
            this.panelHost = new System.Windows.Forms.Panel();
            this.dataGridQuarantine = new System.Windows.Forms.DataGridView();
            this.labelEmpty = new System.Windows.Forms.Label();
            this.actionsPanel = new System.Windows.Forms.Panel();
            this.buttonRestore = new MSearch.UI.RoundButton();
            this.buttonDelete = new MSearch.UI.RoundButton();
            this.rootLayout.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.panelHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuarantine)).BeginInit();
            this.actionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.headerPanel, 0, 0);
            this.rootLayout.Controls.Add(this.panelHost, 0, 1);
            this.rootLayout.Controls.Add(this.actionsPanel, 0, 2);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(26, 20);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 3;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.rootLayout.Size = new System.Drawing.Size(842, 634);
            this.rootLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.labelTitle);
            this.headerPanel.Controls.Add(this.labelCount);
            this.headerPanel.Controls.Add(this.buttonRefresh);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(842, 54);
            this.headerPanel.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(260, 54);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Карантин";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelCount.Location = new System.Drawing.Point(520, 17);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(190, 20);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "Объектов в карантине: 0";
            this.labelCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackColor = System.Drawing.Color.White;
            this.buttonRefresh.CornerRadius = 6;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.buttonRefresh.Location = new System.Drawing.Point(722, 10);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 34);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // panelHost
            // 
            this.panelHost.BackColor = System.Drawing.Color.White;
            this.panelHost.Controls.Add(this.dataGridQuarantine);
            this.panelHost.Controls.Add(this.labelEmpty);
            this.panelHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHost.Location = new System.Drawing.Point(0, 54);
            this.panelHost.Margin = new System.Windows.Forms.Padding(0);
            this.panelHost.Name = "panelHost";
            this.panelHost.Padding = new System.Windows.Forms.Padding(1);
            this.panelHost.Size = new System.Drawing.Size(842, 524);
            this.panelHost.TabIndex = 1;
            this.panelHost.Paint += new System.Windows.Forms.PaintEventHandler(this.panelHost_Paint);
            // 
            // dataGridQuarantine
            // 
            this.dataGridQuarantine.AllowUserToAddRows = false;
            this.dataGridQuarantine.AllowUserToDeleteRows = false;
            this.dataGridQuarantine.AllowUserToResizeRows = false;
            this.dataGridQuarantine.AutoGenerateColumns = false;
            this.dataGridQuarantine.BackgroundColor = System.Drawing.Color.White;
            this.dataGridQuarantine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridQuarantine.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridQuarantine.ColumnHeadersHeight = 36;
            this.dataGridQuarantine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridQuarantine.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dataGridQuarantine.Location = new System.Drawing.Point(1, 1);
            this.dataGridQuarantine.MultiSelect = false;
            this.dataGridQuarantine.Name = "dataGridQuarantine";
            this.dataGridQuarantine.ReadOnly = true;
            this.dataGridQuarantine.RowHeadersVisible = false;
            this.dataGridQuarantine.RowTemplate.Height = 30;
            this.dataGridQuarantine.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridQuarantine.Size = new System.Drawing.Size(840, 522);
            this.dataGridQuarantine.TabIndex = 0;
            this.dataGridQuarantine.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridQuarantine_CellContentClick);
            // 
            // labelEmpty
            // 
            this.labelEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelEmpty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelEmpty.Location = new System.Drawing.Point(1, 1);
            this.labelEmpty.Name = "labelEmpty";
            this.labelEmpty.Size = new System.Drawing.Size(840, 522);
            this.labelEmpty.TabIndex = 1;
            this.labelEmpty.Text = "Карантин пуст";
            this.labelEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // actionsPanel
            // 
            this.actionsPanel.Controls.Add(this.buttonRestore);
            this.actionsPanel.Controls.Add(this.buttonDelete);
            this.actionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionsPanel.Location = new System.Drawing.Point(0, 578);
            this.actionsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.actionsPanel.Name = "actionsPanel";
            this.actionsPanel.Size = new System.Drawing.Size(842, 56);
            this.actionsPanel.TabIndex = 2;
            // 
            // buttonRestore
            // 
            this.buttonRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestore.BackColor = System.Drawing.Color.White;
            this.buttonRestore.CornerRadius = 6;
            this.buttonRestore.Enabled = false;
            this.buttonRestore.FlatAppearance.BorderSize = 0;
            this.buttonRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestore.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonRestore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.buttonRestore.Location = new System.Drawing.Point(512, 10);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(160, 38);
            this.buttonRestore.TabIndex = 0;
            this.buttonRestore.Text = "Восстановить";
            this.buttonRestore.UseVisualStyleBackColor = false;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackColor = System.Drawing.Color.White;
            this.buttonDelete.CornerRadius = 6;
            this.buttonDelete.Enabled = false;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.buttonDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.buttonDelete.Location = new System.Drawing.Point(682, 10);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(160, 38);
            this.buttonDelete.TabIndex = 1;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // QuarantinePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.rootLayout);
            this.Name = "QuarantinePage";
            this.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.Size = new System.Drawing.Size(894, 674);
            this.rootLayout.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.panelHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridQuarantine)).EndInit();
            this.actionsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelCount;
        private MSearch.UI.RoundButton buttonRefresh;
        private System.Windows.Forms.Panel panelHost;
        private System.Windows.Forms.DataGridView dataGridQuarantine;
        private System.Windows.Forms.Label labelEmpty;
        private System.Windows.Forms.Panel actionsPanel;
        private MSearch.UI.RoundButton buttonRestore;
        private MSearch.UI.RoundButton buttonDelete;
    }
}
