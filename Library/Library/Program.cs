using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book
            {
                Author = " Varshith",
                Title = " Accion ",
                ISBN= " 9890-95849"

            };
            DVD dVD = new DVD()
            {
                Director = " kiran ",
                Title = " Oscar",
                LengthInMinutes = " 160 "
            };
            CD cD = new CD()
            {
                Artist = " abdul ",
                Title=" COOL DOWN",
                NumberOfTracks =" 90"
            };

            book.print();
            dVD.print();
            cD.print();

            book.Borrow(" saidulu");
            dVD.Borrow(" shahshank");
            cD.Borrow(" gunm");

            Console.WriteLine(" After Borrowing");
            book.print();
            dVD.print();
            cD.print();

            dVD.Return();

            Console.WriteLine(" after returning the dvd");
            dVD.print();

        }
    }
}
