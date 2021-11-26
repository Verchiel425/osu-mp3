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

namespace osu_mp3
{
    public partial class mainform : Form
    {
        [DllImport("osu!mp3Lib.dll")]
        public static extern int countTotal(string path);
        [DllImport("osu!mp3Lib.dll")]
        public static extern int getTotal();
        [DllImport("osu!mp3Lib.dll")]
        public static extern int extract_songs(string csrc, string cdst, int index);
        public mainform()
        {
            InitializeComponent();

            extraction.DoWork += Extraction_DoWork;
            extraction.ProgressChanged += Extraction_ProgressChanged;
            extraction.WorkerReportsProgress = true;
            extraction.RunWorkerCompleted += Extraction_RunWorkerCompleted;
        }

        private void Extraction_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string timeElapsed = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);

            this.status.Text = "Completed! (Time: " + timeElapsed + " s)";
            this.extractButton.Text = "Extract MP3";
            this.extractButton.Enabled = true;
            string m = "Extracted: " + c + " Duplicates: " + d;
            this.processI.Text = m;
            progressBar1.Value = 100;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        Stopwatch stopwatch = new Stopwatch();
        static int d;
        static int c;

        private void Extraction_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int completed = e.ProgressPercentage;
            int total = getTotal();
            int progress = (int)(((float)completed / (float)total) * 100);
            progressBar1.Value = progress;
            this.processI.Text = String.Format("({0}/{1}) {2}% completed...", completed, total, progress);
        }

        private void Extraction_DoWork(object sender, DoWorkEventArgs e)
        {
            d = 0;
            c = 0;

            int total = getTotal();

            for (int i = 0; i < total; i++)
            {
                switch (extract_songs(this.songovrTextBox.Text, osu.getDstDir(), i))
                {
                    case 0:
                        c++;
                        break;
                    case 1:
                        d++;
                        break;
                    case -1:
                        break;
                    default:
                        break;
                }

                extraction.ReportProgress(i);
            }
        }

        private void extractbutton(object sender, EventArgs e)
        {
            if (this.songovrTextBox.Text != "" && this.dstTextBox.Text != "")
            {
                this.status.Text = "Extracting...";
                stopwatch.Reset();
                stopwatch.Start();
                this.processI.Text = "Initializing...";

                string[] src = { "src " + this.srcTextBox.Text };
                string[] dst = { "dst " + this.dstTextBox.Text };
                string[] songovr = { "ovr " + this.songovrTextBox.Text };
                string[] ovrchecked;
                if (checkBox1.Checked)
                {
                    ovrchecked = new string[]{ "sbc " + (checkBox1.Checked).ToString() };
                }
                else
                {
                    ovrchecked = new string[] { "sbc " + (checkBox1.Checked).ToString() };

                }
                config.save(src, dst, songovr, ovrchecked);

                osu.setDstDir(this.dstTextBox.Text);
                this.processI.Text = "Checking directory...";
                this.extractButton.Enabled = false;
                countTotal(this.songovrTextBox.Text);
                this.progressBar1.Enabled = true;
                this.progressBar1.Value = 1;

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
            this.srcTextBox.Text = config.read(0);
            this.dstTextBox.Text = config.read(1);
            this.songovrTextBox.Text = config.read(2);
            if (config.read(3) == "True")
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
                this.srcTextBox.Text = config.read(0);
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
            this.songovrTT.Active = true;
        }

        private void mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            string[] src = { "src " + this.srcTextBox.Text };
            string[] dst = { "dst " + this.dstTextBox.Text };
            string[] songovr = { "ovr " + this.songovrTextBox.Text };
            string[] ovrchecked;
            if (checkBox1.Checked)
            {
                ovrchecked = new string[] { "sbc " + (checkBox1.Checked).ToString() };
            }
            else
            {
                ovrchecked = new string[] { "sbc " + (checkBox1.Checked).ToString() };

            }
            config.save(src, dst, songovr, ovrchecked);

        }
    }
}
