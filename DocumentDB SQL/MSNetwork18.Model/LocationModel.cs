using Newtonsoft.Json;

namespace MSNetwork18.Model
{
    public class LocationModel
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateProvinceName")]
        public string StateProvinceName { get; set; }
    }
}
