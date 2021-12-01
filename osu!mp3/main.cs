using ezf;
using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace osu_mp3
{
    internal static class main
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new mainform());
        }
    }
    class osu
    {
        public static string src;
        public static string dst;
        public static bool isSuccess = true;
        static string currently_extracting;
        public static string osuToDir(string FileName)
        {
            if ((FileName).Contains("osu!.exe"))
            {
                string l_src = FileName;
                int i = l_src.Length - 1;
                while ((l_src[i]).ToString() != "\\")
                {
                    i--;
                }

                src = l_src.Substring(0, i);
                return src;
            }
            return null;
        }
        public static void setDstDir(string DESTINATION)
        {
            dst = DESTINATION;
        }
        public static string getOsuDir()
        {
            return src;
        }
        public static string getDstDir()
        {
            return dst;
        }
        public static int extract(string songdir, string destdir, int index)
        {
            if (string.IsNullOrEmpty(songdir) || string.IsNullOrEmpty(destdir))
            {
                return -1;
            }

            FOLDER source = new FOLDER(songdir);
            FOLDER dest = new FOLDER(destdir);
            FOLDER songfolder = new FOLDER();

            try
            {
                songfolder.setPath(source.getfolders()[index]);
            }
            catch (System.IndexOutOfRangeException)
            {
                return -2;
            }

            string songname = remove_id(NAME.isolate(songfolder.getpath())) + ".mp3";
            for (int i = 0; i < songfolder.totalfiles(); i++)
            {
                if ((songfolder.getfiles()[i]).Contains(".mp3") || (songfolder.getfiles()[i]).Contains(".Mp3") || (songfolder.getfiles()[i]).Contains("audio.ogg"))
                {
                    if ((FILES.copy(songfolder, NAME.isolate(songfolder.getfiles()[i]), dest, songname)) == 1)
                    {
                        currently_extracting = remove_id(NAME.isolate(songfolder.getpath()));
                        return 1;
                    }
                    currently_extracting = remove_id(NAME.isolate(songfolder.getpath()));
                }
            }
            if (songfolder.totalfiles() == 0)
            {
                return -1;
            }
            return 0;
        }
        public static string getPic(string songdir, string songname)
        {
            FOLDER folder = new FOLDER(songdir);
            FOLDER songfolder = new FOLDER();
            string coverpath = "";

            for (int index = 0; index < folder.totalfolders(); index++)
            {
                if (((folder.getfolders()[index]).ToUpper()).Contains(songname.ToUpper()))
                {
                    songfolder.setPath(folder.getfolders()[index]);
                }
            }
            for (int i = 0; i < songfolder.totalfiles(); i++)
            {
                if ((songfolder.getfiles()[i]).Contains(".jpeg") || (songfolder.getfiles()[i]).Contains(".jpg") || (songfolder.getfiles()[i]).Contains(".png"))
                {
                    coverpath = songfolder.getfiles()[i];
                }
            }
            if (songfolder.totalfiles() == 0)
            {
                return "";
            }
            return coverpath;
        }
        public static string currentlyextracting()
        {
            return currently_extracting;
        }
        public static string remove_id(string str)
        {
            int s = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsWhiteSpace(str[i]))
                {
                    s = i;
                    break;
                }
            }
            if (s == 0)
            {
                return str.Substring(s, (str.Length));
            }
            return str.Substring(s + 1, (str.Length - 1) - s);
        }
        public static string only_id(string str)
        {
            int s = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsWhiteSpace(str[i]))
                {
                    s = i;
                    break;
                }
            }
            if (s == 0)
            {
                return str.Substring(s, (str.Length));
            }
            return str.Substring(0, s);
        }
    };
    public class LOG
    {
        public string time { get; set; }
        public int items { get; set; }
        public string operation { get; set; }
    }
    public class CONFIG
    {
        public string osudir { get; set; }
        public string songdir { get; set; }
        public string destdir { get; set; }
        public string overridestate { get; set; }
        public string operation { get; set; }
    };
    class config
    {
        public static void save(string OSUDIR, string SONGDIR, string DESTDIR, bool OVERRIDE_STATE, int OPERATION)
        {
            CONFIG defconfig = new CONFIG
            {
                osudir = OSUDIR,
                songdir = SONGDIR,
                destdir = DESTDIR,
                overridestate = OVERRIDE_STATE.ToString(),
                operation = OPERATION.ToString()
            };

            string configname = "config.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string configjson = JsonSerializer.Serialize(defconfig, options);
            File.WriteAllText(configname, configjson);
        }
        public static string osudir()
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string jsonString = File.ReadAllText("config.json");
                    CONFIG defconfig = JsonSerializer.Deserialize<CONFIG>(jsonString);
                    return defconfig.osudir;
                }
                catch (System.Text.Json.JsonException)
                {
                    MessageBox.Show("config.json is corrupted!", "Config Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("config.json");
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static string songdir()
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string jsonString = File.ReadAllText("config.json");
                    CONFIG defconfig = JsonSerializer.Deserialize<CONFIG>(jsonString);
                    return defconfig.songdir;
                }
                catch (System.Text.Json.JsonException)
                {
                    MessageBox.Show("config.json is corrupted!", "Config Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("config.json");
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static string destdir()
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string jsonString = File.ReadAllText("config.json");
                    CONFIG defconfig = JsonSerializer.Deserialize<CONFIG>(jsonString);
                    return defconfig.destdir;
                }
                catch (System.Text.Json.JsonException)
                {
                    MessageBox.Show("config.json is corrupted!", "Config Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("config.json");
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static string overridestate()
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string jsonString = File.ReadAllText("config.json");
                    CONFIG defconfig = JsonSerializer.Deserialize<CONFIG>(jsonString);
                    return defconfig.overridestate;
                }
                catch (System.Text.Json.JsonException)
                {
                    MessageBox.Show("config.json is corrupted!", "Config Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("config.json");
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public static string operation()
        {
            if (File.Exists("config.json"))
            {
                try
                {
                    string jsonString = File.ReadAllText("config.json");
                    CONFIG defconfig = JsonSerializer.Deserialize<CONFIG>(jsonString);
                    return defconfig.operation;
                }
                catch (System.Text.Json.JsonException)
                {
                    MessageBox.Show("config.json is corrupted!", "Config Corrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    File.Delete("config.json");
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    };
    class log
    {
        public static void save(string TIME, int ITEMS, int OPERATION)
        {
            string TEMP = "ExtractAndTag";
            switch (OPERATION)
            {
                case 0:
                    TEMP = "ExtractAndTag";
                    break;
                case 1:
                    TEMP = "ExtractOnly";
                    break;
                case 2:
                    TEMP = "TagOnly";
                    break;
                default:
                    break;
            }
            LOG log = new LOG
            {
                time = TIME,
                items = ITEMS,
                operation = TEMP
            };

            string logname = "log.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string logjson = JsonSerializer.Serialize(log, options);
            File.AppendAllText(logname, logjson);
        }
    }
}