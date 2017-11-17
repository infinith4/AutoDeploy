using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace AutoDeploy.Models
{
    public class ImportYaml
    {
        public class DeserializedObject
        {
            [YamlMember(Alias = "originalpath")]
            public string originalpath { get; set; }

            [YamlMember(Alias = "servers")]
            public List<Server> servers { get; set; }

            public class Server
            {
                public string path { get; set; }
            }
        }
    }
}
