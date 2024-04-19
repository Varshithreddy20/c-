using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> test = new List<int>() { 10,20,30};
            test.Add(40);
            List<int> testing = new List<int>() { 50, 60, 80, 90 };
            test.AddRange(testing);
            test.Insert(2, 6);
            List<int> testing2 = new List<int>() { 1,2,3,4,5,6,7 };
            foreach (int item in test) {
                Console.WriteLine(item);
            }

            //using for loop 
            List<string> listing = new List<string>() { "string","godof eroor" };
            foreach (string item in listing)
            {
                Console.Write(item);
            }
            Console.WriteLine();
            //using binary search 
            List<int> bsearch = new List<int>() { 1, 2, 3, 4, 5, 6, 7,8,9,10 };
            int n4 = bsearch.BinarySearch(6);
            Console.WriteLine( "6 binary search" + n4);
            bool a = bsearch.Contains(11);
            bool b = bsearch.Contains(1);
            Console.WriteLine("11 is found " + a);
            Console.WriteLine("1 is foun " + b);

            List<string> covertingtoarray= new List<string>() {"varshith", "sai","ram","reddy" };
            string[] covertingtoarrayArray = covertingtoarray.ToArray();
            foreach(string item in covertingtoarrayArray) { Console.WriteLine(item); }
        }
        }
    }

