using Core.Test.Base;
using RatomirBlog.Model;
using System.Collections.Generic;
using Xunit;

namespace Core.Test
{
    public class PostRepositoryShould : BaseTest
    {
        public PostRepositoryShould() : base()
        {

        }

        [Fact]
        public async void InsertPostModel()
        {
            IEnumerable<ReplayModel> allReplays = _repository.ReadAll<ReplayModel>(Client);

            PostModel newPost = new PostModel()
            {
                Content = "Neki string",
                Replays = allReplays
            };

            await _repository.InsertOneAsync(Client, newPost);

            AssertExtension.Success();
        }
    }
}
