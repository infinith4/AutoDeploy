using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace AutoDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            var yamlImporter = new Utils.YamlUtils.YamlImporter();
            Models.ImportYaml.DeserializedObject obj = yamlImporter.Deserialize("config.yaml");

            string orgPath = obj.originalpath;
            IEnumerable<string> dirs =
                Directory.EnumerateDirectories(
                    orgPath, "*", SearchOption.AllDirectories);
            IEnumerable<string> files =
                Directory.EnumerateFiles(
                    orgPath, "*", SearchOption.AllDirectories);

            //ファイルを列挙する
            foreach (string f in files)
            {
                Console.WriteLine(f);
            }

            foreach (var item in obj.servers)
            {
                //To Directory
                string dest_directory = item.path;

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
            }
        }
    }
}
