using System.Runtime.Serialization;

namespace DemoApplication2.Models
{
    [DataContract]
    [Serializable]
    public class Product
    {
        [DataMember (Name="Id")]
        public int ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }
    }
}
