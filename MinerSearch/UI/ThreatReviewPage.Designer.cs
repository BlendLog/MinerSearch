namespace MSearch.UI
{
    partial class ThreatReviewPage
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
            this.labelInstruction = new System.Windows.Forms.Label();
            this.leftFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBulk = new System.Windows.Forms.ComboBox();
            this.btnBulkApply = new MSearch.UI.RoundButton();
            this.panelGridHost = new System.Windows.Forms.Panel();
            this.dataGridReview = new System.Windows.Forms.DataGridView();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.rightFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSkipAll = new MSearch.UI.RoundButton();
            this.btnContinue = new MSearch.UI.RoundButton();
            this.btnApply = new MSearch.UI.RoundButton();
            this.rootLayout.SuspendLayout();
            this.leftFlow.SuspendLayout();
            this.panelGridHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReview)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.rightFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // rootLayout
            // 
            this.rootLayout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.rootLayout.ColumnCount = 1;
            this.rootLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.Controls.Add(this.labelTitle, 0, 0);
            this.rootLayout.Controls.Add(this.labelInstruction, 0, 1);
            this.rootLayout.Controls.Add(this.leftFlow, 0, 2);
            this.rootLayout.Controls.Add(this.panelGridHost, 0, 3);
            this.rootLayout.Controls.Add(this.bottomPanel, 0, 4);
            this.rootLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootLayout.Location = new System.Drawing.Point(26, 20);
            this.rootLayout.Margin = new System.Windows.Forms.Padding(0);
            this.rootLayout.Name = "rootLayout";
            this.rootLayout.RowCount = 5;
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rootLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
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
            // labelInstruction
            // 
            this.labelInstruction.AutoEllipsis = true;
            this.labelInstruction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInstruction.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelInstruction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(120)))), ((int)(((byte)(132)))));
            this.labelInstruction.Location = new System.Drawing.Point(0, 54);
            this.labelInstruction.Margin = new System.Windows.Forms.Padding(0);
            this.labelInstruction.Name = "labelInstruction";
            this.labelInstruction.Size = new System.Drawing.Size(842, 44);
            this.labelInstruction.TabIndex = 1;
            this.labelInstruction.Text = "labelInstruction";
            this.labelInstruction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leftFlow
            // 
            this.leftFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.leftFlow.Controls.Add(this.comboBulk);
            this.leftFlow.Controls.Add(this.btnBulkApply);
            this.leftFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFlow.Location = new System.Drawing.Point(0, 98);
            this.leftFlow.Margin = new System.Windows.Forms.Padding(0);
            this.leftFlow.Name = "leftFlow";
            this.leftFlow.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.leftFlow.Size = new System.Drawing.Size(842, 42);
            this.leftFlow.TabIndex = 2;
            this.leftFlow.WrapContents = false;
            // 
            // comboBulk
            // 
            this.comboBulk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBulk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBulk.FormattingEnabled = true;
            this.comboBulk.Location = new System.Drawing.Point(0, 8);
            this.comboBulk.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.comboBulk.Name = "comboBulk";
            this.comboBulk.Size = new System.Drawing.Size(190, 25);
            this.comboBulk.TabIndex = 0;
            // 
            // btnBulkApply
            // 
            this.btnBulkApply.BackColor = System.Drawing.Color.White;
            this.btnBulkApply.CornerRadius = 6;
            this.btnBulkApply.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBulkApply.FlatAppearance.BorderSize = 0;
            this.btnBulkApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBulkApply.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBulkApply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.btnBulkApply.Location = new System.Drawing.Point(200, 4);
            this.btnBulkApply.Margin = new System.Windows.Forms.Padding(0);
            this.btnBulkApply.Name = "btnBulkApply";
            this.btnBulkApply.Size = new System.Drawing.Size(150, 34);
            this.btnBulkApply.TabIndex = 1;
            this.btnBulkApply.Text = "btnBulkApply";
            this.btnBulkApply.UseVisualStyleBackColor = false;
            this.btnBulkApply.Click += new System.EventHandler(this.bulkApplyBtn_Click);
            // 
            // panelGridHost
            // 
            this.panelGridHost.BackColor = System.Drawing.Color.White;
            this.panelGridHost.Controls.Add(this.dataGridReview);
            this.panelGridHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridHost.Location = new System.Drawing.Point(0, 140);
            this.panelGridHost.Margin = new System.Windows.Forms.Padding(0);
            this.panelGridHost.Name = "panelGridHost";
            this.panelGridHost.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridHost.Size = new System.Drawing.Size(842, 434);
            this.panelGridHost.TabIndex = 2;
            this.panelGridHost.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGridHost_Paint);
            // 
            // dataGridReview
            // 
            this.dataGridReview.AllowUserToAddRows = false;
            this.dataGridReview.AllowUserToDeleteRows = false;
            this.dataGridReview.AllowUserToResizeRows = false;
            this.dataGridReview.BackgroundColor = System.Drawing.Color.White;
            this.dataGridReview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridReview.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridReview.ColumnHeadersHeight = 36;
            this.dataGridReview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridReview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridReview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(243)))));
            this.dataGridReview.Location = new System.Drawing.Point(1, 1);
            this.dataGridReview.MultiSelect = false;
            this.dataGridReview.Name = "dataGridReview";
            this.dataGridReview.RowHeadersVisible = false;
            this.dataGridReview.RowTemplate.Height = 30;
            this.dataGridReview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridReview.Size = new System.Drawing.Size(840, 432);
            this.dataGridReview.TabIndex = 0;
            this.dataGridReview.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridReview_CellValueChanged);
            this.dataGridReview.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridReview_CurrentCellDirtyStateChanged);
            this.dataGridReview.SelectionChanged += new System.EventHandler(this.dataGridReview_SelectionChanged);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.rightFlow);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 574);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(0);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(842, 60);
            this.bottomPanel.TabIndex = 4;
            // 
            // rightFlow
            // 
            this.rightFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rightFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rightFlow.Controls.Add(this.btnSkipAll);
            this.rightFlow.Controls.Add(this.btnContinue);
            this.rightFlow.Controls.Add(this.btnApply);
            this.rightFlow.Location = new System.Drawing.Point(258, 12);
            this.rightFlow.Margin = new System.Windows.Forms.Padding(0);
            this.rightFlow.Name = "rightFlow";
            this.rightFlow.Size = new System.Drawing.Size(570, 38);
            this.rightFlow.TabIndex = 1;
            this.rightFlow.WrapContents = false;
            // 
            // btnSkipAll
            // 
            this.btnSkipAll.BackColor = System.Drawing.Color.White;
            this.btnSkipAll.CornerRadius = 6;
            this.btnSkipAll.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSkipAll.FlatAppearance.BorderSize = 0;
            this.btnSkipAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkipAll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSkipAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.btnSkipAll.Location = new System.Drawing.Point(10, 0);
            this.btnSkipAll.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSkipAll.Name = "btnSkipAll";
            this.btnSkipAll.Size = new System.Drawing.Size(150, 38);
            this.btnSkipAll.TabIndex = 2;
            this.btnSkipAll.Text = "btnSkipAll";
            this.btnSkipAll.UseVisualStyleBackColor = false;
            this.btnSkipAll.Click += new System.EventHandler(this.btnSkipAll_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.BackColor = System.Drawing.Color.White;
            this.btnContinue.CornerRadius = 6;
            this.btnContinue.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnContinue.FlatAppearance.BorderSize = 0;
            this.btnContinue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContinue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnContinue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(36)))), ((int)(((byte)(45)))));
            this.btnContinue.Location = new System.Drawing.Point(170, 0);
            this.btnContinue.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(240, 38);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "btnContinue";
            this.btnContinue.UseVisualStyleBackColor = false;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.btnApply.CornerRadius = 6;
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(108)))), ((int)(((byte)(223)))));
            this.btnApply.FlatAppearance.BorderSize = 0;
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApply.ForeColor = System.Drawing.Color.White;
            this.btnApply.Location = new System.Drawing.Point(420, 0);
            this.btnApply.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(140, 38);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "btnApply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ThreatReviewPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.rootLayout);
            this.Name = "ThreatReviewPage";
            this.Padding = new System.Windows.Forms.Padding(26, 20, 26, 20);
            this.Size = new System.Drawing.Size(894, 674);
            this.rootLayout.ResumeLayout(false);
            this.leftFlow.ResumeLayout(false);
            this.panelGridHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReview)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.rightFlow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel rootLayout;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelInstruction;
        private System.Windows.Forms.Panel panelGridHost;
        private System.Windows.Forms.DataGridView dataGridReview;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.FlowLayoutPanel rightFlow;
        private System.Windows.Forms.FlowLayoutPanel leftFlow;
        private System.Windows.Forms.ComboBox comboBulk;
        private MSearch.UI.RoundButton btnApply;
        private MSearch.UI.RoundButton btnContinue;
        private MSearch.UI.RoundButton btnSkipAll;
        private MSearch.UI.RoundButton btnBulkApply;
    }
}
