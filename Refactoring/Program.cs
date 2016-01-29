using System.Collections.Generic;
using Serialization;
using Types;

namespace Refactoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<User> users = new List<User>();
            Deserializer.Deserialize(ref users);
            
            List<Product> products = new List<Product>();
            Deserializer.Deserialize(ref products);

            Tusc.Start(users, products);
        }
    }
}
