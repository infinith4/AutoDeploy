using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDeploy.Utils
{
    public class FileUtil
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<string> SelectDirectories(string orgPath)
        {
            try
            {
                IEnumerable<string> dirs =
                    Directory.EnumerateDirectories(
                        orgPath, "*", SearchOption.AllDirectories);
                return dirs.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Error in SelectDirectories", ex);
                return null;
            }
        }

        public List<string> SelectFiles(string orgPath)
        {
            try
            {
                IEnumerable<string> files =
                    Directory.EnumerateFiles(
                        orgPath, "*", SearchOption.AllDirectories);
                return files.ToList();
            }
            catch (Exception ex)
            {
                logger.Error("Error in SelectFiles", ex);
                return null;
            }
        }

        public bool CopyToDestinationDir(string orgPath, List<Models.ImportYaml.DeserializedObject.Server> serverList)
        {
            bool isSuccess = false;
            List<bool> isSuccessList = new List<bool>();
            foreach (var server in serverList)
            {
                string destinationPath = server.path;
                bool isSuccessCopyDirs = CopyDirectories(orgPath, destinationPath, SelectDirectories(orgPath).ToList());
                if (isSuccessCopyDirs)
                {
                    bool isSuccessCopyFiles = CopyFiles(orgPath, destinationPath, SelectFiles(orgPath).ToList());
                    isSuccessList.Add(isSuccessCopyFiles);
                }
            }
            isSuccess = isSuccessList.All(c => c == true);
            return isSuccess;
        }

        public bool CopyDirectories(string orgPath, string destDirectory, List<string> dirs)
        {
            bool isSuccess = false;
            try
            {
                foreach (string dir in dirs)
                {
                    string dest_dir = Path.Combine(destDirectory, dir.Replace(orgPath, string.Empty));

                    if (!Directory.Exists(dest_dir))
                    {
                        Directory.CreateDirectory(dest_dir);
                    }
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                logger.Error("Error in CopyDirectories", ex);
            }
            return isSuccess;
        }

        public bool CopyFiles(string orgPath, string destDirectory, List<string> files)
        {
            bool isSuccess = false;
            try
            {
                foreach (string file in files)
                {
                    string dest_file = Path.Combine(destDirectory, file.Replace(orgPath, string.Empty));
                    File.Copy(file, dest_file, true);
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                logger.Error("Error in CopyFiles", ex);
            }
            return isSuccess;
        }
        
    }
}
