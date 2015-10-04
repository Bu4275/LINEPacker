using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LinePacker
{
    class Program
    {
        static void Main(string[] args)
        {
            string Folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\LINE";
            int count = 0;
            string newFolder = "";
            if (Directory.Exists(Folder))
            {
                while (true)
                {
                    newFolder = "LINE" + count.ToString();
                    if (Directory.Exists(newFolder))
                    {
                        count++;
                        continue;
                    }

                    Directory.CreateDirectory(newFolder);
                    break;
                }


                MiningFile(Folder, @".\" + newFolder, true);
            }
            else
            {
                Console.WriteLine("未安裝電腦版LINE");
                Console.Read();
            }
        }


        private static void MiningFile(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            if (!Directory.Exists(Path.Combine(destDirName, "Picture")))
            {
                Directory.CreateDirectory(Path.Combine(destDirName, "Picture"));
            }
            if (!Directory.Exists(Path.Combine(destDirName, "Sticker")))
            {
                Directory.CreateDirectory(Path.Combine(destDirName, "Sticker"));
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (Path.GetExtension(file.Name) == ".png")
                {
                    temppath = Path.Combine(destDirName, "Sticker", file.Name);
                }else if (Path.GetExtension(file.Name) == "")
                {
                    temppath = Path.Combine(destDirName, "Picture", file.Name + ".jpg");
                }

                Console.WriteLine(Path.Combine(dir.FullName, file.Name));
                if (File.Exists(temppath)) continue;
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    //string temppath = Path.Combine(destDirName, subdir.Name);
                    MiningFile(subdir.FullName, destDirName, copySubDirs);
                }
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
