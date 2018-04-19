using MongoDB.Bson;
using System;

namespace RatomirBlog.Helper
{
    public static class ClassHelperExtension
    {
        /// <summary>
        /// Is object null.
        /// </summary>
        /// <param name="TestingObject"></param>
        /// <returns><see cref="bool"/>True/False</returns>
        public static bool IsObjectNull(this object TestingObject) => TestingObject == null ? true : false;

        public static BsonDocument DocumentWithObjectId(this string stringValue) => new BsonDocument("_id", new ObjectId(stringValue));

        public static ObjectId ToObjectId(this string stringValue) => new ObjectId(stringValue);

    }
}
