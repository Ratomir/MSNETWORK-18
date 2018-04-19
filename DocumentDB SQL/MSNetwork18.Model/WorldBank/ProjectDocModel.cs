using Newtonsoft.Json;

namespace MSNetwork18.Model.WorldBank
{
    public class ProjectDocModel
    {
        [JsonProperty("DocTypeDesc")]
        public string DocTypeDesc { get; set; }

        [JsonProperty("DocType")]
        public string DocType { get; set; }

        [JsonProperty("EntityID")]
        public string EntityId { get; set; }

        [JsonProperty("DocURL")]
        public string DocUrl { get; set; }

        [JsonProperty("DocDate")]
        public string DocDate { get; set; }
    }
}
