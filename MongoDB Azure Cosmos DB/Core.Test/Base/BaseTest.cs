using RatomirBlog.Repository;
using RatomirBlog.Repository.Interface;

namespace Core.Test.Base
{
    public class BaseTest
    {
        public BlogClient Client { get; set; }

        public IBlogRepository _repository { get; set; }
        public IGridFSRepository _gridFSRepository { get; set; }

        public BaseTest()
        {
            Client = new BlogClient();
            _repository = new BlogRepository();
            _gridFSRepository = new GridFSRepository();
        }
    }
}
