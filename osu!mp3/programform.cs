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

        private void Tagger_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.processI.Text = "All processes completed!";
            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            this.extractButton.Enabled = true;
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
                    p++;
                    tagger.ReportProgress(p);
                }
            }
        }

        Stopwatch stopwatch = new Stopwatch();
        static int d;
        static int c;
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

        private void extractbutton(object sender, EventArgs e)
        {
            osu.isSuccess = true;
            if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
            {
                this.status.Text = "Extracting...";
                stopwatch.Reset();
                stopwatch.Start();
                this.processI.Text = "Initializing...";
                wait(500);

                config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked);

                osu.setDstDir(this.dstTextBox.Text);
                this.processI.Text = "Checking directory...";
                wait(500);
                this.extractButton.Enabled = false;
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
        }

        private void browseSrcDir_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog srcOFD = new OpenFileDialog();
            srcOFD.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            config.save(this.srcTextBox.Text, this.songovrTextBox.Text, this.dstTextBox.Text, checkBox1.Checked);
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
    }
}
