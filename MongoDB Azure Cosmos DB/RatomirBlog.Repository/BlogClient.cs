using MongoDB.Bson;
using MongoDB.Driver;
using RatomirBlog.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using RatomirBlog.Model.Attributes;
using MongoDB.Driver.GridFS;
using System.Security.Authentication;
using Microsoft.Extensions.Configuration;

namespace RatomirBlog.Repository
{
    public class BlogClient : MongoClient
    {
        private const string ConnectionString =
  @"mongodb://ratomirblog:JxmbGX2vjOku1ijF3Cn3G6CUN7hKvbm4TTHWm2sqqKxQxb4n6K7kOIhbnHnuh0Ne0k3Fa8FOm1qOBl0qPNtcbQ==@ratomirblog.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";

        private BlogClient _connection;

        public string Database { get; set; } = string.Empty;

        public BlogClient Connection
        {
            get
            {
                if (_connection.IsObjectNull())
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                    _connection = new BlogClient(settings);
                }

                return _connection;
            }
        }

        /// <summary>
        /// Test constructor
        /// </summary>
        public BlogClient()
        {
            Database = "blog";
        }

        public BlogClient(IConfiguration configuration)
        {
            Database = configuration.GetSection("Database").Value;

            if (_connection.IsObjectNull())
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration.GetConnectionString("mongoazure")));
                settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                _connection = new BlogClient(settings);
            }
        }

        public BlogClient(MongoClientSettings mongoClientSettings) : base(mongoClientSettings)
        {
        }

        public async Task<IMongoDatabase> GetDatabaseAsync(string databaseName)
        {
            return await Task.Run(() => Connection.GetDatabase(databaseName));
        }

        public async Task<bool> IsCollectionExist(string collectionName, CancellationToken cancellationToken = default(CancellationToken))
        {
            IMongoDatabase database = await GetDatabaseAsync(Database);
            return await Task.Run(() => database.ListCollections().ToList().Any(t => collectionName == t["name"].AsString), cancellationToken);
        }

        public IMongoCollection<TModel> GetCollection<TModel>()
        {
            IMongoDatabase database = GetDatabaseAsync(Database).Result;
            return database.GetCollection<TModel>(Collection<TModel>());
        }

        public string Collection<TModel>() => typeof(TModel).GetAttributeValue((CollectionNameAttribute cna) => cna.Name);

        public GridFSBucket GridFSBucket => new GridFSBucket(GetDatabaseAsync(Database).Result);

        public GridFSBucket GridFSBucketWithOptions(GridFSBucketOptions options) => new GridFSBucket(GetDatabaseAsync(Database).Result, options);
    }
}
