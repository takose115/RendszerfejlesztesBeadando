namespace WindowsFormsApp1
{
    partial class comment
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
            this.but_cancel = new System.Windows.Forms.Button();
            this.but_comment = new System.Windows.Forms.Button();
            this.lab_title = new System.Windows.Forms.Label();
            this.lab_desc = new System.Windows.Forms.Label();
            this.txt_comment = new System.Windows.Forms.TextBox();
            this.lab_titok = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // but_cancel
            // 
            this.but_cancel.Location = new System.Drawing.Point(2458, 1126);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(87, 38);
            this.but_cancel.TabIndex = 0;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
            // 
            // but_comment
            // 
            this.but_comment.Location = new System.Drawing.Point(2323, 1126);
            this.but_comment.Name = "but_comment";
            this.but_comment.Size = new System.Drawing.Size(101, 38);
            this.but_comment.TabIndex = 1;
            this.but_comment.Text = "Send";
            this.but_comment.UseVisualStyleBackColor = true;
            this.but_comment.Click += new System.EventHandler(this.but_comment_Click);
            // 
            // lab_title
            // 
            this.lab_title.Location = new System.Drawing.Point(48, 42);
            this.lab_title.Name = "lab_title";
            this.lab_title.Size = new System.Drawing.Size(2206, 43);
            this.lab_title.TabIndex = 2;
            this.lab_title.Text = "label1";
            // 
            // lab_desc
            // 
            this.lab_desc.Location = new System.Drawing.Point(48, 85);
            this.lab_desc.Name = "lab_desc";
            this.lab_desc.Size = new System.Drawing.Size(2395, 207);
            this.lab_desc.TabIndex = 3;
            this.lab_desc.Text = "label2";
            // 
            // txt_comment
            // 
            this.txt_comment.Location = new System.Drawing.Point(52, 1132);
            this.txt_comment.Name = "txt_comment";
            this.txt_comment.Size = new System.Drawing.Size(2234, 26);
            this.txt_comment.TabIndex = 4;
            // 
            // lab_titok
            // 
            this.lab_titok.AutoSize = true;
            this.lab_titok.Location = new System.Drawing.Point(2477, 1044);
            this.lab_titok.Name = "lab_titok";
            this.lab_titok.Size = new System.Drawing.Size(51, 20);
            this.lab_titok.TabIndex = 5;
            this.lab_titok.Text = "label1";
            this.lab_titok.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.66296F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.33704F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(52, 307);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(2451, 97);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2582, 1197);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lab_titok);
            this.Controls.Add(this.txt_comment);
            this.Controls.Add(this.lab_desc);
            this.Controls.Add(this.lab_title);
            this.Controls.Add(this.but_comment);
            this.Controls.Add(this.but_cancel);
            this.Name = "comment";
            this.Text = "/";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_cancel;
        private System.Windows.Forms.Button but_comment;
        private System.Windows.Forms.Label lab_title;
        private System.Windows.Forms.Label lab_desc;
        private System.Windows.Forms.TextBox txt_comment;
        private System.Windows.Forms.Label lab_titok;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}