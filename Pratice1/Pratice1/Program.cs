using Practice1;
using System.Security.Claims;
using System;

namespace Practice1
{
    public class Employee
    {
        public string Firstname ="ln";
        public string Lastname = "fn";

        public void PrintFullName()
        {
            Console.WriteLine(Firstname + " " + Lastname);
        }
    }

    public class ParttimeEmployee : Employee
    {
        public new void PrintFullName()
        {
            Console.WriteLine(Firstname + " " + Lastname + " con");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            ParttimeEmployee pte = new ParttimeEmployee();
            pte.PrintFullName();
           

            Console.ReadLine();
        }
    }
}
