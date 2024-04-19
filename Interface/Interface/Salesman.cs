using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public class Salesman : IPerson,IEmployee
    {
        private string _region;
        private int _empID;
        private string _empName;
        private string _location;
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

        public Salesman(int empID, string region, string empName, string location)
        {
            this._empID = empID;
            this._empName = empName;
            this._location = location;
            this._region = region;
        }
        public string Region
        {
            set { _region = value; }
            get { return _region; }
        }


        public long SalesOfTheCurrentMonth()
        {
            return 30000;
        }

        string IEmployee.GetHealthInsuranceAmount()
        {
            throw new NotImplementedException();
        }
        public int GetAge()
        {
            int a = Convert.ToInt32((DateTime.Now - dateOfBirth).TotalDays / 365);
            return a;
        }
    }
}
