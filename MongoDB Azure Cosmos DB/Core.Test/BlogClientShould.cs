using Core.Test.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class BlogClientShould : BaseTest
    {
        public BlogClientShould() : base()
        {

        }

        [Fact]
        public async Task ConnectToMongoDb()
        {
            IAsyncCursor<BsonDocument> database = await Client.Connection.ListDatabasesAsync();

            Assert.True(database.Any());
        }

        [Theory]
        [InlineData("blog", "replay")]
        public async Task CreateAndGetNewCollection(string database, string collectionName)
        {
            IMongoDatabase databaseBlog = await Client.GetDatabaseAsync(database);

            if (!await Client.IsCollectionExist(collectionName))
            {
                await databaseBlog.CreateCollectionAsync(collectionName);
            }

            bool isCollectionExist = await Client.IsCollectionExist(collectionName);
            Assert.True(isCollectionExist);
        }


    }
}
