using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{


    public interface IEmployee : IPerson
    {
        string GetHealthInsuranceAmount();
        int GetAge();
        int EmpID
        { set; get; }
        string EmpName
        { set; get; }
        string Location
        { set; get; }
    }
}
