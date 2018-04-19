using RatomirBlog.Repository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RatomirBlog.Repository.Interface
{
    public interface IGridFSRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploadModel"></param>
        /// <returns>Unique <see cref="ObjectId"/></returns>
        Task<string> UploadAsync(UploadModel uploadModel);

        Task<byte[]> Download(string id);

        Task<FileInfoModel> FindAsync(string id, bool includeSource = false);

        Task DeleteOne(string id);

        Task RenameAsync(RenameModel renameModel);
    }
}
