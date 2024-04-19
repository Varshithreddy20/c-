using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Manager : Employee
    {
        private string _departmentName;
        public Manager(int empID, string empName, string location, string departmentName): base(empID, empName, location)
        {
            _departmentName= departmentName;
        }
        //method overriding
        public string GetHealthInsuranceAmount()
        {
            return "Additional Health Insurance Amount is 200";
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
            return DepartmentName + " at " +base.Location;
        }
    }

    public class BranchManager : Employee
    {
        private string _departmentName;
        public BranchManager(int empID, string empName, string location, string departmentName) : base(empID, empName, location)
        {
            
        }
    }
    }
