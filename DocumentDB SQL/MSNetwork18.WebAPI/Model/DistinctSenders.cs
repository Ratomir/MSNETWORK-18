using Newtonsoft.Json;

namespace MSNetwork18.WebAPI.Model
{
    public class DistinctSenders
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }
    }
}
