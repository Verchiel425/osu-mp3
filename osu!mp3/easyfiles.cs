using System.Diagnostics;
using System.IO;

namespace ezf
{
    class NAME
    {
        public static string isolate(string target)
        {
            return (target.Substring
                (target.LastIndexOf("\\") + 1
                , (target.Length - 1) -
                (target.LastIndexOf("\\"))));
        }
    }
    class FOLDER
    {
        string PATH;
        public FOLDER() { }
        public FOLDER(string path)
        {
            while (path.EndsWith("\\"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            if (Directory.Exists(path))
            {
                PATH = Path.GetFullPath(path);
            }
            else
            {
                Directory.CreateDirectory(path);
                PATH = Path.GetFullPath(path);
            }
        }
        public string getpath()
        {
            if (PATH == null)
            {
                return "No path set";
            }
            else
            {
                while (PATH.EndsWith("\\"))
                {
                    PATH = PATH.Substring(0, PATH.Length - 1);
                }
                return PATH;
            }
        }
        public void setPath(string path)
        {
            while (path.EndsWith("\\"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            if (Directory.Exists(path))
            {
                PATH = Path.GetFullPath(path);
            }
            else
            {
                Directory.CreateDirectory(path);
                PATH = Path.GetFullPath(path);
            }
        }
        public string[] getfiles()
        {
            return Directory.GetFiles(PATH);
        }
        public string[] getfolders()
        {
            return Directory.GetDirectories(PATH);
        }
        public int totalfolders()
        {
            return Directory.GetDirectories(PATH).Length;
        }
        public static int totalfolders(string path)
        {
            try
            {
                return Directory.GetDirectories(path).Length;
            }
            catch (System.ArgumentException)
            {
                return 0;
            }
        }
        public int totalfiles()
        {
            return Directory.GetFiles(PATH).Length;
        }
        public static int totalfiles(string path)
        {
            return Directory.GetFiles(path).Length;
        }
        public void moveUp()
        {
            PATH = Directory.GetParent(PATH).FullName;
        }
        public void moveDown(string targetfolder)
        {
            if (targetfolder[0].Equals("\\"))
            {
                string newpath = PATH + targetfolder;
                setPath(newpath);
            }
            else
            {
                string newpath = PATH + "\\" + targetfolder;
                setPath(newpath);
            }
        }
    }
    class FILES : FOLDER
    {
        public static void create(string name)
        {
            File.Create(name);
        }
        public static void create(string name, FOLDER folder)
        {
            string fullpath = folder.getpath() + "\\" +name;
            File.Create(fullpath);
        }
        public static int copy(FOLDER sourcefolder, string sourcefilename, FOLDER destfolder, string destfilename)
        {

            string source = sourcefolder.getpath() + "\\" + sourcefilename;
            string dest = destfolder.getpath() + "\\" + destfilename;
            if (File.Exists(dest))
            {
                return 1;
            }
            File.Copy(source, dest,true);
            return 0;
        }
        public static void startprocess(FOLDER folder,string filename)
        {
            string name = folder.getpath() + "\\" + filename;
            if (File.Exists(name)) { 
                Process.Start(name); 
            }
        }
    }
}
