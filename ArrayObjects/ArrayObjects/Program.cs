using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using class1;
namespace ArrayObjects
{
    class Program
    {
        static void Main(string[] args)
        {


            Employee[] employees = new Employee[]
            {
                new Employee() { EmpID=1,Name="Varshith"},
                new Employee() { EmpID = 2, Name = "kiran" },
                new Employee() { EmpID = 3, Name = "abdul" }
            };

            foreach (Employee item in employees)
            {
                Console.WriteLine(item.EmpID+ "," +item.Name);
            }
        }
    }
        
}

