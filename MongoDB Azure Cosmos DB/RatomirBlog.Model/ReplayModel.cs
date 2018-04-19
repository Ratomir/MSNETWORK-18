using MongoDB.Bson.Serialization.Attributes;
using RatomirBlog.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatomirBlog.Model
{
    [CollectionName(Collection.Replay)]
    public class ReplayModel : BaseDocumentModel
    {
        public string Text { get; set; }

        [BsonElement("User")]
        public UserModel User { get; set; }
    }
}
