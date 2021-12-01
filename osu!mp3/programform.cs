using ezf;
using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using tagger;

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
            DidSelectSongs = false;
        }
        
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



        public static bool DidSelectSongs { get; set; }
        string currently_tagging;
        Stopwatch stopwatch = new Stopwatch();
        static int d;
        static int c;
        static int tagger_completed;
        static int extraction_completed;
        public static int operation = 0;
        public static string SONGSFOLDER;
        public static bool controlsLocked = false;



        private void Tagger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.processI.Text = "All processes completed!";
            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            log.save(timeElapsed, tagger_completed, operation);
            unlockControls();
        }

        private void Tagger_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tagger_completed = e.ProgressPercentage;
            int total;
            if (DidSelectSongs)
            {
                total = SongSelection.acceptItems.Count(s => s != null) - d;
            }
            else
            {
                total = FOLDER.totalfiles(this.dstTextBox.Text);
            }
            int progress = (int)(((float)tagger_completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", tagger_completed, total, progress);
            string currentlytagging = " " + currently_tagging;
            this.status.Text = "Tagging" + currentlytagging + "...";
        }

        private void Tagger_DoWork(object sender, DoWorkEventArgs e)
        {
            FOLDER destfolder = new FOLDER(this.dstTextBox.Text);
            string songfolder = this.songovrTextBox.Text;
            int total = destfolder.totalfiles();
            int p = 0;
            
            if (DidSelectSongs)
            {
                int selected_total = SongSelection.acceptItems.Count(s => s != null) - d;
                for (int i = 0; i < total; i++)
                {
                    for (int L = 0; L < selected_total; L++)
                    {
                        string selected_song = SongSelection.acceptItems[L];
                        if (destfolder.getfiles()[i].Contains(osu.remove_id(selected_song)))
                        {
                            if ((destfolder.getfiles()[i]).Contains(".mp3"))
                            {
                                wait(25);
                                currently_tagging = isolate.fullname(destfolder.getfiles()[i]);
                                METADATA.TagSongs(destfolder, i);
                                METADATA.SetCover(destfolder, i, osu.getPic(songfolder, currently_tagging));
                                p++;
                                tagger.ReportProgress(p);
                            }
                        }
                    }
                }
                return;
            }
            for (int i = 0; i < total; i++)
            {
                if ((destfolder.getfiles()[i]).Contains(".mp3"))
                {
                    wait(25);
                    currently_tagging = isolate.fullname(destfolder.getfiles()[i]);
                    METADATA.TagSongs(destfolder, i);
                    METADATA.SetCover(destfolder, i, osu.getPic(songfolder, currently_tagging));
                    p++;
                    tagger.ReportProgress(p);
                }
            }
        }

        private void Extraction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (osu.isSuccess)
            {
                string m = String.Format("Extract {0} song(s) ({1} copy(s))", c, d);
                this.processI.Text = m;
                this.status.Text = "Extraction complete!";
                wait(500);
            }
            else
            {
                string m = "No songs found...";
                this.processI.Text = m;
                wait(500);
            }
            progressBar1.Value = 100;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (operation == 0)
            {
                progressBar1.Value = 0;
                this.status.Text = "Proceeding to set tags...";
                wait(1000);
                if (DidSelectSongs)
                {
                    this.status.Text = String.Format("{0} file(s) to be tagged...", SongSelection.acceptItems.Count(s => s != null) - d);
                }
                else
                {
                    this.status.Text = String.Format("{0} file(s) to be tagged...", c);
                }
                wait(500);
                tagger.RunWorkerAsync();
            }
            else
            {
                stopwatch.Stop();

                TimeSpan ts = stopwatch.Elapsed;
                string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

                this.processI.Text = "All processes completed!";
                this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
                log.save(timeElapsed, extraction_completed, operation);
                unlockControls();
            }
        }

        private void Extraction_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            extraction_completed = e.ProgressPercentage;
            int total;
            if (DidSelectSongs)
            {
                total = SongSelection.acceptItems.Count(s => s != null);
            }
            else
            {
                total = FOLDER.totalfolders(this.songovrTextBox.Text);
            }
            int progress = (int)(((float)extraction_completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", extraction_completed, total, progress);
            string currentlyextracting = " " + osu.currentlyextracting();
            this.status.Text = "Extracting" + currentlyextracting + "...";
        }

        private void Extraction_DoWork(object sender, DoWorkEventArgs e)
        {
            d = 0;
            c = 0;

            string songfolder = this.songovrTextBox.Text;
            string destfolder = this.dstTextBox.Text;
            FOLDER SONG_FOLDER = new FOLDER(songfolder);
            int total = SONG_FOLDER.totalfolders();
            if (DidSelectSongs)
            {
                if (SongSelection.acceptItems.Count(s => s != null) == total)
                {
                    for (int i = 0; i < total; i++)
                    {
                        wait(25);
                        switch (osu.extract(songfolder, destfolder, i))
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
                    return;
                }
                for (int i = 0; i < total; i++)
                {
                    for (int L = 0; L < SongSelection.acceptItems.Count(s => s != null); L++)
                    {
                        string selected_song = SongSelection.acceptItems[L];
                        if (SONG_FOLDER.getfolders()[i].Contains(selected_song))
                        {
                            switch (osu.extract(songfolder, destfolder, i))
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
                            extraction.ReportProgress(c);
                        }
                    }
                }
                return;
            }

            for (int i = 0; i < total; i++)
            {
                wait(25);
                switch (osu.extract(songfolder, destfolder, i))
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

        private void extractbutton(object sender, EventArgs e)
        {
            osu.isSuccess = true;
            extraction_completed = 0;
            tagger_completed = 0;
            switch (operation)
            {
                case 0://extractandtag
                    if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
                    {
                        this.status.Text = "Extracting...";
                        stopwatch.Reset();
                        stopwatch.Start();
                        this.processI.Text = "Initializing...";
                        wait(500);

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, operation);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        lockControls();
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

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, operation);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        lockControls();
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
                case 2://tagonly
                    if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
                    {
                        d = 0;
                        c = 0;
                        this.status.Text = "Tagging...";
                        stopwatch.Reset();
                        stopwatch.Start();
                        this.processI.Text = "Initializing...";
                        wait(500);

                        config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, operation);

                        osu.setDstDir(this.dstTextBox.Text);
                        this.processI.Text = "Checking directory...";
                        wait(500);
                        lockControls();
                        this.progressBar1.Enabled = true;

                        if (!extraction.IsBusy)
                        {
                            this.processI.Text = "Proceeding to tag...";
                            tagger.RunWorkerAsync();
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
            srcOFD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            if (ddb.ShowDialog() == true)
            {
                this.dstTextBox.Text = ddb.SelectedPath;
            }
        }

        private void mainform_Load(object sender, EventArgs e)
        {
            this.srcTextBox.Text = config.osudir();
            this.dstTextBox.Text = config.destdir();
            this.songovrTextBox.Text = config.songdir();
            if (config.overridestate() == "True")
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
                    operation = 0;
                    this.extractButton.Text = "Extract/Tag";
                    this.processI.Text = "Click " + this.extractButton.Text + " to start";
                    break;
                case "1":
                    extractOnlyToolStripMenuItem.Checked = true;
                    tagOnlyToolStripMenuItem.Checked = false;
                    extractandtagToolStripMenuItem.Checked = false;
                    operation = 1;
                    this.extractButton.Text = "Extract Only";
                    this.processI.Text = "Click " + this.extractButton.Text + " to start";
                    break;
                case "2":
                    extractOnlyToolStripMenuItem.Checked = false;
                    tagOnlyToolStripMenuItem.Checked = true;
                    extractandtagToolStripMenuItem.Checked = false;
                    operation = 2;
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
            this.tooltip.Active = true;
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
                if (!controlsLocked)
                {
                    this.songovrBrowseDir.Enabled = true;
                }
                this.srcTextBox.Text = "";
            }
            else
            {
                if (!controlsLocked)
                {
                    this.browseSrcDir.Enabled = true;
                }

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
            config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked, operation);
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
                this.selectsongsButton.Enabled = false;
                SONGSFOLDER = this.songovrTextBox.Text;
            }
            else
            {
                this.songnumber.Text = (FOLDER.totalfolders(this.songovrTextBox.Text)).ToString() + " folder(s) found...";
                this.selectsongsButton.Enabled = true;
                SONGSFOLDER = this.songovrTextBox.Text;
            }
        }

        private void extractandtagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = false;
            tagOnlyToolStripMenuItem.Checked = false;
            extractandtagToolStripMenuItem.Checked = true;
            operation = 0;
            this.extractButton.Text = "Extract/Tag";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }

        private void extractOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = true;
            tagOnlyToolStripMenuItem.Checked = false;
            extractandtagToolStripMenuItem.Checked = false;
            operation = 1;
            this.extractButton.Text = "Extract Only";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }

        private void tagOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractOnlyToolStripMenuItem.Checked = false;
            tagOnlyToolStripMenuItem.Checked = true;
            extractandtagToolStripMenuItem.Checked = false;
            operation = 2;
            this.extractButton.Text = "Tag Only";
            this.processI.Text = "Click " + this.extractButton.Text + " to start";
        }

        private void selectsongsButton_Click(object sender, EventArgs e)
        {
            SongSelection songselection = new SongSelection();
            if (songselection.ShowDialog() == DialogResult.OK)
            {
                this.status.Text = String.Format("{0} song(s) selected!", SongSelection.acceptItems.Count(s => s != null));
            }
        }
        public void lockControls()
        {
            this.extractButton.Enabled = false;
            this.browseDstDir.Enabled = false;
            this.browseSrcDir.Enabled = false;
            this.songovrBrowseDir.Enabled = false;
            this.selectsongsButton.Enabled = false;
            this.startbutton.Enabled = false;
            controlsLocked = true;
        }
        public void unlockControls()
        {
            this.extractButton.Enabled = true;
            this.browseDstDir.Enabled = true;
            if (checkBox1.Checked)
            {
                this.browseSrcDir.Enabled = false;
                this.songovrBrowseDir.Enabled = true;
            }
            this.selectsongsButton.Enabled = true;
            this.startbutton.Enabled = true;
            controlsLocked = false;
        }
    }
}