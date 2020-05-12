namespace WindowsFormsApp1
{
    partial class Index
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
            this.SignOutBtn = new System.Windows.Forms.Button();
            this.AddItemBtn = new System.Windows.Forms.Button();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.but_forum = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignOutBtn
            // 
            this.SignOutBtn.Location = new System.Drawing.Point(983, 53);
            this.SignOutBtn.Name = "SignOutBtn";
            this.SignOutBtn.Size = new System.Drawing.Size(75, 23);
            this.SignOutBtn.TabIndex = 0;
            this.SignOutBtn.Text = "Sign out";
            this.SignOutBtn.UseVisualStyleBackColor = true;
            this.SignOutBtn.Click += new System.EventHandler(this.SignOutBtn_Click);
            // 
            // AddItemBtn
            // 
            this.AddItemBtn.Location = new System.Drawing.Point(983, 126);
            this.AddItemBtn.Name = "AddItemBtn";
            this.AddItemBtn.Size = new System.Drawing.Size(75, 23);
            this.AddItemBtn.TabIndex = 1;
            this.AddItemBtn.Text = "Add Item";
            this.AddItemBtn.UseVisualStyleBackColor = true;
            this.AddItemBtn.Click += new System.EventHandler(this.AddItemBtn_Click);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(974, 230);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(93, 20);
            this.SearchTextBox.TabIndex = 3;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(983, 264);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.ColumnCount = 10;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.85254F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.721077F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.009011F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090913F));
            this.panel.Location = new System.Drawing.Point(36, 53);
            this.panel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panel.Name = "panel";
            this.panel.RowCount = 1;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel.Size = new System.Drawing.Size(885, 519);
            this.panel.TabIndex = 0;
            // 
            // but_forum
            // 
            this.but_forum.Location = new System.Drawing.Point(983, 168);
            this.but_forum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.but_forum.Name = "but_forum";
            this.but_forum.Size = new System.Drawing.Size(75, 22);
            this.but_forum.TabIndex = 5;
            this.but_forum.Text = "Forum";
            this.but_forum.UseVisualStyleBackColor = true;
            this.but_forum.Click += new System.EventHandler(this.but_forum_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(965, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Subscribe to topic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 616);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.but_forum);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchTextBox);
            this.Controls.Add(this.AddItemBtn);
            this.Controls.Add(this.SignOutBtn);
            this.Name = "Index";
            this.Text = "Index";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SignOutBtn;
        private System.Windows.Forms.Button AddItemBtn;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Button but_forum;
        private System.Windows.Forms.Button button1;
    }
}