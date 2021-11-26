using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace osu_mp3
{
    internal static class Program
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
    };
    class config
    {
        public static int write(string[] input)
        {
            File.WriteAllLines("config.json", input);
            return 0;
        }
        public static int append(string[] input)
        {
            File.AppendAllLines("config.json", input);
            return 0;
        }
        public static string read(int index)
        {
            if (File.Exists("config.json"))
            {
                string[] output = File.ReadAllLines("config.json");
                string dir;
                if (output[index].Length < 4) { return ""; }
                else
                {
                    dir = output[index].Substring(4, (output[index].Length) - 4);
                }
                return dir;
            }
            else
            {
                return "";
            }
        }
        public static void save(string[] SOURCE, string[] DESTINATION, string[] SONGS_OVERRIDE, string[] OVERRIDE_CHECKED)
        {
            config.write(SOURCE);
            config.append(DESTINATION);
            config.append(SONGS_OVERRIDE);
            config.append(OVERRIDE_CHECKED);
        }
    };
}
