using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PSAZDemo1.NetCoreWebApplication.Model
{
    public class Location
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        public string Address { get; set; }

        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
    }
}
