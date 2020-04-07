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
            this.listview_items = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // SignOutBtn
            // 
            this.SignOutBtn.Location = new System.Drawing.Point(668, 48);
            this.SignOutBtn.Name = "SignOutBtn";
            this.SignOutBtn.Size = new System.Drawing.Size(75, 23);
            this.SignOutBtn.TabIndex = 0;
            this.SignOutBtn.Text = "Sign out";
            this.SignOutBtn.UseVisualStyleBackColor = true;
            this.SignOutBtn.Click += new System.EventHandler(this.SignOutBtn_Click);
            // 
            // AddItemBtn
            // 
            this.AddItemBtn.Location = new System.Drawing.Point(668, 117);
            this.AddItemBtn.Name = "AddItemBtn";
            this.AddItemBtn.Size = new System.Drawing.Size(75, 23);
            this.AddItemBtn.TabIndex = 1;
            this.AddItemBtn.Text = "Add Item";
            this.AddItemBtn.UseVisualStyleBackColor = true;
            this.AddItemBtn.Click += new System.EventHandler(this.AddItemBtn_Click);
            // 
            // listview_items
            // 
            this.listview_items.HideSelection = false;
            this.listview_items.Location = new System.Drawing.Point(31, 48);
            this.listview_items.Name = "listview_items";
            this.listview_items.Size = new System.Drawing.Size(610, 367);
            this.listview_items.TabIndex = 2;
            this.listview_items.UseCompatibleStateImageBehavior = false;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listview_items);
            this.Controls.Add(this.AddItemBtn);
            this.Controls.Add(this.SignOutBtn);
            this.Name = "Index";
            this.Text = "Index";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignOutBtn;
        private System.Windows.Forms.Button AddItemBtn;
        private System.Windows.Forms.ListView listview_items;
    }
}