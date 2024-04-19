using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book : ILoanable, IPrintable
    {
        public string Author {  get; set; }
         public string Title {  get; set; }
         public string ISBN { get; set; }

        public TimeSpan LoanPeriod => TimeSpan.FromDays(21);
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            if (string.IsNullOrEmpty(borrower))
                Borrower = borrower;
            else
                Console.WriteLine("Book alrady borrowed by : "+ Borrower);
        }
        public void Return()
        {
            Borrower = null;
        }
        public void print()
        {
            Console.WriteLine($"book: {Title} by {Author} (ISBN : {ISBN})");
        }

        void IPrintable.Print()
        {
            throw new NotImplementedException();
        }
    }
}
