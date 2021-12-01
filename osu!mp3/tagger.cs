using ezf;
using System;
using System.Drawing;
using System.IO;

namespace tagger
{
    class METADATA
    {
        public static void TagSongs(FOLDER FOLDER_, int index)
        {
            FOLDER folder = new FOLDER(FOLDER_.getpath());
            SONG[] song = new SONG[folder.totalfiles()];

            song[index] = new SONG(folder.getfiles()[index]);
            song[index].SetTitleFromFile();
            song[index].SetArtistsFromFile();
            song[index].album = song[index].title;
            song[index].Save();
        }
        public static void ClearTagFromSongs(FOLDER FOLDER_, int index)
        {
            FOLDER folder = new FOLDER(FOLDER_.getpath());
            SONG[] song = new SONG[folder.totalfiles()];

            song[index] = new SONG(folder.getfiles()[index]);
            song[index].ClearTags();
            song[index].Save();
        }
        public static void SetCover(FOLDER FOLDER_, int index, string COVERPATH)
        {
            if (string.IsNullOrEmpty(COVERPATH))
            {
                return;
            }
            string FILENAME = FOLDER_.getfiles()[index];
            var dest = TagLib.File.Create(@FILENAME);
            MemoryStream ms = new MemoryStream();
            Image image = Image.FromFile(COVERPATH);
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            TagLib.Picture pic = new TagLib.Picture();
            pic.Data = TagLib.ByteVector.FromStream(ms);
            pic.Type = TagLib.PictureType.FrontCover;

            dest.Tag.Pictures = new TagLib.IPicture[] { pic };

            dest.Save();
        }
    }
    class SONG
    {
        public SONG(SONG song)
        {
            filename = song.filename;
            title = song.title;
            artists = song.artists;
            album = song.album;
            duration = song.duration;
        }
        public SONG(string FILENAME)
        {
            filename = FILENAME;
            var ts = TagLib.File.Create(filename);
            title = ts.Tag.Title;
            artists = ts.Tag.Performers;
            album = ts.Tag.Album;
            duration = ts.Properties.Duration;
        }
        public string filename;
        public string title { get; set; }
        public string[] artists { get; set; }
        public string album { get; set; }
        public TimeSpan duration { get; set; }
        public void Save()
        {
            var ts = TagLib.File.Create(filename);
            ts.Tag.Title = title;
            ts.Tag.Performers = artists;
            ts.Tag.Album = album;
            ts.Save();
        }
        public void SetTitleFromFile()
        {
            title = isolate.title(filename);
        }
        public void SetArtistsFromFile()
        {
            artists = new string[1] { isolate.artist(filename) };
        }
        public void ClearTags()
        {
            title = "";
            artists = new string[1] { "" };
            album = "";
        }
        public static SONG GetSong(FOLDER FOLDER_, int INDEX)
        {
            SONG song = new SONG(FOLDER_.getfiles()[INDEX]);
            return song;
        }
    }
    class isolate
    {
        public static string title(string FILENAME)
        {
            string isolated = FILENAME;
            if (FILENAME.Contains("\\"))
            {
                int lastslash = FILENAME.LastIndexOf("\\");
                isolated = FILENAME.Substring(lastslash + 1, (FILENAME.Length - 1) - lastslash);
            }
            if (isolated.Contains(" - "))
            {
                int lastdash = isolated.LastIndexOf(" - ");
                if (isolated.Contains(".mp3"))
                {
                    isolated = isolated.Substring(lastdash + 3, (isolated.Length - 7) - lastdash);
                }
                else
                {
                    isolated = isolated.Substring(lastdash + 3, (isolated.Length - 3) - lastdash);
                }
            }
            return isolated.Replace("_", " ");
        }
        public static string artist(string FILENAME)
        {
            string isolated = FILENAME;
            if (FILENAME.Contains("\\"))
            {
                int lastslash = FILENAME.LastIndexOf("\\");
                isolated = FILENAME.Substring(lastslash + 1, (FILENAME.Length - 1) - lastslash);
            }
            if (isolated.Contains(" - "))
            {
                int lastdash = isolated.LastIndexOf(" - ");
                isolated = isolated.Substring(0, lastdash);
            }
            return isolated.Replace("_", " ");
        }
        public static string fullname(string FILENAME)
        {
            string isolated = FILENAME;
            if (FILENAME.Contains("\\"))
            {
                int lastslash = FILENAME.LastIndexOf("\\");
                isolated = FILENAME.Substring(lastslash + 1, (FILENAME.Length - 1) - lastslash);
            }
            if (isolated.Contains(".mp3"))
            {
                isolated = isolated.Substring(0, (isolated.Length - 4));
            }
            else
            {
                isolated = isolated.Substring(0, (isolated.Length));
            }
            return isolated;
        }
        public static string[] fullnames(string[] FILENAMES)
        {
            string[] isolated = FILENAMES;
            for (int i = 0; i < FILENAMES.Length; i++)
            {
                if (FILENAMES[i].Contains("\\"))
                {
                    int lastslash = FILENAMES[i].LastIndexOf("\\");
                    isolated[i] = FILENAMES[i].Substring(lastslash + 1, (FILENAMES[i].Length - 1) - lastslash);
                }
                if (isolated[i].Contains(".mp3"))
                {
                    isolated[i] = isolated[i].Substring(0, (isolated[i].Length - 4));
                }
                else
                {
                    isolated[i] = isolated[i].Substring(0, (isolated[i].Length));
                }
            }
            return isolated;
        }
    }
}
