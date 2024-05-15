using bank.dataaccess;
using bank.dataaccess.DALContracts;
using Bank.BusinessLayer.BALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank.BusinessLayer
{
    public class TransactionBAL : ITransactionBAL
    {
        #region Private Fields
        private ITransactionDataAccess _transactionsDataAccessLayer;
        private IAccDataAccess _accountsDataAccessLayer;
        #endregion


        #region Constructors
    
        public TransactionBAL()
        {
            _transactionsDataAccessLayer = new TransactionDataAccess();
            _accountsDataAccessLayer = new AccDataAccess();
        }
        #endregion


        #region Properties
     
        private ITransactionDataAccess TransactionsDataAccessLayer
        {
            set => _transactionsDataAccessLayer = value;
            get => _transactionsDataAccessLayer;
        }

        private IAccDataAccess AccountsDataAccessLayer
        {
            set => _accountsDataAccessLayer = value;
            get => _accountsDataAccessLayer;
        }
        #endregion


        #region Methods
     
        public List<Transaction> GetTransactions()
        {
            try
            {
                //invoke DAL
                return TransactionsDataAccessLayer.GetTransactions();
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
                //invoke DAL
                return TransactionsDataAccessLayer.GetTransactionsByCondition(predicate);
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
                //invoke DAL
                var sourceAccount = AccountsDataAccessLayer.GetAccountsByCondition(temp => temp.AccountID == transaction.SourceAccountID).FirstOrDefault();
                var destinationAccount = AccountsDataAccessLayer.GetAccountsByCondition(temp => temp.AccountID == transaction.DestinationAccountID).FirstOrDefault();

                if (sourceAccount != null && destinationAccount != null)
                {
                    if (sourceAccount.Balance < transaction.Amount)
                    {
                        throw new TransactionException($"Source account has insuffient funds for transaction of {transaction.Amount}");
                    }

                    sourceAccount.Balance -= transaction.Amount;
                    destinationAccount.Balance += transaction.Amount;

                    var newTransactionID = TransactionsDataAccessLayer.AddTransaction(transaction);
                    AccountsDataAccessLayer.UpdateAccount(sourceAccount);
                    AccountsDataAccessLayer.UpdateAccount(destinationAccount);

                    return newTransactionID;
                }

                throw new TransactionException("Source account or destination account number is invalid");
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
                //invoke DAL
                return TransactionsDataAccessLayer.UpdateTransaction(transaction);
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
                //invoke DAL
                return TransactionsDataAccessLayer.DeleteTransaction(transactionID);
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

