using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using MSNetwork18.DAL.Interface;
using System;
using System.Linq;

namespace MSNetwork18.DAL
{
    public class BaseDatabaseRepository : IBaseRepository
    {
        public DocumentClient Client { get; set; }
        public FeedOptions DefaultOptions = new FeedOptions { EnableCrossPartitionQuery = true };
        public string EndPoint { get; set; }
        public string MasterKey { get; set; }

        public Uri UriCosmosDB { get; set; }
        public string DatabaseId { get; set; }
        public string CollectionId { get; set; }

        public BaseDatabaseRepository(IConfiguration configuration)
        {
            EndPoint = configuration.GetSection("endpointUrl").Value;
            MasterKey = configuration.GetSection("masterKey").Value;

            UriCosmosDB = new Uri(EndPoint);
            ConnectionPolicy connectionPolicy = new ConnectionPolicy();

            DatabaseId = configuration["configuration:database"];
            CollectionId = configuration["configuration:collection"];

            configuration["configuration:replicationCenter"].Split(',').ToList().ForEach(azureRegion => connectionPolicy.PreferredLocations.Add(azureRegion));

            //Obsolete Here is the old way to load preferred azure region location. It's moved to application configuration.
            //connectionPolicy.PreferredLocations.Add(LocationNames.WestUS);
            //connectionPolicy.PreferredLocations.Add(LocationNames.NorthEurope);
            //connectionPolicy.PreferredLocations.Add(LocationNames.FranceCentral);

            Client = new DocumentClient(UriCosmosDB, MasterKey, connectionPolicy, ConsistencyLevel.Session);
        }

        public Database GetDatabaseQuery(string databaseName)
        {
            SqlQuerySpec query = new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM c WHERE c.id = @id",
                Parameters = new SqlParameterCollection()
                {
                    new SqlParameter("@id", databaseName)
                }
            };

            return Client.CreateDatabaseQuery(query).AsEnumerable().First();
        }
    }
}
