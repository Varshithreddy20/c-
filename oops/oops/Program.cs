using System;
namespace oops
{
    class sample
    {
        static void Main()
        {
            Employee _Employee = new Employee();
            string OraanizationName = "ACCION TECH";
            const string TypeOfEmployee = "Contract Based";
            string DepartmentName = "Finance Department";
            Employee[] employees = new Employee[5];

            for (int i = 0; i < employees.Length; i++)
            {
                employees[i] = new Employee();

                Console.WriteLine("*****************" +OraanizationName+"*******************");
                Console.WriteLine($"\n <<<<<<<<<Employee {i + 1}>>>>>>>>>>>>>>>>");
                Console.Write("EmpId: ");
                employees[i].EmpID = int.Parse(Console.ReadLine());
                Console.Write("Employee Name: ");
                employees[i].EmpName = Console.ReadLine();
                Console.Write("Salary Per Hour: ");
                employees[i].SalaryPerHour = int.Parse(Console.ReadLine());
                Console.Write("No Of Working Hours: ");
                employees[i].NoOfWorkingHours = int.Parse(Console.ReadLine());
                Console.WriteLine("Net Salary: " + employees[i].SalaryPerHour * employees[i].NoOfWorkingHours);
                Console.WriteLine("Type of Employee: " + TypeOfEmployee);
                Console.WriteLine("Department Name: " + DepartmentName);
                Console.WriteLine("___________________________________________________________________");


                if (i < employees.Length - 1)
                {
                    Console.WriteLine("Do you want to continue to the next employee? (Yes/No)");
                    string answer = Console.ReadLine();
                    if (answer.Equals("No", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Press any key to exit.");
                        break;
                        Console.ReadKey();

                    }
                }

                
                Console.ReadKey();
            }
        }
    }
}

