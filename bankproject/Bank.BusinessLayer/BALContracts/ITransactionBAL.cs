using bankentities;
using System;
using System.Collections.Generic;

namespace Bank.BusinessLayer.BALContracts
{
    public interface ITransactionBAL
    {
            List<Transaction> GetTransactions();

            List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate);

           
            Guid AddTransaction(Transaction transaction);

            bool UpdateTransaction(Transaction transaction);

            bool DeleteTransaction(Guid transactionID);
        }
    }

