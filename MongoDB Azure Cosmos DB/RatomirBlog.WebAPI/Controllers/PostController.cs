using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Driver;
using RatomirBlog.Model;
using RatomirBlog.Repository;
using RatomirBlog.Repository.Interface;

namespace RatomirBlog.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : BaseController
    {
        public IBlogRepository _blogRepository { get; set; }

        public PostController(IBlogRepository blogRepository, BlogClient blogClient, IConfiguration configruation)
            :base(blogClient, configruation)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            PostModel postModel = await _blogRepository.ReadOneByIdAsync<PostModel>(BlogClient, id);
            return Ok(postModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            Uri blobUri = new Uri("https://" + AccountName + ".blob.core.windows.net/html/html.html");
            CloudBlockBlob blob = new CloudBlockBlob(blobUri, StorageCredentials);

            MemoryStream mem = new MemoryStream();
            await blob.DownloadToStreamAsync(mem);

            UserModel userModel = await _blogRepository.ReadOneAsync<UserModel>(BlogClient, t => t.Username == "ratomirx" && t.Email == "ratomir@live.com");

            PostModel postModel = new PostModel()
            {
                Content = Encoding.UTF8.GetString(mem.ToArray()),
                CreatedDate = DateTime.Now,
                CreatedUser = userModel.DocumentId.ToString()
            };

            string id = await _blogRepository.InsertOneGetIdAsync(BlogClient, postModel);

            return Ok(id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]ReplayModel replayModel)
        {
            try
            {
                UpdateDefinition<PostModel> updateDocument = Builders<PostModel>.Update.Push(t => t.Replays, replayModel);

                bool updateResult = await _blogRepository.UpdateOneAsync(BlogClient, id, updateDocument);

                if (updateResult)
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
