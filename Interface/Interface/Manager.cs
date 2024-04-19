using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class Manager : IPerson, IEmployee
    {


        private int _empID;
        private string _empName;
        private string _location;
        private string _departmentName;
        private DateTime _dateOfBirth;

       public int EmpID
        {
            set
            {
                _empID = value;
            }
            get
            {
                return _empID;
            }
        }
        public string EmpName
        {
            set
            {
                _empName = value;
            }
            get
            {
                return _empName;
            }
        }
           
                
         public string Location
        {
            set
            {
                _location = value;
            }
            get
            {
                return _location;
            }
        }
        public DateTime dateOfBirth
        {
            set
            {
                _dateOfBirth = value;
            }
            get
            {
                return _dateOfBirth;
            }
        }


        public Manager(int empID, string empName, string location, string departmentName)
        {
            _departmentName = departmentName;
            _empID = empID;
            _empName = empName;
            _location = location;
        }
        //method overriding
        public  string GetHealthInsuranceAmount()
        {
            return "Additional Health Insurance Amount is 2000";
        }

        public string DepartmentName
        {
            set
            {
                _departmentName = value;

            }
            get { return _departmentName; }
        }

        public long GetTotalSalesOfTheYear()
        {
            return 10000;
        }


        public String GetFullDepartmentName()
        {
            return DepartmentName + " at " + _location;
        }
         int IPerson.GetAge()
        {
            int a = Convert.ToInt32((DateTime.Now - dateOfBirth).TotalDays / 365);
            return a;
        }
        int IEmployee.GetAge()
        {
           
            return 20;
        }
    }

   
}
