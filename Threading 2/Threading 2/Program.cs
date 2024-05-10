using System;
using System.Threading;

namespace Threading_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            // Open 5 bank accounts with initial balance of $1000 each
            for (int i = 0; i < 5; i++)
            {
                bank.OpenAccount(1000);
            }

            // Create threads for different operations
            Thread depositsThread = new Thread(() => RandomAccountActions(bank, 60));
            Thread withdrawalsThread = new Thread(() => RandomAccountActions(bank, 60));
            Thread transfersThread = new Thread(() => RandomAccountActions(bank, 60));
            Thread monitoringThread = new Thread(() => RandomAccountActions(bank, 60));
            {
                DateTime endTime = DateTime.Now.AddSeconds(60);
                while (DateTime.Now < endTime)
                {
                    Console.WriteLine($"Total Balance in the Bank: ${bank.GetTotalBalance()}");

                    Thread.Sleep(5000); // Display total balance every 5 seconds
                }
                Console.WriteLine("Monitoring thread was interrupted.");
            };

            // Start the threads
            depositsThread.Start();
            withdrawalsThread.Start();
            transfersThread.Start();
            monitoringThread.Start();

            // Wait for threads to finish
            depositsThread.Join();
            withdrawalsThread.Join();
            transfersThread.Join();

            // Interrupt monitoring thread
            monitoringThread.Interrupt();
            monitoringThread.Join();

            // Display completion message
            Console.WriteLine("All threads have completed their tasks.");
        }

        // Helper method to perform random actions on accounts
        static void RandomAccountActions(Bank bank, int durationSeconds)
        {
            Random random = new Random();
            DateTime endTime = DateTime.Now.AddSeconds(durationSeconds);
            while (DateTime.Now < endTime)
            {
                int action = random.Next(3); // 0 for deposit, 1 for withdraw, 2 for transfer
                int accountIndex = random.Next(5) + 1; // Select a random account (1 to 5)
                double amount = random.Next(100, 501); // Random amount between $100 and $500

                try
                {
                    switch (action)
                    {
                        case 0:
                            bank.Deposit(accountIndex, amount);
                            break;
                        case 1:
                            bank.Withdraw(accountIndex, amount);
                            break;
                        case 2:
                            int destinationAccountIndex = random.Next(5) + 1; // Select a random destination account (1 to 5)
                            bank.Transfer(accountIndex, destinationAccountIndex, amount);
                            break;
                    }
                }
                catch (InsufficientFundsException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(random.Next(1000, 3001)); // Sleep for 1-3 seconds before next action
            }
        }
    }
}
