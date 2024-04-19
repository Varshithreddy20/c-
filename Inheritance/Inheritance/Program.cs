using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
     class Program
    {
        static void Main(string[] args)
        {
            Employee emp1;
            

            //Console.WriteLine("Parent class : Employee");
            //Console.WriteLine(emp1.EmpID);  
            //Console.WriteLine(emp1.EmpName);
            //Console.WriteLine(emp1.Location);
            //Console.WriteLine();

            emp1=new Manager(2,"sai","hyd","devloping");
            Console.WriteLine(emp1.GetHealthInsuranceAmount());
            //Console.WriteLine("Parent class : Employee");
            //Console.WriteLine(man1.EmpID);
            //Console.WriteLine(man1.EmpName);
            //Console.WriteLine(man1.Location);
            //Console.WriteLine(man1.DepartmentName);
            //Console.WriteLine(man1.GetTotalSalesOfTheYear());
            //Console.WriteLine(man1.GetFullDepartmentName());
            //Console.WriteLine(man1.GetHealthInsuranceAmount());
            Console.WriteLine();


            emp1 = new Salesman(3,"ram","hyd","east");
            Console.WriteLine(emp1.GetHealthInsuranceAmount());
            Console.WriteLine();

            //Console.WriteLine("Parent class : Employee");
            //Console.WriteLine(sale1.EmpID);
            //Console.WriteLine(sale1.EmpName);
            //Console.WriteLine(sale1.Location);
            //Console.WriteLine(sale1.Region);
            //Console.WriteLine(sale1.SalesOfTheCurrentMonth());
            //Console.WriteLine();


            Console.ReadKey();
        }
    }
}
