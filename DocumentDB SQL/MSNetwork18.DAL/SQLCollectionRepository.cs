﻿using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using MSNetwork18.DAL.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSNetwork18.DAL
{
    public class SQLCollectionRepository : BaseDatabaseRepository, ISQLCollectionRepository
    {
        public SQLCollectionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool CheckIfCollectionExistAsync(string databaseName, string collectionId) => GetDocumentCollection(databaseName, collectionId) != null;

        public async Task<DocumentCollection> CreateCollection(string databaseName, string collectionId)
        {
            Database database = GetDatabaseQuery(databaseName);

            DocumentCollection collection = new DocumentCollection()
            {
                Id = collectionId
            };

            DocumentCollection documentCollection = await Client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, collection, new RequestOptions() { OfferThroughput = 400 } );

            return documentCollection;
        }

        public async Task<bool> DeleteCollection(string databaseName, string collectionId)
        {
            DocumentCollection collection = await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionId));

            await Client.DeleteDocumentCollectionAsync(collection.SelfLink);
            bool isCollectionExist = CheckIfCollectionExistAsync(databaseName, collectionId);
            return !isCollectionExist; //collection don't exist, ok, we got false but we need inverse value because delete process
        }

        public DocumentCollection GetDocumentCollection(string databaseName, string collectionId)
        {
            Database database = GetDatabaseQuery(databaseName);

            DocumentCollection documentCollection = Client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(t => t.Id == collectionId).ToList().FirstOrDefault();

            return documentCollection;
        }

        public List<DocumentCollection> ReadAllCollections(string databaseName)
        {
            Database database = GetDatabaseQuery(databaseName);

            return Client.CreateDocumentCollectionQuery(database.CollectionsLink).ToList();
        }
    }
}
