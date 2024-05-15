using bank.dataaccess;
using bank.dataaccess.DALContracts;
using Bank.BusinessLayer.BALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;

namespace Bank.BusinessLayer
{

    public class AccBAL : IAccBAL
    {
        private IAccDataAccess _accountsDataAccessLayer;


        #region Constructors
      
        public AccBAL()
        {
            _accountsDataAccessLayer = new AccDataAccess();
        }
        #endregion


        #region Properties
       
        private IAccDataAccess AccountsDataAccessLayer
        {
            set => _accountsDataAccessLayer = value;
            get => _accountsDataAccessLayer;
        }
        #endregion


        #region Methods
     
        public List<Account> GetAccounts()
        {
            try
            {
                //invoke DAL
                return AccountsDataAccessLayer.GetAccounts();
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

 
        public List<Account> GetAccountsByCondition(Predicate<Account> predicate)
        {
            try
            {
                //invoke DAL
                return AccountsDataAccessLayer.GetAccountsByCondition(predicate);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

    
        public Guid AddAccount(Account account)
        {
            try
            {
           
                List<Account> allAccounts = AccountsDataAccessLayer.GetAccounts();
                long maxAccountNumber = 0;
                foreach (var item in allAccounts)
                {
                    if (item.AccountNumber > maxAccountNumber)
                    {
                        maxAccountNumber = item.AccountNumber;
                    }
                }

                if (allAccounts.Count >= 1)
                {
                    account.AccountNumber = maxAccountNumber + 1;
                }
                else
                {
                    account.AccountNumber = Bank.Configuration.Settings.BaseAccountNo + 1;
                }
                account.Balance = 0.0M;

              
                return AccountsDataAccessLayer.AddAccount(account);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public bool UpdateAccount(Account account)
        {
            try
            {
                //invoke DAL
                return AccountsDataAccessLayer.UpdateAccount(account);
            }
            catch (AccountException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        public bool DeleteAccount(Guid accountID)
        {
            try
            {
            
                return AccountsDataAccessLayer.DeleteAccount(accountID);
            }
            catch (AccountException)
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


