namespace WindowsFormsApp1
{
    partial class AddItem
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
            this.BackBtn = new System.Windows.Forms.Button();
            this.SendBtn = new System.Windows.Forms.Button();
            this.Lab_name = new System.Windows.Forms.Label();
            this.lab_startbid = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lab_buyout = new System.Windows.Forms.Label();
            this.lab_end_date = new System.Windows.Forms.Label();
            this.lab_type = new System.Windows.Forms.Label();
            this.text_name = new System.Windows.Forms.TextBox();
            this.date_ending = new System.Windows.Forms.DateTimePicker();
            this.num_start = new System.Windows.Forms.NumericUpDown();
            this.num_buyout = new System.Windows.Forms.NumericUpDown();
            this.list_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_buyout)).BeginInit();
            this.SuspendLayout();
            // 
            // BackBtn
            // 
            this.BackBtn.Location = new System.Drawing.Point(406, 463);
            this.BackBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(112, 35);
            this.BackBtn.TabIndex = 0;
            this.BackBtn.Text = "Cancel";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(538, 463);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(112, 35);
            this.SendBtn.TabIndex = 1;
            this.SendBtn.Text = "Upload";
            this.SendBtn.UseVisualStyleBackColor = true;
            // 
            // Lab_name
            // 
            this.Lab_name.AutoSize = true;
            this.Lab_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Lab_name.Location = new System.Drawing.Point(47, 55);
            this.Lab_name.Name = "Lab_name";
            this.Lab_name.Size = new System.Drawing.Size(199, 36);
            this.Lab_name.TabIndex = 2;
            this.Lab_name.Text = "Product name";
            // 
            // lab_startbid
            // 
            this.lab_startbid.AutoSize = true;
            this.lab_startbid.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_startbid.Location = new System.Drawing.Point(47, 130);
            this.lab_startbid.Name = "lab_startbid";
            this.lab_startbid.Size = new System.Drawing.Size(167, 36);
            this.lab_startbid.TabIndex = 3;
            this.lab_startbid.Text = "Starting bid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 4;
            // 
            // lab_buyout
            // 
            this.lab_buyout.AutoSize = true;
            this.lab_buyout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_buyout.Location = new System.Drawing.Point(47, 214);
            this.lab_buyout.Name = "lab_buyout";
            this.lab_buyout.Size = new System.Drawing.Size(158, 36);
            this.lab_buyout.TabIndex = 5;
            this.lab_buyout.Text = "Buyout bid";
            // 
            // lab_end_date
            // 
            this.lab_end_date.AutoSize = true;
            this.lab_end_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_end_date.Location = new System.Drawing.Point(47, 292);
            this.lab_end_date.Name = "lab_end_date";
            this.lab_end_date.Size = new System.Drawing.Size(175, 36);
            this.lab_end_date.TabIndex = 6;
            this.lab_end_date.Text = "Ending date";
            // 
            // lab_type
            // 
            this.lab_type.AutoSize = true;
            this.lab_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lab_type.Location = new System.Drawing.Point(47, 365);
            this.lab_type.Name = "lab_type";
            this.lab_type.Size = new System.Drawing.Size(81, 36);
            this.lab_type.TabIndex = 7;
            this.lab_type.Text = "Type";
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(338, 65);
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(312, 26);
            this.text_name.TabIndex = 8;
            // 
            // date_ending
            // 
            this.date_ending.Location = new System.Drawing.Point(338, 292);
            this.date_ending.Name = "date_ending";
            this.date_ending.Size = new System.Drawing.Size(312, 26);
            this.date_ending.TabIndex = 13;
            // 
            // num_start
            // 
            this.num_start.Location = new System.Drawing.Point(509, 140);
            this.num_start.Name = "num_start";
            this.num_start.Size = new System.Drawing.Size(107, 26);
            this.num_start.TabIndex = 14;
            this.num_start.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // num_buyout
            // 
            this.num_buyout.Location = new System.Drawing.Point(509, 214);
            this.num_buyout.Name = "num_buyout";
            this.num_buyout.Size = new System.Drawing.Size(107, 26);
            this.num_buyout.TabIndex = 15;
            this.num_buyout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // list_type
            // 
            this.list_type.FormattingEnabled = true;
            this.list_type.Location = new System.Drawing.Point(338, 365);
            this.list_type.Name = "list_type";
            this.list_type.Size = new System.Drawing.Size(312, 28);
            this.list_type.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(622, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 29);
            this.label1.TabIndex = 17;
            this.label1.Text = "Ft";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(622, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 29);
            this.label2.TabIndex = 18;
            this.label2.Text = "Ft";
            // 
            // AddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 564);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.list_type);
            this.Controls.Add(this.num_buyout);
            this.Controls.Add(this.num_start);
            this.Controls.Add(this.date_ending);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.lab_type);
            this.Controls.Add(this.lab_end_date);
            this.Controls.Add(this.lab_buyout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lab_startbid);
            this.Controls.Add(this.Lab_name);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.BackBtn);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AddItem";
            this.Text = "AddItem";
            ((System.ComponentModel.ISupportInitialize)(this.num_start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_buyout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label Lab_name;
        private System.Windows.Forms.Label lab_startbid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lab_buyout;
        private System.Windows.Forms.Label lab_end_date;
        private System.Windows.Forms.Label lab_type;
        private System.Windows.Forms.TextBox text_name;
        private System.Windows.Forms.DateTimePicker date_ending;
        private System.Windows.Forms.NumericUpDown num_start;
        private System.Windows.Forms.NumericUpDown num_buyout;
        private System.Windows.Forms.ComboBox list_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}