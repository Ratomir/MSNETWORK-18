using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSNetwork18.WebAPI.Model.Base
{
    public class ConfigurationModel
    {
        public string Database { get; set; }
        public string Collection { get; set; }
        public string[] ReplicationCenter { get; set; }

        public ConfigurationModel()
        {
            
        }

        public ConfigurationModel(IConfiguration configuration)
        {
            Database = configuration["configuration:database"];
            Collection = configuration["configuration:collection"];

            ReplicationCenter = configuration["configuration:replicationCenter"].Split(',');
        }
    }
}
