using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using RatomirBlog.Repository;

namespace RatomirBlog.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        public BlogClient BlogClient { get; set; }
        public StorageCredentials StorageCredentials { get; set; }
        public CloudStorageAccount StorageAccount { get; set; }

        public BaseController(BlogClient blogClient, IConfiguration configuration)
        {
            BlogClient = blogClient;

            IConfigurationSection section = configuration.GetSection("BlobStorage");

            StorageCredentials = new StorageCredentials(section.GetValue<string>("accountname"), section.GetValue<string>("key"));
            StorageAccount = new CloudStorageAccount(StorageCredentials, true);
        }
    }
}
