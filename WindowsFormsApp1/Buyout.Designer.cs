namespace WindowsFormsApp1
{
    partial class Buyout
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
            this.TermekNevLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.txtKartyaSzam = new System.Windows.Forms.TextBox();
            this.txtCardMonth = new System.Windows.Forms.TextBox();
            this.txtCVC = new System.Windows.Forms.TextBox();
            this.txtCardYear = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TermekNevLabel
            // 
            this.TermekNevLabel.AutoSize = true;
            this.TermekNevLabel.Location = new System.Drawing.Point(53, 18);
            this.TermekNevLabel.Name = "TermekNevLabel";
            this.TermekNevLabel.Size = new System.Drawing.Size(0, 13);
            this.TermekNevLabel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Card Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "MM/YY";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CVC:";
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(56, 183);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmBtn.TabIndex = 4;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(150, 182);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // txtKartyaSzam
            // 
            this.txtKartyaSzam.Location = new System.Drawing.Point(83, 53);
            this.txtKartyaSzam.Name = "txtKartyaSzam";
            this.txtKartyaSzam.Size = new System.Drawing.Size(294, 20);
            this.txtKartyaSzam.TabIndex = 6;
            // 
            // txtCardMonth
            // 
            this.txtCardMonth.Location = new System.Drawing.Point(56, 123);
            this.txtCardMonth.Name = "txtCardMonth";
            this.txtCardMonth.Size = new System.Drawing.Size(32, 20);
            this.txtCardMonth.TabIndex = 7;
            // 
            // txtCVC
            // 
            this.txtCVC.Location = new System.Drawing.Point(277, 122);
            this.txtCVC.Name = "txtCVC";
            this.txtCVC.Size = new System.Drawing.Size(100, 20);
            this.txtCVC.TabIndex = 8;
            // 
            // txtCardYear
            // 
            this.txtCardYear.Location = new System.Drawing.Point(112, 124);
            this.txtCardYear.Name = "txtCardYear";
            this.txtCardYear.Size = new System.Drawing.Size(30, 20);
            this.txtCardYear.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "/";
            // 
            // Buyout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 319);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCardYear);
            this.Controls.Add(this.txtCVC);
            this.Controls.Add(this.txtCardMonth);
            this.Controls.Add(this.txtKartyaSzam);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TermekNevLabel);
            this.Name = "Buyout";
            this.Text = "Buyout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TermekNevLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.TextBox txtKartyaSzam;
        private System.Windows.Forms.TextBox txtCardMonth;
        private System.Windows.Forms.TextBox txtCVC;
        private System.Windows.Forms.TextBox txtCardYear;
        private System.Windows.Forms.Label label5;
    }
}