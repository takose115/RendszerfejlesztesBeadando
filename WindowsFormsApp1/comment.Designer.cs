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
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
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
            this.lab_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_title.Location = new System.Drawing.Point(48, 42);
            this.lab_title.Name = "lab_title";
            this.lab_title.Size = new System.Drawing.Size(2206, 67);
            this.lab_title.TabIndex = 2;
            this.lab_title.Text = "label1";
            // 
            // lab_desc
            // 
            this.lab_desc.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_desc.Location = new System.Drawing.Point(50, 109);
            this.lab_desc.Name = "lab_desc";
            this.lab_desc.Size = new System.Drawing.Size(2455, 111);
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
            // panel
            // 
            this.panel.AllowDrop = true;
            this.panel.AutoScroll = true;
            this.panel.AutoSize = true;
            this.panel.ColumnCount = 3;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.82832F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.17168F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 289F));
            this.panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panel.Location = new System.Drawing.Point(52, 316);
            this.panel.Name = "panel";
            this.panel.RowCount = 2;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.36082F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.63918F));
            this.panel.Size = new System.Drawing.Size(2451, 102);
            this.panel.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(52, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "Comments:";
            // 
            // comment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2582, 1197);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.lab_titok);
            this.Controls.Add(this.txt_comment);
            this.Controls.Add(this.lab_desc);
            this.Controls.Add(this.lab_title);
            this.Controls.Add(this.but_comment);
            this.Controls.Add(this.but_cancel);
            this.Name = "comment";
            this.Text = "/";
            this.Load += new System.EventHandler(this.comment_Load);
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
        private System.Windows.Forms.TableLayoutPanel panel;
        internal System.Windows.Forms.Label label1;
    }
}