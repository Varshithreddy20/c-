using System;

namespace propertiesindexers
{
    public class Employee
    {
        private int _empID;
        private string _empName;
        private string _job;
        private double _salary;
        private double _tax;

        private static string companyName;

        public int empID
        {
            set { _empID = value; }
            get { return _empID; }
        }

        public string empName
        {
            set { _empName = value; }
            get { return _empName; }
        }

        public string job
        {
            set { _job = value; }
            get { return _job; }
        }

        public static string _CompanyName
        {
            set { companyName = value; }
            get { return companyName; }
        }

        public Employee(int empID, string empName, string job)
        {
            this.empID = empID;
            this.empName = empName;
            this._job = job;
            this._salary = 1000;
        }

        public Employee(int empID, string empName)
        {
            this.empID = empID;
            this.empName = empName;
            this._salary = 3000;
        }

        public double Salary
        {
            get
            {
                return _salary;
            }
        }

        public double Tax
        {
            set
            {
                _tax = value;
            }
        }

        public double CalculateNetSalary()
        {
            double t = Salary-_tax;
            return t;
        }

        public string NativePlace { get; set; }
        public string given { get; set; } = " laptop";
    }
}