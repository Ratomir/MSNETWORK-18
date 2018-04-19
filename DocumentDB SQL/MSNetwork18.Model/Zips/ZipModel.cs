using Microsoft.Azure.Documents.Spatial;
using Newtonsoft.Json;
using System;

namespace MSNetwork18.Model.Zips
{
    public partial class ZipModel
    {
        [JsonProperty("createdDateTime")]
        public DateTime? CreatedDateTime { get; set; }

        [JsonProperty("updatedDateTime")]
        public DateTime? UpdatedDateTime { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("loc")]
        public Point Loc { get; set; }

        [JsonProperty("pop")]
        public long Pop { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
