using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collectionsassignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
               List<List<int>> collections = new List<List<int>>
        {
            new List<int> { 1, 5, 3, 9, 7 },
            new List<int> { 6, 8, 2, 4 },
            new List<int> { 11, 15, 13, 19, 17 }
        };

                List<int> largestNumbers = FindLargest(collections);

                Console.WriteLine("Largest Numbers in Collections:");
                foreach (int num in largestNumbers)
                {
                    Console.WriteLine(num);
                }
            }

        static List<int> FindLargest(List<List<int>> collections)
        {
            List<int> largestNumbers = new List<int>();

            foreach (List<int> collection in collections)
            {
                int largestInCollection = collection.Max();
                largestNumbers.Add(largestInCollection);
            }

            return largestNumbers;
        }
    }

}

