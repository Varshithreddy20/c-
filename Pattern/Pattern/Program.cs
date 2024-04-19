using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace Pattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            ParentClass pc;

           pc = new ChildClass() { x = 10, y = 20 };

            if(pc is ChildClass cc)
            {
                Console.WriteLine(cc.x);
                Console.WriteLine(cc.y);
            }
            Console.WriteLine(pc.x);
           

        }
    }
}
