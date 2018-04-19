using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MSNetwork18.Model.Email
{
    public class EmailModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("recipients")]
        public List<string> Recipients { get; set; }

        [JsonProperty("cc")]
        public List<string> Cc { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("mid")]
        public string Mid { get; set; }

        [JsonProperty("fpath")]
        public string Fpath { get; set; }

        [JsonProperty("bcc")]
        public List<string> Bcc { get; set; }

        [JsonProperty("to")]
        public List<string> To { get; set; }

        [JsonProperty("replyto")]
        public object Replyto { get; set; }

        [JsonProperty("ctype")]
        public string Ctype { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}
