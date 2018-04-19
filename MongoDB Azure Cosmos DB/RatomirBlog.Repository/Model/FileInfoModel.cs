using System;
using System.Collections.Generic;
using System.Text;

namespace RatomirBlog.Repository.Model
{
    public class FileInfoModel
    {
        public string DocumentId { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public string FileName { get; set; }
        public string CreateUserId { get; set; }
        public byte[] Source { get; set; } = null;
    }
}
