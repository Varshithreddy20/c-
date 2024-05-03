using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace xml_serialzation
{
    [Serializable]

    public class Country
    {
        public int CountryID {  get; set; } 
        public string Name { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {   
            Country country = new Country();
            XmlSerializer xmlSerialzer = new XmlSerializer(typeof(Country));
            string filepath = @"C:\Users\al6113\Desktop\sample\xml serialzation\country.txt";
            FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            xmlSerialzer.Serialize(fileStream, country);
            fileStream.Close();
            Console.WriteLine("country.xml created");

        }
    }
}
