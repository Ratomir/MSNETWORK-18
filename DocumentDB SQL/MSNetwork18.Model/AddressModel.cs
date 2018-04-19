using Newtonsoft.Json;

namespace MSNetwork18.Model
{
    public class AddressModel
    {
        [JsonProperty("addressType")]
        public string AddressType { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("location")]
        public LocationModel Location { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("countryRegionName")]
        public string CountryRegionName { get; set; }
    }
}
