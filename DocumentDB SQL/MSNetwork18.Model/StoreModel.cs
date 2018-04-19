using Newtonsoft.Json;

namespace MSNetwork18.Model
{
    public class StoreModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address")]
        public AddressModel Address { get; set; }
    }
}
