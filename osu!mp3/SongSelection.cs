using ezf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using tagger;

namespace osu_mp3
{
    public partial class SongSelection : Form
    {
        public SongSelection()
        {
            InitializeComponent();

            var dt = new DataTable();
            dt.Columns.Add("Fullname", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Checked", typeof(bool));
            FOLDER SongsFolder = new FOLDER(mainform.SONGSFOLDER);
            SONGS_LISTED = new BindingList<songlisted>();
            for (int i = 0; i < SongsFolder.totalfolders(); i++)
            {
                SONGS_LISTED.Add(new songlisted()
                {
                    index = i,
                    id = osu.only_id(isolate.fullname(SongsFolder.getfolders()[i])),
                    fullname = isolate.fullname(SongsFolder.getfolders()[i]),
                    name = osu.remove_id(isolate.fullname(SongsFolder.getfolders()[i]))
                });
            }
            foreach (var item in SONGS_LISTED) dt.Rows.Add(item.fullname, item.name, false);
            songCheckList.DataSource = dt.DefaultView;
            songCheckList.DisplayMember = "Fullname";
            songCheckList.ValueMember = "Name";
        }
        public static string[] acceptItems;
        public class songlisted
        {
            public string fullname { get; set; }
            public string name { get; set; }
            public string id { get; set; }
            public int index { get; set; }
            public bool IsChecked { get; set; }
            public override string ToString()
            {
                return fullname;
            }
        }
        List<string> selected_songs = new List<string>();
        private BindingList<songlisted> SONGS_LISTED;

        private void SongSelection_Load(object sender, EventArgs e)
        {

        }

        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectAllCheckBox.Checked)
            {
                for (int i = 0; i < songCheckList.Items.Count; i++)
                {
                    this.songCheckList.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < songCheckList.Items.Count; i++)
                {
                    this.songCheckList.SetItemChecked(i, false);
                }
            }
        }

        private void songCheckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)(() =>
                this.SongCount.Text = String.Format("{0} songs selected...", this.songCheckList.CheckedItems.Count)));
            var dv = songCheckList.DataSource as DataView;
            var drv = dv[e.Index];
            drv["Checked"] = e.NewValue == CheckState.Checked ? true : false;
        }

        private void searchbar_Enter(object sender, EventArgs e)
        {
            if (this.searchbar.Text == "Search...")
            {
                this.searchbar.Text = "";
                this.searchbar.ForeColor = System.Drawing.Color.WhiteSmoke;
            }
        }

        private void searchbar_TextChanged(object sender, EventArgs e)
        {
            this.SongCount.Text = String.Format("{0} song(s) selected...", this.songCheckList.CheckedItems.Count);
            var dv = songCheckList.DataSource as DataView;
            var filter = searchbar.Text.Trim().Length > 0
                ? $"Fullname LIKE '%{searchbar.Text}%*'"
                : null;

            dv.RowFilter = filter;

            for (var i = 0; i < songCheckList.Items.Count; i++)
            {
                var drv = songCheckList.Items[i] as DataRowView;
                var chk = Convert.ToBoolean(drv["Checked"]);
                songCheckList.SetItemChecked(i, chk);
            }
        }

        private void acceptbutton_Click(object sender, EventArgs e)
        {
            this.searchbar.Text = "";
            acceptItems = new string[songCheckList.CheckedItems.Count];
            for (int i = 0; i < this.songCheckList.CheckedItems.Count; i++)
            {
                DataRowView drv = songCheckList.CheckedItems[i] as DataRowView;
                acceptItems[i] = drv["Fullname"].ToString();
            }
            mainform.DidSelectSongs = true;
            DialogResult = DialogResult.OK;
        }
    }
}
