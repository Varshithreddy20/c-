using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class Salesman : Employee
    {
        private string _region;

        public Salesman(int empID, string region, string empName, string location): base(empID, empName, location)
        {
            this._region = region;
        }
        public string Region
        {
            set { _region = value; }
            get { return _region; }
        }

        public string GetHealthInsuranceAmount()
        {
            
            return "Additional Health Insurance Amount is 2000";
        }
        public long SalesOfTheCurrentMonth()
        {
            return 30000;
        }
    }
}
