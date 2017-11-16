using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            string orgPath = @"C:\Gitrepo\AutoDeploy\testdir\";
            IEnumerable<string> dirs =
                Directory.EnumerateDirectories(
                    orgPath, "*", System.IO.SearchOption.AllDirectories);
            IEnumerable<string> files =
                Directory.EnumerateFiles(
                    orgPath, "*", System.IO.SearchOption.AllDirectories);

            //ファイルを列挙する
            foreach (string f in files)
            {
                Console.WriteLine(f);
            }

            //To Directory
            string dest_directory = @"C:\Gitrepo\AutoDeploy\cp_testdir";

            foreach (string dir in dirs)
            {
                string dest_dir = string.Format(@"{0}\{1}", dest_directory, dir.Replace(orgPath, string.Empty));

                if (!Directory.Exists(dest_dir))
                {
                    Directory.CreateDirectory(dest_dir);
                }
            }
            foreach (string file in files)
            {
                string dest_file = string.Format(@"{0}\{1}", dest_directory, file.Replace(orgPath, string.Empty));
                File.Copy(file, dest_file, true);
            }
            Console.WriteLine("");
        }
    }
}
