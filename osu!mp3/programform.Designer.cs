namespace osu_mp3
{
    partial class mainform
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainform));
            this.extractButton = new System.Windows.Forms.Button();
            this.maingroup = new System.Windows.Forms.GroupBox();
            this.startbutton = new osu_mp3.mainform.MenuButton();
            this.startmenustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractandtagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tagOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.songnumber = new System.Windows.Forms.Label();
            this.clear = new System.Windows.Forms.Label();
            this.songovrBrowseDir = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.songovrLabel = new System.Windows.Forms.Label();
            this.songovrTextBox = new System.Windows.Forms.TextBox();
            this.createdby = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.openFolder = new System.Windows.Forms.Label();
            this.processI = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dstLabel = new System.Windows.Forms.Label();
            this.srcLabel = new System.Windows.Forms.Label();
            this.browseDstDir = new System.Windows.Forms.Button();
            this.dstTextBox = new System.Windows.Forms.TextBox();
            this.srcTextBox = new System.Windows.Forms.TextBox();
            this.browseSrcDir = new System.Windows.Forms.Button();
            this.extraction = new System.ComponentModel.BackgroundWorker();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.tagger = new System.ComponentModel.BackgroundWorker();
            this.extractonly = new System.ComponentModel.BackgroundWorker();
            this.tagonly = new System.ComponentModel.BackgroundWorker();
            this.maingroup.SuspendLayout();
            this.startmenustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // extractButton
            // 
            this.extractButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.extractButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.extractButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.extractButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.extractButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.extractButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extractButton.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.extractButton.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.extractButton.Location = new System.Drawing.Point(109, 204);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(81, 27);
            this.extractButton.TabIndex = 0;
            this.extractButton.TabStop = false;
            this.extractButton.Text = "Extract/Tag";
            this.extractButton.UseVisualStyleBackColor = false;
            this.extractButton.Click += new System.EventHandler(this.extractbutton);
            this.extractButton.MouseEnter += new System.EventHandler(this.extractButton_MouseEnter);
            this.extractButton.MouseLeave += new System.EventHandler(this.extractButton_MouseLeave);
            // 
            // maingroup
            // 
            this.maingroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maingroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(60)))), ((int)(((byte)(66)))), ((int)(((byte)(77)))));
            this.maingroup.Controls.Add(this.extractButton);
            this.maingroup.Controls.Add(this.startbutton);
            this.maingroup.Controls.Add(this.songnumber);
            this.maingroup.Controls.Add(this.clear);
            this.maingroup.Controls.Add(this.songovrBrowseDir);
            this.maingroup.Controls.Add(this.checkBox1);
            this.maingroup.Controls.Add(this.songovrLabel);
            this.maingroup.Controls.Add(this.songovrTextBox);
            this.maingroup.Controls.Add(this.createdby);
            this.maingroup.Controls.Add(this.status);
            this.maingroup.Controls.Add(this.openFolder);
            this.maingroup.Controls.Add(this.processI);
            this.maingroup.Controls.Add(this.progressBar1);
            this.maingroup.Controls.Add(this.dstLabel);
            this.maingroup.Controls.Add(this.srcLabel);
            this.maingroup.Controls.Add(this.browseDstDir);
            this.maingroup.Controls.Add(this.dstTextBox);
            this.maingroup.Controls.Add(this.srcTextBox);
            this.maingroup.Controls.Add(this.browseSrcDir);
            this.maingroup.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maingroup.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.maingroup.Location = new System.Drawing.Point(12, 12);
            this.maingroup.Name = "maingroup";
            this.maingroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maingroup.Size = new System.Drawing.Size(318, 247);
            this.maingroup.TabIndex = 1;
            this.maingroup.TabStop = false;
            this.maingroup.Text = "osu!MP3";
            this.maingroup.UseCompatibleTextRendering = true;
            // 
            // startbutton
            // 
            this.startbutton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.startbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startbutton.Location = new System.Drawing.Point(189, 204);
            this.startbutton.Menu = this.startmenustrip;
            this.startbutton.Name = "startbutton";
            this.startbutton.ShowMenuUnderCursor = true;
            this.startbutton.Size = new System.Drawing.Size(21, 27);
            this.startbutton.TabIndex = 16;
            this.startbutton.TabStop = false;
            this.startbutton.UseVisualStyleBackColor = true;
            // 
            // startmenustrip
            // 
            this.startmenustrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(56)))), ((int)(((byte)(67)))));
            this.startmenustrip.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startmenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractandtagToolStripMenuItem,
            this.extractOnlyToolStripMenuItem,
            this.tagOnlyToolStripMenuItem});
            this.startmenustrip.Name = "startmenustrip";
            this.startmenustrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.startmenustrip.Size = new System.Drawing.Size(165, 70);
            // 
            // extractandtagToolStripMenuItem
            // 
            this.extractandtagToolStripMenuItem.Checked = true;
            this.extractandtagToolStripMenuItem.CheckOnClick = true;
            this.extractandtagToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.extractandtagToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.extractandtagToolStripMenuItem.Name = "extractandtagToolStripMenuItem";
            this.extractandtagToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.extractandtagToolStripMenuItem.Text = "Extract and Tag";
            this.extractandtagToolStripMenuItem.Click += new System.EventHandler(this.extractandtagToolStripMenuItem_Click);
            // 
            // extractOnlyToolStripMenuItem
            // 
            this.extractOnlyToolStripMenuItem.CheckOnClick = true;
            this.extractOnlyToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.extractOnlyToolStripMenuItem.Name = "extractOnlyToolStripMenuItem";
            this.extractOnlyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.extractOnlyToolStripMenuItem.Text = "Extract Only";
            this.extractOnlyToolStripMenuItem.Click += new System.EventHandler(this.extractOnlyToolStripMenuItem_Click);
            // 
            // tagOnlyToolStripMenuItem
            // 
            this.tagOnlyToolStripMenuItem.CheckOnClick = true;
            this.tagOnlyToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.tagOnlyToolStripMenuItem.Name = "tagOnlyToolStripMenuItem";
            this.tagOnlyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.tagOnlyToolStripMenuItem.Text = "Tag Only";
            this.tagOnlyToolStripMenuItem.Click += new System.EventHandler(this.tagOnlyToolStripMenuItem_Click);
            // 
            // songnumber
            // 
            this.songnumber.AutoSize = true;
            this.songnumber.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songnumber.Location = new System.Drawing.Point(28, 97);
            this.songnumber.Name = "songnumber";
            this.songnumber.Size = new System.Drawing.Size(0, 13);
            this.songnumber.TabIndex = 15;
            // 
            // clear
            // 
            this.clear.AutoSize = true;
            this.clear.BackColor = System.Drawing.Color.Transparent;
            this.clear.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.clear.Location = new System.Drawing.Point(216, 210);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(36, 15);
            this.clear.TabIndex = 14;
            this.clear.Text = "Clear";
            this.clear.Click += new System.EventHandler(this.clear_Click);
            this.clear.MouseEnter += new System.EventHandler(this.clear_MouseEnter);
            this.clear.MouseLeave += new System.EventHandler(this.clear_MouseLeave);
            // 
            // songovrBrowseDir
            // 
            this.songovrBrowseDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.songovrBrowseDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.songovrBrowseDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.songovrBrowseDir.Cursor = System.Windows.Forms.Cursors.Default;
            this.songovrBrowseDir.Enabled = false;
            this.songovrBrowseDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.songovrBrowseDir.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songovrBrowseDir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.songovrBrowseDir.Location = new System.Drawing.Point(236, 76);
            this.songovrBrowseDir.Name = "songovrBrowseDir";
            this.songovrBrowseDir.Size = new System.Drawing.Size(73, 23);
            this.songovrBrowseDir.TabIndex = 13;
            this.songovrBrowseDir.TabStop = false;
            this.songovrBrowseDir.Text = "Browse...";
            this.songovrBrowseDir.UseVisualStyleBackColor = false;
            this.songovrBrowseDir.Click += new System.EventHandler(this.songovrBrowseDir_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Century Gothic", 6.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(94, 59);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 17);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "override";
            this.tooltip.SetToolTip(this.checkBox1, "If you don\'t have your \\Songs folder inside your osu! root folder, \ntick this box" +
        " to browse your \\Songs folder");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            this.checkBox1.MouseHover += new System.EventHandler(this.checkBox1_MouseHover);
            // 
            // songovrLabel
            // 
            this.songovrLabel.AutoSize = true;
            this.songovrLabel.BackColor = System.Drawing.Color.Transparent;
            this.songovrLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songovrLabel.ForeColor = System.Drawing.Color.White;
            this.songovrLabel.Location = new System.Drawing.Point(6, 59);
            this.songovrLabel.Name = "songovrLabel";
            this.songovrLabel.Size = new System.Drawing.Size(82, 15);
            this.songovrLabel.TabIndex = 11;
            this.songovrLabel.Text = "\\Songs Folder";
            // 
            // songovrTextBox
            // 
            this.songovrTextBox.Enabled = false;
            this.songovrTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.songovrTextBox.Location = new System.Drawing.Point(7, 77);
            this.songovrTextBox.Name = "songovrTextBox";
            this.songovrTextBox.Size = new System.Drawing.Size(224, 20);
            this.songovrTextBox.TabIndex = 10;
            this.songovrTextBox.TextChanged += new System.EventHandler(this.songovrTextBox_TextChanged);
            // 
            // createdby
            // 
            this.createdby.AutoSize = true;
            this.createdby.BackColor = System.Drawing.Color.Transparent;
            this.createdby.Font = new System.Drawing.Font("Calibri", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createdby.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.createdby.Location = new System.Drawing.Point(159, 234);
            this.createdby.Name = "createdby";
            this.createdby.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.createdby.Size = new System.Drawing.Size(156, 10);
            this.createdby.TabIndex = 2;
            this.createdby.Text = "osu!MP3 1.4.34.1 (2021) created by Verchiel_";
            this.createdby.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.Transparent;
            this.status.Font = new System.Drawing.Font("Century Gothic", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.status.Location = new System.Drawing.Point(8, 172);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(302, 13);
            this.status.TabIndex = 9;
            this.status.Text = "Idle";
            this.status.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFolder
            // 
            this.openFolder.AutoSize = true;
            this.openFolder.BackColor = System.Drawing.Color.Transparent;
            this.openFolder.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openFolder.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.openFolder.Location = new System.Drawing.Point(57, 210);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(46, 15);
            this.openFolder.TabIndex = 8;
            this.openFolder.Text = "Open...";
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            this.openFolder.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.openFolder.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            // 
            // processI
            // 
            this.processI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processI.BackColor = System.Drawing.Color.Transparent;
            this.processI.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processI.ForeColor = System.Drawing.Color.White;
            this.processI.Location = new System.Drawing.Point(7, 155);
            this.processI.Name = "processI";
            this.processI.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.processI.Size = new System.Drawing.Size(304, 17);
            this.processI.TabIndex = 7;
            this.processI.Text = "Click Extract/Tag to start";
            this.processI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(54)))));
            this.progressBar1.Enabled = false;
            this.progressBar1.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.progressBar1.Location = new System.Drawing.Point(8, 188);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(302, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 6;
            // 
            // dstLabel
            // 
            this.dstLabel.AutoSize = true;
            this.dstLabel.BackColor = System.Drawing.Color.Transparent;
            this.dstLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dstLabel.ForeColor = System.Drawing.Color.White;
            this.dstLabel.Location = new System.Drawing.Point(6, 113);
            this.dstLabel.Name = "dstLabel";
            this.dstLabel.Size = new System.Drawing.Size(96, 15);
            this.dstLabel.TabIndex = 5;
            this.dstLabel.Text = "Extraction Folder";
            // 
            // srcLabel
            // 
            this.srcLabel.AutoSize = true;
            this.srcLabel.BackColor = System.Drawing.Color.Transparent;
            this.srcLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srcLabel.ForeColor = System.Drawing.Color.White;
            this.srcLabel.Location = new System.Drawing.Point(6, 19);
            this.srcLabel.Name = "srcLabel";
            this.srcLabel.Size = new System.Drawing.Size(113, 15);
            this.srcLabel.TabIndex = 4;
            this.srcLabel.Text = "osu! Executable File";
            // 
            // browseDstDir
            // 
            this.browseDstDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseDstDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.browseDstDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.browseDstDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseDstDir.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseDstDir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.browseDstDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.browseDstDir.Location = new System.Drawing.Point(236, 130);
            this.browseDstDir.Name = "browseDstDir";
            this.browseDstDir.Size = new System.Drawing.Size(73, 23);
            this.browseDstDir.TabIndex = 3;
            this.browseDstDir.TabStop = false;
            this.browseDstDir.Text = "Browse...";
            this.browseDstDir.UseVisualStyleBackColor = false;
            this.browseDstDir.Click += new System.EventHandler(this.browseDstDir_Click);
            // 
            // dstTextBox
            // 
            this.dstTextBox.Enabled = false;
            this.dstTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dstTextBox.Location = new System.Drawing.Point(7, 131);
            this.dstTextBox.Name = "dstTextBox";
            this.dstTextBox.Size = new System.Drawing.Size(224, 20);
            this.dstTextBox.TabIndex = 2;
            // 
            // srcTextBox
            // 
            this.srcTextBox.Enabled = false;
            this.srcTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srcTextBox.Location = new System.Drawing.Point(7, 37);
            this.srcTextBox.Name = "srcTextBox";
            this.srcTextBox.Size = new System.Drawing.Size(224, 20);
            this.srcTextBox.TabIndex = 1;
            // 
            // browseSrcDir
            // 
            this.browseSrcDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.browseSrcDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(57)))), ((int)(((byte)(69)))));
            this.browseSrcDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.browseSrcDir.Cursor = System.Windows.Forms.Cursors.Default;
            this.browseSrcDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.browseSrcDir.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseSrcDir.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.browseSrcDir.Location = new System.Drawing.Point(236, 36);
            this.browseSrcDir.Name = "browseSrcDir";
            this.browseSrcDir.Size = new System.Drawing.Size(73, 23);
            this.browseSrcDir.TabIndex = 0;
            this.browseSrcDir.TabStop = false;
            this.browseSrcDir.Text = "Browse...";
            this.browseSrcDir.UseVisualStyleBackColor = false;
            this.browseSrcDir.Click += new System.EventHandler(this.browseSrcDir_Click);
            // 
            // extraction
            // 
            this.extraction.WorkerReportsProgress = true;
            this.extraction.WorkerSupportsCancellation = true;
            // 
            // tooltip
            // 
            this.tooltip.Active = false;
            this.tooltip.AutomaticDelay = 100;
            this.tooltip.AutoPopDelay = 5000;
            this.tooltip.InitialDelay = 100;
            this.tooltip.ReshowDelay = 20;
            // 
            // tagger
            // 
            this.tagger.WorkerReportsProgress = true;
            this.tagger.WorkerSupportsCancellation = true;
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::osu_mp3.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(342, 271);
            this.Controls.Add(this.maingroup);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "osu!MP3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainform_FormClosing);
            this.Load += new System.EventHandler(this.mainform_Load);
            this.maingroup.ResumeLayout(false);
            this.maingroup.PerformLayout();
            this.startmenustrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.GroupBox maingroup;
        private System.Windows.Forms.Button browseDstDir;
        private System.Windows.Forms.TextBox dstTextBox;
        private System.Windows.Forms.TextBox srcTextBox;
        private System.Windows.Forms.Button browseSrcDir;
        private System.Windows.Forms.Label dstLabel;
        private System.Windows.Forms.Label srcLabel;
        private System.Windows.Forms.Label processI;
        private System.ComponentModel.BackgroundWorker extraction;
        private System.Windows.Forms.Label createdby;
        private System.Windows.Forms.Label openFolder;
        private System.Windows.Forms.Label status;
        public System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label songovrLabel;
        private System.Windows.Forms.TextBox songovrTextBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button songovrBrowseDir;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Label clear;
        private System.Windows.Forms.Label songnumber;
        private System.ComponentModel.BackgroundWorker tagger;
        private MenuButton startbutton;
        private System.Windows.Forms.ContextMenuStrip startmenustrip;
        private System.Windows.Forms.ToolStripMenuItem extractandtagToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagOnlyToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker extractonly;
        private System.ComponentModel.BackgroundWorker tagonly;
    }
}

