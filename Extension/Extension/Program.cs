using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtensionNamespace;
using ClassLibrary1;

 namespace Extension
{
   public class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product() { ProductCost = 1000, DiscountPercentage = 20 };
            Console.WriteLine(p.GetDiscount());
            
            
          
   
        }
    }
}
