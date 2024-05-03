using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace binaryserialzation
{
    [Serializable]
    public class country
    {
        public short CountryID { get; set; }
        public string CountryName { get; set; }
        public long Population { get; set; }
        public string Region { get; set; }
        internal class Program
        {
            static void Main(string[] args)
            {
                country country = new country() { CountryID = 1, CountryName = "INDIA", Population = 345678765, Region = "Asia" };
                string filepath = @"C:\Users\al6113\Desktop\sample\binaryserialzation\country.txt";
                
                FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, country);
                fileStream.Close();
                Console.WriteLine("serialzed");


                FileStream fileStream2 = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                country country_from_file= (country)bf.Deserialize(fileStream2);
                Console.WriteLine("\nafter deserialzed");
                Console.WriteLine("country id:" + country_from_file.CountryID);
                Console.WriteLine("country name:" + country_from_file.CountryName);
                Console.WriteLine("population:" + country_from_file.Population);
                Console.WriteLine("region:" + country_from_file.Region);
            }
        }
    }
}
