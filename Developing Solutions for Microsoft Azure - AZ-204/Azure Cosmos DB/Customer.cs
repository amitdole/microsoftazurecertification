using Newtonsoft.Json;

namespace Azure_Cosmos_DB
{
    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string CustomerId { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        public List<Order> Orders { get; set; }
    }
}
