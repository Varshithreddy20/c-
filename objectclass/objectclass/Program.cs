using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace objectclass
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Object obj =new Person() { PersonNAME = "Varshith", email = "jfks@gmailcom" };
            Console.WriteLine(obj.Equals(new Person() { PersonNAME = "Varshith", email = "jfks@gmailcom" }));
            Console.WriteLine(obj.GetHashCode());
            Console.WriteLine(obj.ToString());
        }
        
    }
}
