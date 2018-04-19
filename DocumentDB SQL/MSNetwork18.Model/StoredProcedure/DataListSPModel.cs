using Newtonsoft.Json;
using System.Collections.Generic;

namespace MSNetwork18.Model.StoredProcedure
{
    public class DataListSPModel<T>
    {
        [JsonProperty("length")]
        public int Length { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
