using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mp3ID3parser
{
    class ID3
    {
        public static void setTags(string FILENAME)
        {
            
            string filename = FILENAME;
            var dest = TagLib.File.Create(@filename);
            string title = modify.isolate_title(filename);
            string artist = modify.isolate_artist(filename);
            
            dest.Tag.Title = title;
            dest.Tag.Performers = new string[1] { artist };
            dest.Tag.AlbumArtists = new string[1] { artist };
            if (string.IsNullOrEmpty(dest.Tag.Album))
            {
                dest.Tag.Album = title;
            }
            if(dest.Tag.Track == 0)
            {
                dest.Tag.Track = 1;
            }
            dest.Save();
        }
        public static void setCover(string FILENAME, string COVERPATH)
        {
            if (string.IsNullOrEmpty(COVERPATH))
            {
                return;
            }
            string filename = FILENAME;
            var dest = TagLib.File.Create(@filename);
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
    class modify
    {
        public static string isolate_title(string FILENAME)
        {
            string isolated = FILENAME;
            if (FILENAME.Contains("\\"))
            {
                int lastslash = FILENAME.LastIndexOf("\\");
                isolated = FILENAME.Substring(lastslash + 1,(FILENAME.Length - 1)- lastslash);
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
            isolated.Replace("_", " ");
            return isolated;
        }
        public static string isolate_artist(string FILENAME)
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
            isolated.Replace("_", " ");
            return isolated;
        }
    }
}
