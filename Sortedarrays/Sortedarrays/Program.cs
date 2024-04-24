using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortedarrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedList<int, string> employees = new SortedList<int, string>()
            {
                {101,"Varshith" },
                {102,"krishna" },
                {103,"Vars" },
                {104,"ram" },
                {105,"sai" }
            };
            employees.Add(107, "benu");

            employees.Remove(102);
            foreach (KeyValuePair<int, string> kvp in employees)
            {
                Console.WriteLine(kvp.Key + " , " +kvp.Value);
            }
        }
    }
}
