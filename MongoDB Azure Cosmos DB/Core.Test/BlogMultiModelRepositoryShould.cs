using Core.Test.Base;
using MongoDB.Bson;
using RatomirBlog.Helper;
using RatomirBlog.Model;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class BlogMultiModel_repositoryShould : BaseTest
    {
        public BlogMultiModel_repositoryShould() : base()
        {

        }

        [Fact]
        public async Task TestInsert()
        {
            ReplayModel replayModel = new ReplayModel()
            {
                Text = "Ovo je neki replay od nekog cove",
                User = new UserModel()
                {
                    FirstName = "Neki cova opet",
                    LastName = "Rock brother"
                }
            };

            await _repository.InsertOneAsync(Client, replayModel);
        }

        [Fact]
        public async Task TestRead()
        {
            ReplayModel readOneReplay = await _repository.ReadOneByIdAsync<ReplayModel>(Client, "5a6b2d3bce9f084240e56a89");
        }

        [Fact]
        public async Task ReadWithDelegate()
        {
            ReplayModel readOneReplay = await _repository.ReadOneAsync<ReplayModel>(Client, t => t.User.LastName == "Rock brother");
        }

        [Fact]
        public void ReadWIthSelectMethod()
        {
            ObjectId userModel = _repository.ReadOneAsync<ReplayModel, ObjectId>(Client, t => t.DocumentId == "5a6b3a44c905b608b8d854b8".ToObjectId(), r => r.User.DocumentId);
        }

    }
}
