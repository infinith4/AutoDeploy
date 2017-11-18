using log4net;
using System;

namespace AutoDeploy
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            try
            {
                var yamlImporter = new Utils.YamlUtil.YamlImporter();
                string deployConfigPath = Properties.Settings.Default.DeployConfigPath;
                var yamlDeserialized = yamlImporter.Deserialize(deployConfigPath);
                var fileUtil = new Utils.FileUtil();
                string orgPath = yamlDeserialized.originalpath;

                bool isCopySuccess = fileUtil.CopyToDestinationDir(orgPath, yamlDeserialized.servers);
                if (isCopySuccess)
                {
                    logger.Info("Copy To Files.");
                }
                else
                {
                    throw new Exception("Failed Copy Files");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Failed Copy Files");
            }
        }
    }
}
