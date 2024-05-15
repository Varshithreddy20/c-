using bank.dataaccess.DALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;

namespace bank.dataaccess
{
    public class TransactionDataAccess : ITransactionDataAccess
    {
        #region Fields
        private static List<Transaction> _transactions;
        #endregion


        #region Constructors
        static TransactionDataAccess()
        {
            _transactions = new List<Transaction>();
        }
        #endregion


        #region Properties
      
        private static List<Transaction> Transactions
        {
            set => _transactions = value;
            get => _transactions;
        }
        #endregion



        #region Methods
   
        public List<Transaction> GetTransactions()
        {
            try
            {
                //create a new transactions list
                List<Transaction> transactionsList = new List<Transaction>();

                //copy all transactions from the soruce collection into the newTransactions list
                Transactions.ForEach(item => transactionsList.Add(item.Clone() as Transaction));
                return transactionsList;
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Transaction> GetTransactionsByCondition(Predicate<Transaction> predicate)
        {
            try
            {
                //create a new transactions list
                List<Transaction> transactionsList = new List<Transaction>();

                //filter the collection
                List<Transaction> filteredTransactions = Transactions.FindAll(predicate);

                //copy all transactions from the soruce collection into the newTransactions list
                filteredTransactions.ForEach(item => transactionsList.Add(item.Clone() as Transaction));
                return transactionsList;
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


    
        public Guid AddTransaction(Transaction transaction)
        {
            try
            {
                //generate new Guid
                transaction.TransactionID = Guid.NewGuid();

                //add transaction
                Transactions.Add(transaction);

                return transaction.TransactionID;
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            try
            {
                //find existing transaction by TransactionID
                Transaction existingTransaction = Transactions.Find(item => item.TransactionID == transaction.TransactionID);

                //update all details of transaction
                if (existingTransaction != null)
                {
                    existingTransaction.SourceAccountID = transaction.SourceAccountID;
                    existingTransaction.DestinationAccountID = transaction.DestinationAccountID;
                    existingTransaction.Amount = transaction.Amount;
                    existingTransaction.TransactionDateTime = transaction.TransactionDateTime;

                    return true; //indicates the transaction is updated
                }
                else
                {
                    return false; //indicates no object is updated
                }
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteTransaction(Guid transactionID)
        {
            try
            {
                //delete transaction by TransactionID
                if (Transactions.RemoveAll(item => item.TransactionID == transactionID) > 0)
                {
                    return true;  //indicates one or more transactions are deleted
                }
                else
                {
                    return false; //indicates no transaction is deleted
                }
            }
            catch (TransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
