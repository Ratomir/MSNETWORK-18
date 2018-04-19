﻿using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using MSNetwork18.DAL.Interface;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSNetwork18.DAL
{
    public class SQLDocumentRepository : BaseDatabaseRepository, ISQLDocumentRepository
    {
        public SQLDocumentRepository(IConfiguration configuration):base(configuration)
        {
        }

        public async Task<bool> DeleteDocument(string databaseId, string collectionId, string id)
        {
            await Client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, id));
            Document document = await ReadDocumentByIdAsync<Document>(id, UriFactory.CreateDocumentCollectionUri(databaseId, collectionId));
            return document == null;
        }

        public async Task<Document> InsertDocument(string databaseId, string collectionId, object document, RequestOptions requestOptions = null)
        {
            Document documentResponse = await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, collectionId), document, requestOptions);
            return documentResponse;
        }

        public async Task<Document> InsertDocument(Uri collectionLink, object document, RequestOptions requestOptions = null)
        {
            Document documentResponse = await Client.CreateDocumentAsync(collectionLink, document, requestOptions);
            return documentResponse;
        }

        public async Task<T> ReadDocumentByIdAsync<T>(object id, Uri collectionUri)
        {
            SqlQuerySpec query = new SqlQuerySpec()
            {
                QueryText = "SELECT * FROM root r WHERE r.id = @id",
                Parameters = new SqlParameterCollection()
                {
                    new SqlParameter("@id", id)
                }
            };

            T document = await Task.Run(() => Client.CreateDocumentQuery<T>(collectionUri, query, DefaultOptions).ToList().FirstOrDefault());
            return document;
        }

        public IQueryable<T> ReadDocumentByQuery<T>(Uri collectionUri, SqlQuerySpec query)
        {
            return Client.CreateDocumentQuery<T>(collectionUri, query, DefaultOptions);
        }

        public async Task<T> UpdateDocument<T>(Uri documentLink, object document, RequestOptions requestOptions = null) where T:class
        {
            Document result = await Client.ReplaceDocumentAsync(documentLink, document, requestOptions);
            if (typeof(T) != typeof(Document))
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(result));
            }

            return result as T;
        }
    }
}
