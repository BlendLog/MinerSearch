
namespace MSearch
{
    partial class License
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Accept_btn = new System.Windows.Forms.Button();
            this.Exit_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.Black;
            this.richTextBox1.Location = new System.Drawing.Point(13, 13);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(826, 248);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // Accept_btn
            // 
            this.Accept_btn.BackColor = System.Drawing.Color.White;
            this.Accept_btn.FlatAppearance.BorderSize = 0;
            this.Accept_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Accept_btn.Location = new System.Drawing.Point(270, 273);
            this.Accept_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Accept_btn.Name = "Accept_btn";
            this.Accept_btn.Size = new System.Drawing.Size(131, 56);
            this.Accept_btn.TabIndex = 2;
            this.Accept_btn.Text = "Accept";
            this.Accept_btn.UseVisualStyleBackColor = false;
            this.Accept_btn.Click += new System.EventHandler(this.Accept_btn_Click);
            // 
            // Exit_btn
            // 
            this.Exit_btn.BackColor = System.Drawing.Color.White;
            this.Exit_btn.FlatAppearance.BorderSize = 0;
            this.Exit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Exit_btn.Location = new System.Drawing.Point(409, 273);
            this.Exit_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.Size = new System.Drawing.Size(131, 56);
            this.Exit_btn.TabIndex = 1;
            this.Exit_btn.Text = "Exit";
            this.Exit_btn.UseVisualStyleBackColor = false;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(854, 342);
            this.Controls.Add(this.Exit_btn);
            this.Controls.Add(this.Accept_btn);
            this.Controls.Add(this.richTextBox1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "License";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "License";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.Button Accept_btn;
        public System.Windows.Forms.Button Exit_btn;
    }
}