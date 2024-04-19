using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using class1;


namespace delegates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sample s = new Sample();

            mydelegateType mydelegate;
            mydelegate =new mydelegateType(s.Add);

            Console.Write(mydelegate.Invoke(30,40));
        }
    }
}
