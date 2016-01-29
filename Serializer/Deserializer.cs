using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Types;

namespace Serialization
{
    public static class Deserializer
    {
        public static void Deserialize(ref List<Product> products)
        {
            products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data\Products.json"));
        }

        public static void Deserialize(ref List<User> users)
        {
            users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data\Users.json"));
        }
    }
}
