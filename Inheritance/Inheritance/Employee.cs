using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{

    
    public class Employee
    {
        private int _empID;
        private string _empName;
        protected string _location;


        public Employee(int empID, string empName, string location)
        {
            this._empID = empID;
            this._empName = empName;
            this._location = location;

        }

        public  string GetHealthInsuranceAmount()
        {
            return "health " + 507770;
        }

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
    }
}
