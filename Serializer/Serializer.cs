using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Types;

namespace Serialization
{
    public static class Serializer
    {
        public static void Serialize(List<Product> products)
        {
            string json2 = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(@"Data\Products.json", json2);
        }

        public static void Serialize(List<User> users)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(@"Data\Users.json", json);
        }
    }
}
