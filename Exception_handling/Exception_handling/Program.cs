using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception_handling
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int a, b;
                Console.WriteLine("Enter first nnumber:");
                string input1 = Console.ReadLine();
                a = int.Parse(input1);
                Console.WriteLine("Enter second nnumber:");
                string input2 = Console.ReadLine();
                b = int.Parse(input2);

                int c = a / b;
                Console.WriteLine("division" + c);
            }catch(DivideByZeroException ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}