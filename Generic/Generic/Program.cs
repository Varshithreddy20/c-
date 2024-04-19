using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    public class Program
    {
        static void Main(string[] args)
        {
            //User<int, int> user1 = new User<int, int>();
            //User<bool, string> user2 = new User<bool, string>();


            //user1.RegistrationStatus = 1234;
            //user1.Age = 20;
            //user2.Age = "35 -40";
            //user2.RegistrationStatus = false;

            //Console.WriteLine(user1.RegistrationStatus);
            //Console.WriteLine(user1.Age);

            //Console.WriteLine(user2.RegistrationStatus);
            //Console.WriteLine(user2.Age);


            //MarksPrinter<GraduateStudent> marksPrinter = new MarksPrinter<GraduateStudent>();
            //marksPrinter.stu = new GraduateStudent() { Marks = 80 };
            //marksPrinter.PrintMarks();

            Sample sample = new Sample();
            employee emp = new employee() { salary = 1000 };
            student1 stu = new student1() { rank1 = 1 };



            sample.PrintData<student1>(stu);
            sample.PrintData<employee>(emp);

            
        }
    }
}
