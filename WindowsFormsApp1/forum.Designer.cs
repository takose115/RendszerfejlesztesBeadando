namespace WindowsFormsApp1
{
    partial class forum
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
            this.but_newtopic = new System.Windows.Forms.Button();
            this.but_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // but_newtopic
            // 
            this.but_newtopic.Location = new System.Drawing.Point(556, 400);
            this.but_newtopic.Name = "but_newtopic";
            this.but_newtopic.Size = new System.Drawing.Size(104, 38);
            this.but_newtopic.TabIndex = 0;
            this.but_newtopic.Text = "New";
            this.but_newtopic.UseVisualStyleBackColor = true;
            this.but_newtopic.Click += new System.EventHandler(this.but_newtopic_Click);
            // 
            // but_cancel
            // 
            this.but_cancel.Location = new System.Drawing.Point(682, 400);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(97, 38);
            this.but_cancel.TabIndex = 1;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
            // 
            // forum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_newtopic);
            this.Name = "forum";
            this.Text = "forum";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button but_newtopic;
        private System.Windows.Forms.Button but_cancel;
    }
}