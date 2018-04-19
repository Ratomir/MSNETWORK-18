using Newtonsoft.Json;

namespace MSNetwork18.DAL.ExceptionCore
{
    public class ExceptionModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("line")]
        public int Line { get; set; }
    }
}
