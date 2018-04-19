using Core.Test.Base;
using RatomirBlog.Repository.Model;
using System.IO;
using Xunit;

namespace Core.Test.GridFSTest
{
    public class GridFSRepositoryShould : BaseTest
    {
        public GridFSRepositoryShould() : base()
        {
                
        }

        [Fact]
        public async void UploadTest()
        {
            byte[] source = new byte[0];

            using (FileStream file = File.OpenRead(@"C:\Users\ratomir.vukadin\Downloads\bg.jpg"))
            {
                source = new byte[file.Length];
                await file.ReadAsync(source, 0, (int)file.Length);
            }

            UploadModel uploadModel = new UploadModel()
            {
                CreateUserId = "32322323234",
                FileName = "photoPicture",
                Source = source
            };

            string newObjectFSId = await _gridFSRepository.UploadAsync(uploadModel);
            AssertExtension.Success();
        }
    }
}
