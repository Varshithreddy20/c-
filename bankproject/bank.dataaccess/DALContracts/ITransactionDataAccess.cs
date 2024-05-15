using bankentities;
using System;
using System.Collections.Generic;

namespace bank.dataaccess.DALContracts
{
    public interface ITransactionDataAccess
    {
        List<Transaction> GetTransactions();

   
        List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate);

      
        Guid AddTransaction(Transaction transaction);

   
        bool UpdateTransaction(Transaction transaction);

  
        bool DeleteTransaction(Guid transactionID);
    }
}
