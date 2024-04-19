using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DVD
    {
        public string Director { get; set; }
        public string Title { get; set; }
        public string LengthInMinutes { get; set; }

        public TimeSpan LoanPeriod => TimeSpan.FromDays(07);
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            if (string.IsNullOrEmpty(borrower))
                Borrower = borrower;
            else
                Console.WriteLine("DVD alrady borrowed by : " + Borrower);
        }
        public void Return()
        {
            Borrower = null;
        }
        public void print()
        {
            Console.WriteLine($"Director: {Director} by {Title} and Length (mins) : {LengthInMinutes}");
        }
    }
}
