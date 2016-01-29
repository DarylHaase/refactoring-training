using Newtonsoft.Json;
using System;

namespace Types
{
    [Serializable]
    public class Product
    {
        [JsonProperty("Name")]
        public string Name;
        [JsonProperty("Price")]
        public double Price;
        [JsonProperty("Quantity")]
        public int Qty;
    }
}
