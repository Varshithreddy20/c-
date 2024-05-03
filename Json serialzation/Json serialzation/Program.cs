using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Json_serialzation
{
    [Serializable]
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int Age { get; set; }
        internal class Program
        {
            static void Main(string[] args)
            {
                Customer customer = new Customer() { CustomerID = 1, CustomerName = "Varshith", Age = 22 };
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                string filepath = @"C:\Users\al6113\Desktop\sample\Json serialzation\customer.txt";
                StreamWriter sw = new StreamWriter(filepath);

                string json = javaScriptSerializer.Serialize(customer);
                sw.WriteLine(json);
                sw.Close();
                Console.WriteLine("Serialzied");

                //deserialze
                StreamReader sr = new StreamReader(filepath);
                Customer customer_from_file= javaScriptSerializer.Deserialize(sr.ReadToEnd(), typeof(Customer)) as Customer;
                Console.WriteLine(customer_from_file.CustomerName);
                Console.WriteLine(customer_from_file.CustomerID);
                Console.WriteLine(customer_from_file.Age);
            }
        }
    }
}
