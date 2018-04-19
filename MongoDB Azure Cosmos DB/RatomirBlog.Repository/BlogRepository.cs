using MongoDB.Driver;
using RatomirBlog.Helper;
using RatomirBlog.Model;
using RatomirBlog.Model.Attributes;
using RatomirBlog.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using RatomirBlog.Helper.Enum;
using RatomirBlog.Repository.ExtensionsOnMongo;

namespace RatomirBlog.Repository
{
    public class BlogRepository : IBlogRepository
    {
        #region << Delete methods >>

        public async Task DeleteOneAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel
        {
            IMongoCollection<TModel> collection = client.GetCollection<TModel>();

            await collection.FindOneAndDeleteAsync(documentId.DocumentWithObjectId());
        }

        public async Task<bool> DeleteOneWithResultAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel
        {
            IMongoCollection<TModel> collection = client.GetCollection<TModel>();

            DeleteResult result = await collection.DeleteOneAsync(documentId.DocumentWithObjectId());
            return result.DeletedCount == 1;
        }

        #endregion << Delete methods >>

        public async Task InsertOneAsync<TModel>(BlogClient client, TModel insertModel) where TModel : BaseDocumentModel
        {
            await client.GetCollection<TModel>().InsertOneAsync(insertModel);
        }

        #region << Mark document methods >>

        public async Task<bool> MarkDocumentAsInvalidAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel
        {
            UpdateDefinition<TModel> updateDefinition = Builders<TModel>.Update.Set("IsInvalid", true);
            UpdateResult result = await client.GetCollection<TModel>().UpdateOneAsync(documentId.DocumentWithObjectId(), updateDefinition);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> MarkDocumentAsValidAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel
        {
            UpdateDefinition<TModel> updateDefinition = Builders<TModel>.Update.Set("IsInvalid", false);
            UpdateResult result = await client.GetCollection<TModel>().UpdateOneAsync(documentId.DocumentWithObjectId(), updateDefinition);

            return result.ModifiedCount == 1;
        }

        #endregion << Mark document methods >>

        #region << Read methods >>

        public async Task<List<TModel>> ReadAllAsync<TModel>(BlogClient client, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel
        {
            return await Task.Run(() => client.GetCollection<TModel>().BaseQueryAsParallel(mode).ToList());
        }

        public IEnumerable<TModel> ReadAll<TModel>(BlogClient client, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel
        {
            return client.GetCollection<TModel>().BaseQueryAsParallel(mode);
        }

        public IEnumerable<TModel> ReadAll<TModel>(BlogClient client, Func<TModel, bool> func, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel
        {
            return client.GetCollection<TModel>().BaseQueryAsParallel(mode).Where(func);
        }

        public async Task<TModel> ReadOneAsync<TModel>(BlogClient client, Func<TModel, bool> func) where TModel : BaseDocumentModel
        {
            return await Task.Run(() => client.GetCollection<TModel>().AsQueryable().Where(func).First());
        }

        public TSelect ReadOneAsync<TModel, TSelect>(BlogClient client, Func<TModel, bool> where, Func<TModel, TSelect> select) where TModel : BaseDocumentModel
        {
            return client.GetCollection<TModel>().AsQueryable().Where(where).Select(select).First();
        }

        public async Task<TModel> ReadOneByIdAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel
        {
            IAsyncCursor<TModel> filterFind =  await client.GetCollection<TModel>().FindAsync(documentId.DocumentWithObjectId());

            filterFind.MoveNext();
            return filterFind.Current.Single();
        }

        #endregion << Read methods >>

        #region << Update methods >>

        public async Task<bool> UpdateOneAsync<TModel>(BlogClient client, string documentId, UpdateDefinition<TModel> update) where TModel : BaseDocumentModel
        {
            UpdateResult result = await client.GetCollection<TModel>().UpdateOneAsync(documentId.DocumentWithObjectId(), update);

            return result.ModifiedCount == 1;
        }

        public async Task<string> InsertOneGetIdAsync<TModel>(BlogClient client, TModel insertModel) where TModel : BaseDocumentModel
        {
            await client.GetCollection<TModel>().InsertOneAsync(insertModel);
            return insertModel.DocumentId.ToString();
        }

        #endregion << Update methods >>

    }
}
