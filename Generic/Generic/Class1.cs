using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic
{
    //public class User<T1, T2>
    //{
    //    public T1 RegistrationStatus;

    //    public T2 Age;
    //}
    //public abstract class Student
    //{
    //    public abstract int Marks { get; set; }
    //}

    //public class GraduateStudent : Student
    //{
    //    public override int Marks { get; set; }

    //}
    //public class PostGraduateStudent : Student
    //{
    //    public override int Marks { get; set; }
    //}
    //public class MarksPrinter<M>where M : Student
    //{
    //    public M stu;
    //    public void PrintMarks()
    //    {
    //        Student temp = (Student)stu;
    //        Console.WriteLine(temp.Marks);
    //    }

        public class employee
        {
            public int salary;
        }
        
        public class student1
        {
            public int rank1;
        }
        public class Sample
        {
            public void PrintData<V>(V obj)
            {
                if (obj.GetType() == typeof(student1))
                {
                    student1 temp = obj as student1;
                    Console.WriteLine(temp.rank1);
                }
                else if (obj.GetType() == typeof(employee))
                {
                    employee temp = obj as employee;
                    Console.WriteLine(temp.salary);
                }
                
            }
        }

        
    }

