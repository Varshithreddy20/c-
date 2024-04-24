using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collections_library
{
    

// Part 1: Create Interfaces
public interface ILoanable
        {
            DateTime BorrowDate { get; set; }
            DateTime DueDate { get; }
            string Borrower { get; set; }
            bool IsAvailable { get; }
            void Borrow(string borrower);
            void Return();
        }

        public interface IPrintable
        {
            void Print();
        }

        // Part 2: Create Classes
        public class Book : ILoanable, IPrintable
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public string ISBN { get; set; }
            public DateTime BorrowDate { get; set; }
            public string Borrower { get; set; }

            public DateTime DueDate => BorrowDate.AddDays(21);

            public bool IsAvailable => string.IsNullOrEmpty(Borrower);

            public void Borrow(string borrower)
            {
                if (IsAvailable)
                {
                    Borrower = borrower;
                    BorrowDate = DateTime.Now;
                    Console.WriteLine($"Book '{Title}' borrowed by {borrower}.");
                }
                else
                {
                    Console.WriteLine($"Book '{Title}' is already borrowed.");
                }
            }

            public void Return()
            {
                if (!IsAvailable)
                {
                    Borrower = null;
                    Console.WriteLine($"Book '{Title}' returned.");
                }
                else
                {
                    Console.WriteLine($"Book '{Title}' is already available.");
                }
            }

            public void Print()
            {
                Console.WriteLine($"Book Title: {Title}");
                Console.WriteLine($"Author: {Author}");
                Console.WriteLine($"ISBN: {ISBN}");
                Console.WriteLine($"Available: {IsAvailable}");
                Console.WriteLine();
            }
        }

        public class DVD : ILoanable, IPrintable
        {
            public string Director { get; set; }
            public string Title { get; set; }
            public int LengthInMinutes { get; set; }
            public DateTime BorrowDate { get; set; }
            public string Borrower { get; set; }

            public DateTime DueDate => BorrowDate.AddDays(7);

            public bool IsAvailable => string.IsNullOrEmpty(Borrower);

            public void Borrow(string borrower)
            {
                if (IsAvailable)
                {
                    Borrower = borrower;
                    BorrowDate = DateTime.Now;
                    Console.WriteLine($"DVD '{Title}' borrowed by {borrower}.");
                }
                else
                {
                    Console.WriteLine($"DVD '{Title}' is already borrowed.");
                }
            }

            public void Return()
            {
                if (!IsAvailable)
                {
                    Borrower = null;
                    Console.WriteLine($"DVD '{Title}' returned.");
                }
                else
                {
                    Console.WriteLine($"DVD '{Title}' is already available.");
                }
            }

            public void Print()
            {
                Console.WriteLine($"DVD Title: {Title}");
                Console.WriteLine($"Director: {Director}");
                Console.WriteLine($"Length (Minutes): {LengthInMinutes}");
                Console.WriteLine($"Available: {IsAvailable}");
                Console.WriteLine();
            }
        }

        public class CD : ILoanable, IPrintable
        {
            public string Artist { get; set; }
            public string Title { get; set; }
            public int NumberOfTracks { get; set; }
            public DateTime BorrowDate { get; set; }
            public string Borrower { get; set; }

            public DateTime DueDate => BorrowDate.AddDays(14);

            public bool IsAvailable => string.IsNullOrEmpty(Borrower);

            public void Borrow(string borrower)
            {
                if (IsAvailable)
                {
                    Borrower = borrower;
                    BorrowDate = DateTime.Now;
                    Console.WriteLine($"CD '{Title}' borrowed by {borrower}.");
                }
                else
                {
                    Console.WriteLine($"CD '{Title}' is already borrowed.");
                }
            }

            public void Return()
            {
                if (!IsAvailable)
                {
                    Borrower = null;
                    Console.WriteLine($"CD '{Title}' returned.");
                }
                else
                {
                    Console.WriteLine($"CD '{Title}' is already available.");
                }
            }

            public void Print()
            {
                Console.WriteLine($"CD Title: {Title}");
                Console.WriteLine($"Artist: {Artist}");
                Console.WriteLine($"Number of Tracks: {NumberOfTracks}");
                Console.WriteLine($"Available: {IsAvailable}");
                Console.WriteLine();
            }
        }

        // Part 3: Create a Library Class
        public class Library
        {
            public List<ILoanable> Items { get; set; }

            public Library()
            {
                Items = new List<ILoanable>();
            }

            public void AddItem(ILoanable item)
            {
                Items.Add(item);
            }

            public void RemoveItem(ILoanable item)
            {
                Items.Remove(item);
            }

            public void PrintLibrary()
            {
                foreach (var item in Items)
                {
                    IPrintable printableItem = item as IPrintable;
                    printableItem.Print();
                }
            }
        }

        // Part 4: Test Your Implementation
        class Program
        {
            static void Main(string[] args)
            {
                Library library = new Library();

                // Adding items to the library
                Book book1 = new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565" };
                DVD dvd1 = new DVD { Title = "Inception", Director = "Christopher Nolan", LengthInMinutes = 148 };
                CD cd1 = new CD { Title = "Abbey Road", Artist = "The Beatles", NumberOfTracks = 17 };

                library.AddItem(book1);
                library.AddItem(dvd1);
                library.AddItem(cd1);

                // Borrowing items
                book1.Borrow("John Doe");
                dvd1.Borrow("Jane Smith");

                // Printing library
                library.PrintLibrary();

                // Returning items
                book1.Return();
                dvd1.Return();

                // Printing library again
                library.PrintLibrary();
            }
        }

    }

