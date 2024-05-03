using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Format_Exception
{
    public class BankAccount
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public double Balance { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {


                BankAccount bankAccount = new BankAccount();
                Console.WriteLine("Enter Account Holder name:");
                bankAccount.Name = Console.ReadLine();
                Console.WriteLine("Enter the Accont Number:");
                bankAccount.Number = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the current balance:");
                bankAccount.Balance = double.Parse(Console.ReadLine());

                Console.WriteLine("\nNew bank account details");
                Console.WriteLine("Account Number:" + bankAccount.Balance);
                Console.WriteLine("Account Name:" + bankAccount.Name);
                Console.WriteLine("Account Balance:" + bankAccount.Balance);
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
