using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class CD
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string NumberOfTracks { get; set; }

        public TimeSpan LoanPeriod => TimeSpan.FromDays(14);
        public string Borrower { get; set; }

        public void Borrow(string borrower)
        {
            if (string.IsNullOrEmpty(borrower))
                Borrower = borrower;
            else
                Console.WriteLine("CD alrady borrowed by : " + Borrower);
        }
        public void Return()
        {
            Borrower = null;
        }
        public void print()
        {
            Console.WriteLine($"Artist : {Artist} by {Title} NumberOfTracks : {NumberOfTracks}");
        }
    }

}

