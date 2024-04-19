using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace propertiesindexers
{
    public class Program
    {
        static void Main(string[] args)
        {
            Employee._CompanyName = "Test Company";

            Employee emp1 = new Employee(1, "Test", "BACK END");
            emp1.Tax= 50;
            emp1.NativePlace = "chennai";
            emp1.given = " welcome kit";

            Employee emp2 = new Employee(2, "varshith");
            emp2.Tax = 100;
            emp2.NativePlace = "hyd";

            Console.WriteLine(Employee._CompanyName);
            Console.WriteLine("\nFirst employee");
            Console.WriteLine(emp1.empID);
            Console.WriteLine(emp1.empName);
            Console.WriteLine(emp1.job);
            Console.WriteLine(emp1.Salary);
            Console.WriteLine(emp1.CalculateNetSalary());
            Console.WriteLine(emp1.NativePlace);
            Console.WriteLine(emp1.given);
            Console.WriteLine();

            Console.WriteLine("\nSecond employee");
            Console.WriteLine(emp2.empID);
            Console.WriteLine(emp2.empName);
            Console.WriteLine(emp2.job);
            Console.WriteLine(emp2.Salary);
            Console.WriteLine(emp2.CalculateNetSalary());
            Console.WriteLine(emp2.NativePlace);
            Console.WriteLine(emp2.given);
            Console.WriteLine();
        }
    }
}