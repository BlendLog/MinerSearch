
namespace MSearch
{
    partial class Finish
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LBL_ScanComplete = new System.Windows.Forms.Label();
            this.LBL_scanElapsedTime = new System.Windows.Forms.Label();
            this.LBL_ScanTime = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Label_SuspiciousObjectsCount = new System.Windows.Forms.Label();
            this.Label_suspiciousObjects = new System.Windows.Forms.Label();
            this.LBL_curedCount = new System.Windows.Forms.Label();
            this.LBL_totalThreats = new System.Windows.Forms.Label();
            this.LBL_threatsCount = new System.Windows.Forms.Label();
            this.LabelSeparator = new System.Windows.Forms.Label();
            this.LBL_neutralizedThreats = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Label_OpenLogsFolder = new System.Windows.Forms.LinkLabel();
            this.Label_showAllLogs = new System.Windows.Forms.Label();
            this.btnDetails = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.FinalStatus_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35.29412F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 64.70588F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 429);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 43);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(649, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 99;
            this.button1.Text = "X";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // top
            // 
            this.top.BackColor = System.Drawing.Color.RoyalBlue;
            this.top.Dock = System.Windows.Forms.DockStyle.Top;
            this.top.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.top.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.top.ForeColor = System.Drawing.Color.White;
            this.top.Location = new System.Drawing.Point(0, 0);
            this.top.Margin = new System.Windows.Forms.Padding(0);
            this.top.Name = "top";
            this.top.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.top.Size = new System.Drawing.Size(684, 39);
            this.top.TabIndex = 1;
            this.top.Text = "MinerSearch          ";
            this.top.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.top.MouseDown += new System.Windows.Forms.MouseEventHandler(this.top_MouseDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.LBL_ScanComplete);
            this.panel3.Controls.Add(this.LBL_scanElapsedTime);
            this.panel3.Controls.Add(this.LBL_ScanTime);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.ForeColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(5, 46);
            this.panel3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(674, 75);
            this.panel3.TabIndex = 5;
            // 
            // LBL_ScanComplete
            // 
            this.LBL_ScanComplete.BackColor = System.Drawing.Color.Transparent;
            this.LBL_ScanComplete.Dock = System.Windows.Forms.DockStyle.Top;
            this.LBL_ScanComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_ScanComplete.Font = new System.Drawing.Font("Trebuchet MS", 14F, System.Drawing.FontStyle.Bold);
            this.LBL_ScanComplete.ForeColor = System.Drawing.Color.Black;
            this.LBL_ScanComplete.Location = new System.Drawing.Point(0, 0);
            this.LBL_ScanComplete.Margin = new System.Windows.Forms.Padding(0);
            this.LBL_ScanComplete.Name = "LBL_ScanComplete";
            this.LBL_ScanComplete.Size = new System.Drawing.Size(672, 42);
            this.LBL_ScanComplete.TabIndex = 3;
            this.LBL_ScanComplete.Text = "Сканирование завершено";
            this.LBL_ScanComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_scanElapsedTime
            // 
            this.LBL_scanElapsedTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_scanElapsedTime.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold);
            this.LBL_scanElapsedTime.ForeColor = System.Drawing.Color.Black;
            this.LBL_scanElapsedTime.Location = new System.Drawing.Point(380, 42);
            this.LBL_scanElapsedTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_scanElapsedTime.Name = "LBL_scanElapsedTime";
            this.LBL_scanElapsedTime.Size = new System.Drawing.Size(293, 20);
            this.LBL_scanElapsedTime.TabIndex = 8;
            this.LBL_scanElapsedTime.Text = "00:00:00:000";
            this.LBL_scanElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBL_ScanTime
            // 
            this.LBL_ScanTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_ScanTime.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold);
            this.LBL_ScanTime.ForeColor = System.Drawing.Color.Black;
            this.LBL_ScanTime.Location = new System.Drawing.Point(2, 42);
            this.LBL_ScanTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_ScanTime.Name = "LBL_ScanTime";
            this.LBL_ScanTime.Size = new System.Drawing.Size(339, 20);
            this.LBL_ScanTime.TabIndex = 5;
            this.LBL_ScanTime.Text = "Время сканирования:";
            this.LBL_ScanTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.77778F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.22222F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 121);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(684, 213);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.Label_SuspiciousObjectsCount);
            this.panel2.Controls.Add(this.Label_suspiciousObjects);
            this.panel2.Controls.Add(this.LBL_curedCount);
            this.panel2.Controls.Add(this.LBL_totalThreats);
            this.panel2.Controls.Add(this.LBL_threatsCount);
            this.panel2.Controls.Add(this.LabelSeparator);
            this.panel2.Controls.Add(this.LBL_neutralizedThreats);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(5, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 203);
            this.panel2.TabIndex = 0;
            // 
            // Label_SuspiciousObjectsCount
            // 
            this.Label_SuspiciousObjectsCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label_SuspiciousObjectsCount.AutoSize = true;
            this.Label_SuspiciousObjectsCount.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label_SuspiciousObjectsCount.ForeColor = System.Drawing.Color.Black;
            this.Label_SuspiciousObjectsCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_SuspiciousObjectsCount.Location = new System.Drawing.Point(285, 64);
            this.Label_SuspiciousObjectsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_SuspiciousObjectsCount.Name = "Label_SuspiciousObjectsCount";
            this.Label_SuspiciousObjectsCount.Size = new System.Drawing.Size(21, 20);
            this.Label_SuspiciousObjectsCount.TabIndex = 9;
            this.Label_SuspiciousObjectsCount.Text = "0";
            this.Label_SuspiciousObjectsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_suspiciousObjects
            // 
            this.Label_suspiciousObjects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label_suspiciousObjects.AutoSize = true;
            this.Label_suspiciousObjects.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.Label_suspiciousObjects.ForeColor = System.Drawing.Color.Black;
            this.Label_suspiciousObjects.Location = new System.Drawing.Point(13, 64);
            this.Label_suspiciousObjects.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label_suspiciousObjects.Name = "Label_suspiciousObjects";
            this.Label_suspiciousObjects.Size = new System.Drawing.Size(135, 20);
            this.Label_suspiciousObjects.TabIndex = 8;
            this.Label_suspiciousObjects.Text = "Подозрений:";
            this.Label_suspiciousObjects.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBL_curedCount
            // 
            this.LBL_curedCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_curedCount.AutoSize = true;
            this.LBL_curedCount.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.LBL_curedCount.ForeColor = System.Drawing.Color.Black;
            this.LBL_curedCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LBL_curedCount.Location = new System.Drawing.Point(285, 147);
            this.LBL_curedCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_curedCount.Name = "LBL_curedCount";
            this.LBL_curedCount.Size = new System.Drawing.Size(24, 23);
            this.LBL_curedCount.TabIndex = 7;
            this.LBL_curedCount.Text = "0";
            this.LBL_curedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_totalThreats
            // 
            this.LBL_totalThreats.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_totalThreats.AutoSize = true;
            this.LBL_totalThreats.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.LBL_totalThreats.ForeColor = System.Drawing.Color.Crimson;
            this.LBL_totalThreats.Location = new System.Drawing.Point(13, 31);
            this.LBL_totalThreats.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_totalThreats.Name = "LBL_totalThreats";
            this.LBL_totalThreats.Size = new System.Drawing.Size(160, 20);
            this.LBL_totalThreats.TabIndex = 1;
            this.LBL_totalThreats.Text = "Найдено угроз:";
            this.LBL_totalThreats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LBL_threatsCount
            // 
            this.LBL_threatsCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_threatsCount.AutoSize = true;
            this.LBL_threatsCount.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold);
            this.LBL_threatsCount.ForeColor = System.Drawing.Color.Crimson;
            this.LBL_threatsCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LBL_threatsCount.Location = new System.Drawing.Point(285, 31);
            this.LBL_threatsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_threatsCount.Name = "LBL_threatsCount";
            this.LBL_threatsCount.Size = new System.Drawing.Size(21, 20);
            this.LBL_threatsCount.TabIndex = 6;
            this.LBL_threatsCount.Text = "0";
            this.LBL_threatsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSeparator
            // 
            this.LabelSeparator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LabelSeparator.AutoSize = true;
            this.LabelSeparator.BackColor = System.Drawing.Color.Transparent;
            this.LabelSeparator.Font = new System.Drawing.Font("Verdana", 10.2F);
            this.LabelSeparator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LabelSeparator.Location = new System.Drawing.Point(-21, 96);
            this.LabelSeparator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSeparator.Name = "LabelSeparator";
            this.LabelSeparator.Size = new System.Drawing.Size(372, 20);
            this.LabelSeparator.TabIndex = 4;
            this.LabelSeparator.Text = "_________________________________";
            // 
            // LBL_neutralizedThreats
            // 
            this.LBL_neutralizedThreats.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LBL_neutralizedThreats.AutoSize = true;
            this.LBL_neutralizedThreats.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.LBL_neutralizedThreats.ForeColor = System.Drawing.Color.Black;
            this.LBL_neutralizedThreats.Location = new System.Drawing.Point(12, 147);
            this.LBL_neutralizedThreats.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBL_neutralizedThreats.Name = "LBL_neutralizedThreats";
            this.LBL_neutralizedThreats.Size = new System.Drawing.Size(199, 23);
            this.LBL_neutralizedThreats.TabIndex = 0;
            this.LBL_neutralizedThreats.Text = "Устранено угроз:";
            this.LBL_neutralizedThreats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Gainsboro;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.Label_OpenLogsFolder);
            this.panel4.Controls.Add(this.Label_showAllLogs);
            this.panel4.Controls.Add(this.btnDetails);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.ForeColor = System.Drawing.Color.Transparent;
            this.panel4.Location = new System.Drawing.Point(365, 5);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 5, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(314, 203);
            this.panel4.TabIndex = 1;
            // 
            // Label_OpenLogsFolder
            // 
            this.Label_OpenLogsFolder.ActiveLinkColor = System.Drawing.Color.Navy;
            this.Label_OpenLogsFolder.BackColor = System.Drawing.Color.Transparent;
            this.Label_OpenLogsFolder.DisabledLinkColor = System.Drawing.Color.Gray;
            this.Label_OpenLogsFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Label_OpenLogsFolder.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.Label_OpenLogsFolder.LinkColor = System.Drawing.Color.RoyalBlue;
            this.Label_OpenLogsFolder.Location = new System.Drawing.Point(3, 137);
            this.Label_OpenLogsFolder.Name = "Label_OpenLogsFolder";
            this.Label_OpenLogsFolder.Size = new System.Drawing.Size(322, 23);
            this.Label_OpenLogsFolder.TabIndex = 11;
            this.Label_OpenLogsFolder.TabStop = true;
            this.Label_OpenLogsFolder.Text = "C:\\_MinerSearchLogs";
            this.Label_OpenLogsFolder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_OpenLogsFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label_OpenLogsFolder_LinkClicked);
            // 
            // Label_showAllLogs
            // 
            this.Label_showAllLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Label_showAllLogs.ForeColor = System.Drawing.Color.Black;
            this.Label_showAllLogs.Location = new System.Drawing.Point(7, 112);
            this.Label_showAllLogs.Name = "Label_showAllLogs";
            this.Label_showAllLogs.Size = new System.Drawing.Size(318, 21);
            this.Label_showAllLogs.TabIndex = 10;
            this.Label_showAllLogs.Text = "Посмотреть все отчёты в папке:";
            this.Label_showAllLogs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDetails
            // 
            this.btnDetails.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDetails.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnDetails.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDetails.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Navy;
            this.btnDetails.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnDetails.ForeColor = System.Drawing.Color.AliceBlue;
            this.btnDetails.Location = new System.Drawing.Point(58, 46);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(199, 53);
            this.btnDetails.TabIndex = 9;
            this.btnDetails.Text = "Открыть отчёт";
            this.btnDetails.UseVisualStyleBackColor = false;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.FinalStatus_label);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(5, 334);
            this.panel5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(674, 90);
            this.panel5.TabIndex = 6;
            // 
            // FinalStatus_label
            // 
            this.FinalStatus_label.BackColor = System.Drawing.Color.Gainsboro;
            this.FinalStatus_label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FinalStatus_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinalStatus_label.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.FinalStatus_label.ForeColor = System.Drawing.Color.Black;
            this.FinalStatus_label.Location = new System.Drawing.Point(0, 0);
            this.FinalStatus_label.Margin = new System.Windows.Forms.Padding(0);
            this.FinalStatus_label.Name = "FinalStatus_label";
            this.FinalStatus_label.Size = new System.Drawing.Size(674, 90);
            this.FinalStatus_label.TabIndex = 2;
            this.FinalStatus_label.Text = "-----------------------------------";
            this.FinalStatus_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Finish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 429);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Finish";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Finish_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LBL_totalThreats;
        private System.Windows.Forms.Label LBL_neutralizedThreats;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LBL_ScanComplete;
        private System.Windows.Forms.Label LBL_ScanTime;
        private System.Windows.Forms.Label LabelSeparator;
        private System.Windows.Forms.Label top;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LBL_curedCount;
        private System.Windows.Forms.Label LBL_threatsCount;
        private System.Windows.Forms.Label LBL_scanElapsedTime;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label FinalStatus_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label Label_SuspiciousObjectsCount;
        private System.Windows.Forms.Label Label_suspiciousObjects;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.LinkLabel Label_OpenLogsFolder;
        private System.Windows.Forms.Label Label_showAllLogs;
    }
}