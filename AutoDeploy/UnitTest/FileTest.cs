using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoDeploy;
using System.IO;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class FileTest
    {
        [TestMethod]
        public void CheckExistFiles()
        {
            var yamlImporter = new AutoDeploy.Utils.YamlUtil.YamlImporter();
            string deployConfigPath = Properties.Settings.Default.DeployConfigPath;
            var yamlDeserialized = yamlImporter.Deserialize(deployConfigPath);
            //yamlDeserialized.servers;
            var fileUtil = new AutoDeploy.Utils.FileUtil();

            var orgFiles = fileUtil.SelectFiles(yamlDeserialized.originalpath);
            orgFiles.Sort();
            foreach (var server in yamlDeserialized.servers)
            {
                string destPath = server.path;

                var destFiles = fileUtil.SelectFiles(destPath);
                List<string> replaceFileList = new List<string>();
                foreach (var destFile in destFiles)
                {
                    replaceFileList.Add(destFile.Replace(destPath, string.Empty));
                }
                replaceFileList.Sort();
                if (replaceFileList.Count == orgFiles.Count)
                {
                    for (int i = 0; i < replaceFileList.Count; i++)
                    {
                        string orgFileName = orgFiles[i].Replace(yamlDeserialized.originalpath, string.Empty);
                        StringAssert.Contains(replaceFileList[i], orgFileName);
                    }
                }
                else
                {
                    throw new AssertFailedException();
                }
            }
        }

        [TestMethod]
        public void CheckExistDirectories()
        {
            var yamlImporter = new AutoDeploy.Utils.YamlUtil.YamlImporter();
            string deployConfigPath = Properties.Settings.Default.DeployConfigPath;
            var yamlDeserialized = yamlImporter.Deserialize(deployConfigPath);
            var fileUtil = new AutoDeploy.Utils.FileUtil();

            var orgDirs = fileUtil.SelectDirectories(yamlDeserialized.originalpath);
            orgDirs.Sort();
            foreach (var server in yamlDeserialized.servers)
            {
                string destPath = server.path;

                var destDirs = fileUtil.SelectDirectories(destPath);
                List<string> replaceDirList = new List<string>();
                foreach (var destDir in destDirs)
                {
                    replaceDirList.Add(destDir.Replace(destPath, string.Empty));
                }
                replaceDirList.Sort();
                if (replaceDirList.Count == orgDirs.Count)
                {
                    for (int i = 0; i < replaceDirList.Count; i++)
                    {
                        string orgDirName = orgDirs[i].Replace(yamlDeserialized.originalpath, string.Empty);
                        StringAssert.Contains(replaceDirList[i], orgDirName);
                    }
                }
                else
                {
                    throw new AssertFailedException();
                }
            }
        }
    }
}
