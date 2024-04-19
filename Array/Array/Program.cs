using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring and initializing arrays of different types
            int[] a = new int[5] { 10, 20, 30, 40, 50 };
            string[] b = new string[] { "one", "two", "three", "four", "five" };
            string[] c = new string[] { "rt", "jk", "ty", "es" };
            double[] d = new double[5] { 10, 20, 30, 40, 50 };

            //jagged arrays
            int[][] g = new int[5][];
            g[0] = new int[4] { 10, 20, 50, 80, };
            g[1] = new int[3] { 20, 50, 80, };
            g[2] = new int[2] { 80, 78 };
            g[3] = new int[1] { 90 };
            g[4] = new int[7] { 89, 96, 59, 49, 39, 37, 48, };

            for (int k = 0; k < 5; k++)
            {
                for (int j = 0; j < g[k].Length; j++)
                {
                    Console.Write(g[k][j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            // Printing elements of array b using a for loop
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[i]);
            }
            Console.WriteLine();

            // Printing elements of array b in reverse order using a for loop
            for (int i = b.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(b[i]);
            }
            Console.WriteLine();

            // Printing elements of array c using a foreach loop
            foreach (string i in c)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            // Printing elements of array a using a for loop
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
            Console.WriteLine();

            // Finding the index of element 30 in array a

            int n = System.Array.IndexOf(a, 30);
            Console.WriteLine("30 is found at index: " + n);


            Console.WriteLine();
            //binary search 

            int n2 = System.Array.BinarySearch(a, 20);
            Console.WriteLine("20 is found at index: " + n2);

            //araay clear 

            System.Array.Clear(a, 2, 3);
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //array resize INCRREASING

            System.Array.Resize(ref a, 7);
            foreach (var item in a)
            { Console.WriteLine(item); }
            Console.WriteLine();

            //array resize decreasing
            System.Array.Resize(ref a, 3);
            foreach (var item in a)
            { Console.WriteLine(item); }
            Console.WriteLine();

            //array sort ascending
            int[] f = new int[] { 56, 89, 32, 0, 76, 94, 20, 12 };
            System.Array.Sort(f);
            foreach (var item in f)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //array reverse
            System.Array.Reverse(f);
            foreach (var item in f)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();


            int[,] e = new int[4, 3]
            {
                {10,20,30 },
                {50,60,70 },
                {10,20,30 },
                {10,20,30 },
            };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(e[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();

            }
        }
    }
}
            

            
