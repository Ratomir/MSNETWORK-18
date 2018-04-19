using MongoDB.Bson.Serialization.Attributes;
using RatomirBlog.Model.Attributes;
using System;
using System.Collections.Generic;

namespace RatomirBlog.Model
{
    [CollectionName(Collection.Post)]
    public class PostModel : BaseDocumentModel
    {
        [BsonRequired]
        public string Content { get; set; }

        [BsonIgnoreIfDefault]
        public IEnumerable<ReplayModel> Replays { get; set; }
    }
}
