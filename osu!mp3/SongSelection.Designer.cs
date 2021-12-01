namespace osu_mp3
{
    partial class SongSelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongSelection));
            this.songCheckList = new System.Windows.Forms.CheckedListBox();
            this.SelectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.acceptbutton = new System.Windows.Forms.Button();
            this.cancelbutton = new System.Windows.Forms.Button();
            this.SongCount = new System.Windows.Forms.Label();
            this.searchbar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // songCheckList
            // 
            this.songCheckList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.songCheckList.CheckOnClick = true;
            this.songCheckList.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songCheckList.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.songCheckList.FormattingEnabled = true;
            this.songCheckList.Location = new System.Drawing.Point(14, 38);
            this.songCheckList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.songCheckList.Name = "songCheckList";
            this.songCheckList.Size = new System.Drawing.Size(411, 308);
            this.songCheckList.TabIndex = 0;
            this.songCheckList.ThreeDCheckBoxes = true;
            this.songCheckList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.songCheckList_ItemCheck);
            // 
            // SelectAllCheckBox
            // 
            this.SelectAllCheckBox.AutoSize = true;
            this.SelectAllCheckBox.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllCheckBox.Location = new System.Drawing.Point(17, 18);
            this.SelectAllCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SelectAllCheckBox.Name = "SelectAllCheckBox";
            this.SelectAllCheckBox.Size = new System.Drawing.Size(72, 19);
            this.SelectAllCheckBox.TabIndex = 1;
            this.SelectAllCheckBox.Text = "All Items";
            this.SelectAllCheckBox.UseVisualStyleBackColor = true;
            this.SelectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAllCheckBox_CheckedChanged);
            // 
            // acceptbutton
            // 
            this.acceptbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.acceptbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.acceptbutton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptbutton.Location = new System.Drawing.Point(114, 403);
            this.acceptbutton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.acceptbutton.Name = "acceptbutton";
            this.acceptbutton.Size = new System.Drawing.Size(88, 29);
            this.acceptbutton.TabIndex = 2;
            this.acceptbutton.Text = "Select";
            this.acceptbutton.UseVisualStyleBackColor = false;
            this.acceptbutton.Click += new System.EventHandler(this.acceptbutton_Click);
            // 
            // cancelbutton
            // 
            this.cancelbutton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cancelbutton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbutton.Location = new System.Drawing.Point(237, 403);
            this.cancelbutton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.Size = new System.Drawing.Size(88, 29);
            this.cancelbutton.TabIndex = 3;
            this.cancelbutton.Text = "Cancel";
            this.cancelbutton.UseVisualStyleBackColor = false;
            // 
            // SongCount
            // 
            this.SongCount.Location = new System.Drawing.Point(100, 383);
            this.SongCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SongCount.Name = "SongCount";
            this.SongCount.Size = new System.Drawing.Size(239, 16);
            this.SongCount.TabIndex = 4;
            this.SongCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchbar
            // 
            this.searchbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(47)))), ((int)(((byte)(59)))));
            this.searchbar.ForeColor = System.Drawing.Color.Gray;
            this.searchbar.Location = new System.Drawing.Point(14, 355);
            this.searchbar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.searchbar.Name = "searchbar";
            this.searchbar.Size = new System.Drawing.Size(411, 21);
            this.searchbar.TabIndex = 5;
            this.searchbar.Text = "Search...";
            this.searchbar.TextChanged += new System.EventHandler(this.searchbar_TextChanged);
            this.searchbar.Enter += new System.EventHandler(this.searchbar_Enter);
            // 
            // SongSelection
            // 
            this.AcceptButton = this.acceptbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(66)))), ((int)(((byte)(77)))));
            this.CancelButton = this.cancelbutton;
            this.ClientSize = new System.Drawing.Size(438, 443);
            this.Controls.Add(this.searchbar);
            this.Controls.Add(this.SongCount);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.acceptbutton);
            this.Controls.Add(this.SelectAllCheckBox);
            this.Controls.Add(this.songCheckList);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SongSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Songs...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SongSelection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox songCheckList;
        private System.Windows.Forms.CheckBox SelectAllCheckBox;
        private System.Windows.Forms.Button acceptbutton;
        private System.Windows.Forms.Button cancelbutton;
        private System.Windows.Forms.Label SongCount;
        private System.Windows.Forms.TextBox searchbar;
    }
}