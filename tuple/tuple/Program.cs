using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace tuple
{
    class sample
    {
        public Tuple<string,int> GetPersonDetails()
        {
            Tuple<string, int> perosn = new Tuple<string, int>("scott", 20);
            return perosn;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           sample s = new sample();
            Tuple<string,int>person =s.GetPersonDetails();

            Console.WriteLine(person.Item1);
            Console.WriteLine(person.Item2);
        }
    }
}
