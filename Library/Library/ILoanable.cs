using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface ILoanable
    {
        TimeSpan LoanPeriod {  get; }
        string Borrower { get; set; }

        void Borrow(string borrower);
        void Return();
    }
}
