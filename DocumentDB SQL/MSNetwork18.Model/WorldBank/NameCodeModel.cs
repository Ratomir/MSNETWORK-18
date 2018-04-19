using Newtonsoft.Json;

namespace MSNetwork18.Model.WorldBank
{
    public class NameCodeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
