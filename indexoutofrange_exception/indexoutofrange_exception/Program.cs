using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexOutOfRangeExceptio
{
    public class BankAccount
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public double Balance { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                BankAccount[] bankAccount = new BankAccount[]
                {new BankAccount(){Number = 1, Name ="varshith", Balance = 78900},
                new BankAccount(){Number = 2,Name="sai",Balance=89009}

                };
                for (int i = 0; i < bankAccount.Length; i++)
                {
                    Console.WriteLine($"{i + 1}.{bankAccount[i].Name},{bankAccount[i].Number},{bankAccount[i].Balance}");
                }
                int serialNumber;
                Console.Write("Enter serial number to print:");
                serialNumber = int.Parse(Console.ReadLine());
                serialNumber--;
                //if (serialNumber < 0 || serialNumber >= bankAccount.Length)
                //{
                //    Console.WriteLine("Invalid serial Number");
                //}
                //else
                {
                    BankAccount selectedBankccount = bankAccount[serialNumber];
                    Console.WriteLine("Selected Bank ccount Details");
                    Console.WriteLine("Account Number:" + selectedBankccount.Number);
                    Console.WriteLine("Name:" + selectedBankccount.Name);
                    Console.WriteLine("Current Balance:" + selectedBankccount.Balance);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
