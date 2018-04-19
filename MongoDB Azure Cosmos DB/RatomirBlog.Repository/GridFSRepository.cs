using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using RatomirBlog.Helper;
using RatomirBlog.Repository.Interface;
using RatomirBlog.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatomirBlog.Repository
{
    public class GridFSRepository : BlogClient, IGridFSRepository
    {
        public async Task<byte[]> Download(string id)
        {
            return await GridFSBucket.DownloadAsBytesAsync(id.ToObjectId());
        }

        public async Task<string> UploadAsync(UploadModel uploadModel)
        {
            GridFSUploadOptions fsOption = new GridFSUploadOptions()
            {
                Metadata = new BsonDocument(nameof(uploadModel.CreateUserId), uploadModel.CreateUserId)
            };

            ObjectId objectId = await GridFSBucket.UploadFromBytesAsync(uploadModel.FileName, uploadModel.Source, fsOption);
            return objectId.ToString();
        }

        public async Task<FileInfoModel> FindAsync(string id, bool includeSource = false)
        {
            FilterDefinition<GridFSFileInfo> filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Id, id.ToObjectId());
            IAsyncCursor<GridFSFileInfo> fileInfoCursor = await GridFSBucket.FindAsync(filter);
            fileInfoCursor.MoveNext();

            GridFSFileInfo fileInfo = fileInfoCursor.Current.Single();

            byte[] source = null;

            if (includeSource)
            {
                source = await Download(id);
            }

            return new FileInfoModel
            {
                DocumentId = fileInfo.Id.ToString(),
                FileName = fileInfo.Filename,
                UploadedDateTime = fileInfo.UploadDateTime,
                CreateUserId = fileInfo.Metadata[nameof(FileInfoModel.CreateUserId)].AsString,
                Source = source
            };
        }

        public async Task DeleteOne(string id) => await GridFSBucket.DeleteAsync(id.ToObjectId());

        public async Task RenameAsync(RenameModel renameModel) => await GridFSBucket.RenameAsync(renameModel.DocumentId.ToObjectId(), renameModel.NewFileName);
    }
}
