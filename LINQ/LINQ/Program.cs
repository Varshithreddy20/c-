using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
     public class employee
    {
        public int EmpId{ get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public string City { get; set; }
        public double Salary { get; set; }   

    }
   
        public class Program
        {
            static void Main(string[] args)
            {
                List<employee> employees = new List<employee>()
                {
                    new employee { EmpId = 1, EmpName = "John", Job = "Software Engineer", City = "New York", Salary = 90000 },
                    new employee { EmpId = 2, EmpName = "Alice", Job = "Web Developer", City = "Los Angeles", Salary = 85000 },
                    new employee { EmpId = 3, EmpName = "Michael", Job = "Data Scientist", City = "Chicago", Salary = 95000 },
                    new employee { EmpId = 4, EmpName = "Emily", Job = "UI/UX Designer", City = "San Francisco", Salary = 80000 },
                    new employee { EmpId = 5, EmpName = "David", Job = "Project Manager", City = "New York", Salary = 100000 }

                };
            foreach (employee emp in employees)
            {
                Console.WriteLine(emp.EmpId + " , " + emp.EmpName + " , " + emp.Job + " , " + emp.City + " , " + emp.Salary);
            }
            Console.WriteLine();
            Console.WriteLine("Order by Alphabetical Order");
            Console.WriteLine();
            var re = employees.OrderBy(emp => emp.EmpName);
            foreach (employee emp in re)
            {
                Console.WriteLine(emp.EmpId + " , " + emp.EmpName + " , " + emp.Job + " , " + emp.City + " , " + emp.Salary);
            }
            Console.WriteLine();
            Console.WriteLine("Employee who live only in New York");
            Console.WriteLine();
            var result=employees.Where(emp => emp.City == "New York");
                foreach(employee emp in result)
                {
                    Console.WriteLine(emp.EmpId+" , " +emp.EmpName+" , "+emp.Job+" , "+emp.City+" , "+emp.Salary);
                }
            Console.WriteLine();
            Console.WriteLine("Employee with more than 95k");
            Console.WriteLine();

            var res = employees.Where(emp => emp.Salary >=95000);
            foreach (employee emp in res)
            {
                Console.WriteLine(emp.EmpId + " , " + emp.EmpName + " , " + emp.Job + " , " + emp.City + " , " + emp.Salary);
            }
        }
        }
    }

