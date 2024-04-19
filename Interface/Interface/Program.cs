using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    class Program
    {
            static void Main(string[] args)
            {

            IEmployee emp1;
               

                emp1 = new Manager(2, "sai", "hyd", "devloping");
                Console.WriteLine("Manager : Employee");
            Console.WriteLine(emp1.GetHealthInsuranceAmount());
            emp1.dateOfBirth = Convert.ToDateTime("1990-07-09");
            Console.WriteLine(emp1.GetAge());
            //Console.WriteLine(man1.EmpID);
            //Console.WriteLine(man1.EmpName);
            //Console.WriteLine(man1.Location);
            //Console.WriteLine(man1.DepartmentName);
            //Console.WriteLine(man1.GetTotalSalesOfTheYear());
            //Console.WriteLine(man1.GetFullDepartmentName());
            //Console.WriteLine(man1.GetHealthInsuranceAmount());

            Console.WriteLine();

            IPerson person;
            person = new Manager(2, "vivivi", "banglore", "testing");
            Console.WriteLine("Manager : Employee");
            person.dateOfBirth = Convert.ToDateTime("2002-03-20");
            Console.WriteLine(person.GetAge());
            //person = new Salesman(3, "ram", "hyd", "east");
            // Console.WriteLine("Salesman: Employee");
            //Console.WriteLine(sale1.EmpID);
            //Console.WriteLine(sale1.EmpName);
            //Console.WriteLine(sale1.Location);
            //Console.WriteLine(sale1.Region);
            //Console.WriteLine(sale1.SalesOfTheCurrentMonth());
            Console.WriteLine();


                Console.ReadKey();
            }
        }
    }


