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
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // but_newtopic
            // 
            this.but_newtopic.Location = new System.Drawing.Point(1160, 538);
            this.but_newtopic.Name = "but_newtopic";
            this.but_newtopic.Size = new System.Drawing.Size(104, 38);
            this.but_newtopic.TabIndex = 0;
            this.but_newtopic.Text = "New";
            this.but_newtopic.UseVisualStyleBackColor = true;
            this.but_newtopic.Click += new System.EventHandler(this.but_newtopic_Click);
            // 
            // but_cancel
            // 
            this.but_cancel.Location = new System.Drawing.Point(1295, 538);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(97, 38);
            this.but_cancel.TabIndex = 1;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
            // 
            // panel
            // 
            this.panel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.panel.ColumnCount = 3;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.98387F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.01613F));
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.panel.Location = new System.Drawing.Point(36, 38);
            this.panel.Name = "panel";
            this.panel.RowCount = 2;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.2163F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.7837F));
            this.panel.Size = new System.Drawing.Size(1050, 174);
            this.panel.TabIndex = 2;
            // 
            // forum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 601);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_newtopic);
            this.Name = "forum";
            this.Text = "forum";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button but_newtopic;
        private System.Windows.Forms.Button but_cancel;
        private System.Windows.Forms.TableLayoutPanel panel;
    }
}