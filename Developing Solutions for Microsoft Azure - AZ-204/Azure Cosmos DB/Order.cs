using Newtonsoft.Json;

namespace Azure_Cosmos_DB
{
    public class Order
    {
        public string OrderId { get; set; }

        public string Category { get; set; }

        public int Quantity { get; set; }
    }
}
