using Newtonsoft.Json;

namespace MSNetwork18.Model.WorldBank
{
    public class MajorSectorPercentModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Percent")]
        public long Percent { get; set; }
    }
}
