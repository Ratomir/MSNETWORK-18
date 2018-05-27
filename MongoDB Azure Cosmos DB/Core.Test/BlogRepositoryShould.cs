using Core.Test.Base;
using MongoDB.Driver;
using RatomirBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Core.Test
{
    public class BlogRepositoryShould : BaseTest
    {
        public BlogRepositoryShould() : base()
        {

        }

        [Fact]
        public async Task InsertNewUser()
        {
            UserModel newUserModel = new UserModel()
            {
                CreatedUser = "admin",
                Username = "ratomirx",
                Email = "ratomir@live.com",
                FirstName = "Ratomir",
                LastName = "Vukadin",
                IsInvalid = false,
                CreatedDate = DateTime.Now,
                DateOfBirth = new DateTime(1992, 7, 9),
            };

            await _repository.InsertOneAsync(Client, newUserModel);
        }

        [Fact]
        public async Task ReadOne()
        {
            UserModel userModel = await _repository.ReadOneAsync<UserModel>(Client, t => t.FirstName == "Ratomir novi upis");

            Assert.Equal("Ratomir novi upis", userModel.FirstName);
        }

        [Fact]
        public async Task ReadAll()
        {
            List<UserModel> userModel = await _repository.ReadAllAsync<UserModel>(Client);
            AssertExtension.Success();
        }

        [Fact]
        public void ReadAllAsEnumerable()
        {
            IEnumerable<UserModel> enumerableUserModelList = _repository.ReadAll<UserModel>(Client);
            List<UserModel> userModel = enumerableUserModelList.ToList();
            AssertExtension.Success();
        }

        [Fact]
        public async Task ReadOneById()
        {
            UserModel userModel = await _repository.ReadOneByIdAsync<UserModel>(Client, "5a69b10ddcf6e8eaec1559f5");
        }

        [Fact]
        public async Task UpdateOneById()
        {
            UpdateDefinition<UserModel> updateDocument = Builders<UserModel>.Update.Set(t => t.FirstName, "Update ratomir");
            updateDocument = updateDocument.Set(t => t.LastName, "Pokemoni");

            bool result = await _repository.UpdateOneAsync(Client, "5a6f01634d079623b07fa5af", updateDocument);

            Assert.True(result);
        }

        [Fact]
        public async Task MarkDocumentAsInvalid()
        {
            bool result = await _repository.MarkDocumentAsInvalidAsync<UserModel>(Client, "5a69b10ddcf6e8eaec1559f5");

            Assert.True(result);
        }

        [Fact]
        public async Task MarkDocumentAsValid()
        {
            bool result = await _repository.MarkDocumentAsValidAsync<UserModel>(Client, "5a69b10ddcf6e8eaec1559f5");

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteById()
        {
            await _repository.DeleteOneAsync<UserModel>(Client, "5a69f5eb5d290409b0d30541");
        }
    }
}
