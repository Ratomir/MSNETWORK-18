using MongoDB.Bson.Serialization.Attributes;
using RatomirBlog.Model.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RatomirBlog.Model
{
    [CollectionName(Collection.User)]
    public class UserModel : BaseDocumentModel
    {
        [BsonIgnoreIfNull]
        public string FirstName { get; set; }

        [BsonIgnoreIfNull]
        public string LastName { get; set; }

        [BsonIgnoreIfNull]
        public string Username { get; set; }

        [BsonIgnoreIfNull]
        public string Email { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? DateOfBirth { get; set; }
    }
}
