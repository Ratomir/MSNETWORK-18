using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RatomirBlog.Repository.Model
{
    public class UploadModel
    {
        [Required(ErrorMessage = "File name is required for document")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Source is required for document")]
        public byte[] Source { get; set; }

        [Required(ErrorMessage = "User identification can't be empty")]
        public string CreateUserId { get; set; }
    }
}
