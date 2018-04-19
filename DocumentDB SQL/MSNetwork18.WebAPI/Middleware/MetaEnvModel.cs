using Newtonsoft.Json;

namespace MSNetwork18.WebAPI.Middleware
{
    [JsonObject("_meta_info")]
    public class MetaEnvModel
    {
        [JsonProperty("elapsed_miliseconds")]
        public long ElapsedMiliseconds { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }
    }
}
