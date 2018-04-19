using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSNetwork18.DAL.Interface
{
    public interface ISQLDocumentRepository : IBaseRepository
    {
        IQueryable<T> ReadDocumentByQuery<T>(Uri collectionUri, SqlQuerySpec query);
        Task<T> ReadDocumentByIdAsync<T>(object id, Uri collectionUri);
        Task<Document> InsertDocument(string databaseId, string collectionId, object document, RequestOptions requestOptions = null);
        Task<Document> InsertDocument(Uri collectionLink, object document, RequestOptions requestOptions = null);
        Task<bool> DeleteDocument(string databaseId, string collectionId, string id);

        Task<T> UpdateDocument<T>(Uri documentLink, object document, RequestOptions requestOptions = null) where T : class;
    }
}
