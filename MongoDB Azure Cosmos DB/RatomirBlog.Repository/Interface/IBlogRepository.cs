using MongoDB.Bson;
using MongoDB.Driver;
using RatomirBlog.Helper.Enum;
using RatomirBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RatomirBlog.Repository.Interface
{
    public interface IBlogRepository
    {
        Task InsertOneAsync<TModel>(BlogClient client, TModel insertModel) where TModel : BaseDocumentModel;
        Task<string> InsertOneGetIdAsync<TModel>(BlogClient client, TModel insertModel) where TModel : BaseDocumentModel;


        #region << Read methods >>

        Task<TModel> ReadOneAsync<TModel>(BlogClient client, Func<TModel, bool> func) where TModel : BaseDocumentModel;
        TSelect ReadOneAsync<TModel, TSelect>(BlogClient client, Func<TModel, bool> where, Func<TModel, TSelect> select) where TModel : BaseDocumentModel;
        Task<TModel> ReadOneByIdAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel;

        IEnumerable<TModel> ReadAll<TModel>(BlogClient client, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel;
        IEnumerable<TModel> ReadAll<TModel>(BlogClient client, Func<TModel, bool> func, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel;

        Task<List<TModel>> ReadAllAsync<TModel>(BlogClient client, ReadModeEnum mode = ReadModeEnum.Valid) where TModel : BaseDocumentModel;

        #endregion << Read methods >>

        #region << Update methods >>

        Task<bool> UpdateOneAsync<TModel>(BlogClient client, string documentId, UpdateDefinition<TModel> update) where TModel : BaseDocumentModel;

        #endregion << Update methods >>

        #region << Mark document methods >>

        Task<bool> MarkDocumentAsInvalidAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel;
        Task<bool> MarkDocumentAsValidAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel;

        #endregion << Mark document methods >>

        #region << Delete methods >>

        Task DeleteOneAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel;
        Task<bool> DeleteOneWithResultAsync<TModel>(BlogClient client, string documentId) where TModel : BaseDocumentModel;

        #endregion << Delete methods >>
    }
}
