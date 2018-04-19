using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatomirBlog.Model
{
    public class BaseDocumentModel
    {
        [BsonId]
        public ObjectId DocumentId { get; set; }

        public bool IsInvalid { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [BsonRequired]
        public string CreatedUser { get; set; }
    }
}
