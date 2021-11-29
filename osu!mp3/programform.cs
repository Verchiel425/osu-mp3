using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Threading;
using ezf;
using mp3ID3parser;

namespace osu_mp3
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();

            extraction.DoWork += Extraction_DoWork;
            extraction.ProgressChanged += Extraction_ProgressChanged;
            extraction.WorkerReportsProgress = true;
            extraction.RunWorkerCompleted += Extraction_RunWorkerCompleted;
            tagger.DoWork += Tagger_DoWork;
            tagger.ProgressChanged += Tagger_ProgressChanged;
            tagger.WorkerReportsProgress = true;
            tagger.RunWorkerCompleted += Tagger_RunWorkerCompleted;
            extractonly.DoWork += Extractonly_DoWork;
            extractonly.ProgressChanged += Extractonly_ProgressChanged;
            extractonly.WorkerReportsProgress = true;
            extractonly.RunWorkerCompleted += Extractonly_RunWorkerCompleted;
            tagonly.DoWork += Tagonly_DoWork;
            tagonly.ProgressChanged += Tagonly_ProgressChanged;
            tagonly.WorkerReportsProgress = true;
            tagonly.RunWorkerCompleted += Tagonly_RunWorkerCompleted;
        }
        public static int startbuttonstate = 0;
        public class MenuButton : Button
        {
            [DefaultValue(null)]
            public ContextMenuStrip Menu { get; set; }

            [DefaultValue(false)]
            public bool ShowMenuUnderCursor { get; set; }

            protected override void OnMouseDown(MouseEventArgs mevent)
            {
                base.OnMouseDown(mevent);

                if (Menu != null && mevent.Button == MouseButtons.Left)
                {
                    Point menuLocation;

                    if (ShowMenuUnderCursor)
                    {
                        menuLocation = mevent.Location;
                    }
                    else
                    {
                        menuLocation = new Point(0, Height);
                    }

                    Menu.Show(this, menuLocation);
                }
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                base.OnPaint(pevent);

                if (Menu != null)
                {
                    int arrowX = ClientRectangle.Width - 14;
                    int arrowY = ClientRectangle.Height / 2 - 1;

                    Brush brush = Enabled ? SystemBrushes.ControlLight : SystemBrushes.ControlLight;
                    Point[] arrows = new Point[] { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
                    pevent.Graphics.FillPolygon(brush, arrows);
                }
            }
        }
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        string currently_tagging;
        Stopwatch stopwatch = new Stopwatch();
        static int d;
        static int c;

        #region TagOnly
        private void Tagonly_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.processI.Text = "All processes completed!";
            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            this.extractButton.Enabled = true;
            this.startbutton.Enabled = true;
        }

        private void Tagonly_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int completed = e.ProgressPercentage;
            int total = FOLDER.totalfiles(this.dstTextBox.Text);
            int progress = (int)(((float)completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", completed, total, progress);
            string currentlytagging = " " + currently_tagging;
            this.status.Text = "Tagging" + currentlytagging + "...";
        }

        private void Tagonly_DoWork(object sender, DoWorkEventArgs e)
        {
            FOLDER destfolder = new FOLDER(this.dstTextBox.Text);
            int total = destfolder.totalfiles();
            int p = 0;
            for (int i = 0; i < total; i++)
            {
                if ((destfolder.getfiles()[i]).Contains(".mp3"))
                {
                    wait(25);
                    currently_tagging = NAME.isolate(destfolder.getfiles()[i]);
                    ID3.setTags(destfolder.getfiles()[i]);
                    ID3.setCover(destfolder.getfiles()[i], osu.getPic(this.songovrTextBox.Text, modify.isolate_title(destfolder.getfiles()[i])));
                    p++;
                    tagonly.ReportProgress(p);
                }
            }
        }
        #endregion TagOnly
        #region ExtractOnly
        private void Extractonly_DoWork(object sender, DoWorkEventArgs e)
        {
            d = 0;
            c = 0;

            int total = FOLDER.totalfolders(this.songovrTextBox.Text);

            for (int i = 0; i < total; i++)
            {
                wait(25);
                switch (osu.extract(this.songovrTextBox.Text, this.dstTextBox.Text, i))
                {
                    case 0:
                        c++;
                        break;
                    case 1:
                        d++;
                        break;
                    default:
                        osu.isSuccess = false;
                        break;
                }

                extractonly.ReportProgress(i);
            }
        }

        private void Extractonly_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int completed = e.ProgressPercentage;
            int total = FOLDER.totalfolders(this.songovrTextBox.Text);
            int progress = (int)(((float)completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", completed, total, progress);
            string currentlyextracting = " " + osu.currentlyextracting();
            this.status.Text = "Extracting" + currentlyextracting + "...";
        }

        private void Extractonly_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.processI.Text = "All processes completed!";
            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            this.extractButton.Enabled = true;
            this.startbutton.Enabled = true;
        }
        #endregion
        #region ExtractAndTag
        private void Tagger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.processI.Text = "All processes completed!";
            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            this.extractButton.Enabled = true;
            this.startbutton.Enabled = true;
        }

        private void Tagger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int completed = e.ProgressPercentage;
            int total = FOLDER.totalfiles(this.dstTextBox.Text);
            int progress = (int)(((float)completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", completed, total, progress);
            string currentlytagging = " " + currently_tagging;
            this.status.Text = "Tagging" + currentlytagging + "...";
        }

        private void Tagger_DoWork(object sender, DoWorkEventArgs e)
        {
            FOLDER destfolder = new FOLDER(this.dstTextBox.Text);
            int total = destfolder.totalfiles();
            int p = 0;
            for (int i = 0; i < total; i++){
                if ((destfolder.getfiles()[i]).Contains(".mp3"))
                {
                    wait(25);
                    currently_tagging = NAME.isolate(destfolder.getfiles()[i]);
                    ID3.setTags(destfolder.getfiles()[i]);
                    ID3.setCover(destfolder.getfiles()[i], osu.getPic(this.songovrTextBox.Text, modify.isolate_title(destfolder.getfiles()[i])));
                    p++;
                    tagger.ReportProgress(p);
                }
            }
        }

        private void Extraction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (osu.isSuccess)
            {
                string m = "Extracted " + c + " songs (" + d +" copies)";
                this.processI.Text = m;
                this.status.Text = "Extraction complete!";
                progressBar1.Value = 100;
                wait(500);
                this.status.Text = "Proceeding to set tags...";
                wait(1000);
                this.status.Text = String.Format("{0} files to be tagged...", FOLDER.totalfiles(this.dstTextBox.Text));
                wait(500);
            }
            else
            {
                string m = "No songs found...";
                this.processI.Text = m;
                progressBar1.Value = 100;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            progressBar1.Value = 0;
            tagger.RunWorkerAsync();
        }

        private void Extraction_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int completed = e.ProgressPercentage;
            int total = FOLDER.totalfolders(this.songovrTextBox.Text);
            int progress = (int)(((float)completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", completed, total, progress);
            string currentlyextracting = " " + osu.currentlyextracting();
            this.status.Text = "Extracting" + currentlyextracting + "...";
        }

        private void Extraction_DoWork(object sender, DoWorkEventArgs e)
        {
            d = 0;
            c = 0;

            int total = FOLDER.totalfolders(this.songovrTextBox.Text);

            for (int i = 0; i < total; i++)
            {
                wait(25);
                switch (osu.extract(this.songovrTextBox.Text, this.dstTextBox.Text, i))
                {
                    case 0:
                        c++;
                        break;
                    case 1:
                        d++;
                        break;
                    default:
                        osu.isSuccess = false;
                        break;
                }

                extraction.ReportProgress(i);
            }
        }
        #endregion ExtractAndTag

        private void extractbutton(object sender, EventArgs e)
        {
            osu.isSuccess = true;
            switch (startbuttonstate)
            {
                case 0://extractandtag
                    if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
                    {
                        this.status.Text = "Extracting...";
                        stopwatch.Reset();
                        stopwatch.Start();
                        this.processI.Text = "Initializing...";
                        wait(500);

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, startbuttonstate);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        this.extractButton.Enabled = false;
                        this.startbutton.Enabled = false;
                        this.progressBar1.Enabled = true;

                        if (!extraction.IsBusy)
                        {
                            this.processI.Text = "Proceeding to extraction...";
                            extraction.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("EXTRACTION IN PROGRESS!");
                        }
                    }
                    else
                    {
                        this.processI.Text = "Please assign a directory!";
                    }
                    break;
                case 1://extractonly
                    if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
                    {
                        this.status.Text = "Extracting...";
                        stopwatch.Reset();
                        stopwatch.Start();
                        this.processI.Text = "Initializing...";
                        wait(500);

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, startbuttonstate);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        this.extractButton.Enabled = false;
                        this.startbutton.Enabled = false;
                        this.progressBar1.Enabled = true;

                        if (!extraction.IsBusy)
                        {
                            this.processI.Text = "Proceeding to extraction...";
                            extractonly.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("EXTRACTION IN PROGRESS!");
                        }
                    }
                    else
                    {
                        this.processI.Text = "Please assign a directory!";
                    }
                    break;
                case 2://tagonly
                    if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
                    {
                        this.status.Text = "Tagging...";
                        stopwatch.Reset();
                        stopwatch.Start();
                        this.processI.Text = "Initializing...";
                        wait(500);

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, startbuttonstate);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        this.extractButton.Enabled = false;
                        this.startbutton.Enabled = false;
                        this.progressBar1.Enabled = true;

                        if (!extraction.IsBusy)
                        {
                            this.processI.Text = "Proceeding to tag...";
                            tagonly.RunWorkerAsync();
                        }
                        else
                        {
                            MessageBox.Show("TAGGING IN PROGRESS!");
                        }
                    }
                    else
                    {
                        this.processI.Text = "Please assign a directory!";
                    }
                    break;
                default:
                    break;
            }
            
        }

        private void browseSrcDir_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog srcOFD = new OpenFileDialog();
            srcOFD.Filter = "osu! Executable File |osu!.exe";
            srcOFD.Title = "Open osu! Executable File";
            if (srcOFD.ShowDialog() == DialogResult.OK)
            {
                this.srcTextBox.Text = srcOFD.FileName;
                this.songovrTextBox.Text = osu.osuToDir(srcOFD.FileName) + "\\Songs";
            }
        }

        private void browseDstDir_Click(object sender, EventArgs e)
        {
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog ddb = new VistaFolderBrowserDialog();
            if(ddb.ShowDialog() == true)
            {
                this.dstTextBox.Text = ddb.SelectedPath;
            }
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            this.srcTextBox.Text = config.osudir();
            this.dstTextBox.Text = config.destdir();
            this.songovrTextBox.Text = config.songdir();
            if(config.overridestate() == "True")
            {
                this.checkBox1.Checked = true;
            }
            else
            {
                this.checkBox1.Checked = false;
            }
            switch (config.operation())
            {
                case "0":
                    extractOnlyToolStripMenuItem.Checked = false;
                    tagOnlyToolStripMenuItem.Checked = false;
                    extractandtagToolStripMenuItem.Checked = true;
                    startbuttonstate = 0;
                    this.extractButton.Text = "Extract/Tag";
                    this.processI.Text = "Click " + this.extractButton.Text + " to start";
                    break;
                case "1":
                    extractOnlyToolStripMenuItem.Checked = true;
                    tagOnlyToolStripMenuItem.Checked = false;
                    extractandtagToolStripMenuItem.Checked = false;
                    startbuttonstate = 1;
                    this.extractButton.Text = "Extract Only";
                    this.processI.Text = "Click " + this.extractButton.Text + " to start";
                    break;
                case "2":
                    extractOnlyToolStripMenuItem.Checked = false;
                    tagOnlyToolStripMenuItem.Checked = true;
                    extractandtagToolStripMenuItem.Checked = false;
                    startbuttonstate = 2;
                    this.extractButton.Text = "Tag Only";
                    this.processI.Text = "Click " + this.extractButton.Text + " to start";
                    break;
                default:
                    break;
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.openFolder.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.openFolder.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void extractButton_MouseEnter(object sender, EventArgs e)
        {
            this.extractButton.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
        }

        private void extractButton_MouseLeave(object sender, EventArgs e)
        {
            this.extractButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            if (dstTextBox.Text != "")
            {
                if (Directory.Exists(dstTextBox.Text))
                {
                    Process.Start("explorer.exe", @dstTextBox.Text);
                }
                else { Process.Start("explorer.exe", @Environment.GetFolderPath(Environment.SpecialFolder.Desktop)); }
            }
            else
            {
                Process.Start("explorer.exe", @Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.browseSrcDir.Enabled = false;
                
                this.songovrBrowseDir.Enabled = true;
                this.srcTextBox.Text = "";
            }
            else
            {
                this.browseSrcDir.Enabled = true;
                
                this.songovrBrowseDir.Enabled = false;
                this.srcTextBox.Text = config.osudir();
            }
        }

        private void songovrBrowseDir_Click(object sender, EventArgs e)
        {
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog sodb = new VistaFolderBrowserDialog();
            if (sodb.ShowDialog() == true)
            {
                this.songovrTextBox.Text = sodb.SelectedPath;
            }
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
            this.tooltip.Active = true;
        }

        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (extraction.IsBusy)
            {
                extraction.CancelAsync();
            }
            if (tagger.IsBusy)
            {
                tagger.CancelAsync();
            }
            this.progressBar1.Value = 100;
            config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, startbuttonstate);
        }

        private void clear_MouseEnter(object sender, EventArgs e)
        {
            this.clear.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void clear_MouseLeave(object sender, EventArgs e)
        {
            this.clear.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void clear_Click(object sender, EventArgs e)
        {
            this.srcTextBox.Text = "";
            this.dstTextBox.Text = "";
            this.songovrTextBox.Text = "";
        }

        private void songovrTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(songovrTextBox.Text))
            {
                this.songnumber.Text = "";
            }
            this.songnumber.Text = (FOLDER.totalfolders(this.songovrTextBox.Text)).ToString() + " folders found...";
        }

        private void extractandtagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = false;
            tagOnlyToolStripMenuItem.Checked = false;
            extractandtagToolStripMenuItem.Checked = true;
            startbuttonstate = 0;
            this.extractButton.Text = "Extract/Tag";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }

        private void extractOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = true;
            tagOnlyToolStripMenuItem.Checked = false;
            extractandtagToolStripMenuItem.Checked = false;
            startbuttonstate = 1;
            this.extractButton.Text = "Extract Only";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }

        private void tagOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = false;
            tagOnlyToolStripMenuItem.Checked = true;
            extractandtagToolStripMenuItem.Checked = false;
            startbuttonstate = 2;
            this.extractButton.Text = "Tag Only";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }
    }
}