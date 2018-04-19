using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RatomirBlog.Helper.Enum;
using RatomirBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RatomirBlog.Repository.ExtensionsOnMongo
{
    public static class IMongoExtraExtensions
    {
        public static IEnumerable<TDocument> BaseQuery<TDocument>(this IMongoCollection<TDocument> collection, ReadModeEnum mode) where TDocument : BaseDocumentModel
        {
            Func<TDocument, bool> func = (s) =>
            {
                if (mode == ReadModeEnum.Valid)
                    return !s.IsInvalid;

                if (mode == ReadModeEnum.Invalid)
                    return s.IsInvalid;

                return s.IsInvalid || !s.IsInvalid;
            };

            return collection.AsQueryable().Where(func);
        }

        public static ParallelQuery<TDocument> BaseQueryAsParallel<TDocument>(this IMongoCollection<TDocument> collection, ReadModeEnum mode) where TDocument : BaseDocumentModel
        {
            Func<TDocument, bool> func = (s) =>
            {
                if (mode == ReadModeEnum.Valid)
                    return !s.IsInvalid;

                if (mode == ReadModeEnum.Invalid)
                    return s.IsInvalid;

                return s.IsInvalid || !s.IsInvalid;
            };

            return collection.AsQueryable().Where(func).AsParallel();
        }
    }
}
