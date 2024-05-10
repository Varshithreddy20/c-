using System;
using System.Collections.Generic;
using System.Threading;

namespace Threading_2
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() { }
        public InsufficientFundsException(string message) : base(message) { }
        public InsufficientFundsException(string message, Exception inner) : base(message, inner) { }
    }

    // BankAccount class representing a bank account
    public class BankAccount
    {
        private static int nextAccountNumber = 1;
        private readonly int accountNumber;
        private double balance;
        private readonly object lockObject = new object();

        public BankAccount(double initialBalance)
        {
            accountNumber = nextAccountNumber++;
            balance = initialBalance;
        }

        public int AccountNumber => accountNumber;
        public double Balance => balance;

        // Deposit method
        public void Deposit(double amount)
        {
            lock (lockObject)
            {
                balance += amount;
                Console.WriteLine($"Deposited ${amount} into Account {accountNumber}. Updated Balance: {balance}");
            }
        }

        // Withdraw method
        public void Withdraw(double amount)
        {
            lock (lockObject)
            {
                if (balance < amount)
                    throw new InsufficientFundsException($"Insufficient funds in Account {accountNumber}.");

                balance -= amount;
                Console.WriteLine($"Withdrawn ${amount} from Account {accountNumber}. Updated Balance: {balance}");
            }
        }

        // TransferTo method
        public void TransferTo(BankAccount destination, double amount)
        {
            lock (lockObject)
            {
                if (balance < amount)
                    throw new InsufficientFundsException($"Insufficient funds in Account {accountNumber}.");

                destination.Deposit(amount);
                balance -= amount;
                Console.WriteLine($"Transferred ${amount} from Account {accountNumber} to Account {destination.AccountNumber}. Updated Balance in Source Account: {balance}. Updated Balance in Destination Account: {destination.Balance}");
            }
        }
    }

    // Bank class representing a bank
    public class Bank
    {
        private readonly List<BankAccount> accounts = new List<BankAccount>();
        private readonly object lockObject = new object();

        // OpenAccount method
        public void OpenAccount(double initialBalance)
        {
            lock (lockObject)
            {
                var account = new BankAccount(initialBalance);
                accounts.Add(account);
                Console.WriteLine($"Opened Account {account.AccountNumber} with initial balance ${initialBalance}");
            }
        }

        // CloseAccount method
        public void CloseAccount(int accountNumber)
        {
            lock (lockObject)
            {
                var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    accounts.Remove(account);
                    Console.WriteLine($"Closed Account {accountNumber}");
                }
                else
                {
                    Console.WriteLine($"Account {accountNumber} not found.");
                }
            }
        }

        // GetTotalBalance method
        public double GetTotalBalance()
        {
            double totalBalance = 0;
            lock (lockObject)
            {
                foreach (var account in accounts)
                {
                    totalBalance += account.Balance;
                }
            }
            return totalBalance;
        }

        // Deposit method
        public void Deposit(int accountNumber, double amount)
        {
            lock (lockObject)
            {
                var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Deposit(amount);
                }
                else
                {
                    Console.WriteLine($"Account {accountNumber} not found.");
                }
            }
        }

        // Withdraw method
        public void Withdraw(int accountNumber, double amount)
        {
            lock (lockObject)
            {
                var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
                if (account != null)
                {
                    account.Withdraw(amount);
                }
                else
                {
                    Console.WriteLine($"Account {accountNumber} not found.");
                }
            }
        }

        // Transfer method
        public void Transfer(int sourceAccountNumber, int destinationAccountNumber, double amount)
        {
            lock (lockObject)
            {
                var sourceAccount = accounts.Find(acc => acc.AccountNumber == sourceAccountNumber);
                var destinationAccount = accounts.Find(acc => acc.AccountNumber == destinationAccountNumber);
                if (sourceAccount != null && destinationAccount != null)
                {
                    sourceAccount.TransferTo(destinationAccount, amount);
                }
                else
                {
                    Console.WriteLine("One or both accounts not found.");
                }
            }
        }
    }
}
