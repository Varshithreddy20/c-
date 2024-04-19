using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strutures
{
     class Program
    {
        static void Main(string[] args)
        {
            Category category= new Category(20,"general");

            //category.CategoryID = 20;
            //category.CategoryName = "General";

            Console.WriteLine(category.CategoryID);
            Console.WriteLine(category.CategoryName);
            Console.WriteLine(category.GetCategoryNameLength());

        }
    }
}
