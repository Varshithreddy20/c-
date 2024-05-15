using bank.dataaccess.DALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;

namespace bank.dataaccess
{


    public class AccDataAccess : IAccDataAccess
    {
        #region Fields
        private static List<Account> _accounts;
        #endregion


        #region Constructors
        static AccDataAccess()
        {
            _accounts = new List<Account>()
            {
                new Account() { AccountID = Guid.Parse("E3B7F3CB-1315-431B-8E60-4FE6D79084C8"), AccountNumber = 10001, Balance = 5000, CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8") },

            };
        }
        #endregion


        #region Properties
     
        private static List<Account> Accounts
        {
            set => _accounts = value;
            get => _accounts;
        }
        #endregion



        #region Methods
      
        public List<Account> GetAccounts()
        {
            try
            {
                //create a new accounts list
                List<Account> accountsList = new List<Account>();

                //copy all accounts from the soruce collection into the newAccounts list
                Accounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
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
                //create a new accounts list
                List<Account> accountsList = new List<Account>();

                //filter the collection
                List<Account> filteredAccounts = Accounts.FindAll(predicate);

                //copy all accounts from the soruce collection into the newAccounts list
                filteredAccounts.ForEach(item => accountsList.Add(item.Clone() as Account));
                return accountsList;
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
                //generate new Guid
                account.AccountID = Guid.NewGuid();

                //add account
                Accounts.Add(account);

                return account.AccountID;
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
                //find existing account by AccountID
                Account existingAccount = Accounts.Find(item => item.AccountID == account.AccountID);

                //update all details of account
                if (existingAccount != null)
                {
                    existingAccount.AccountID = account.AccountID;
                    existingAccount.AccountNumber = account.AccountNumber;
                    existingAccount.CustomerID = account.CustomerID;
                    existingAccount.Balance = account.Balance;


                    return true; //indicates the account is updated
                }
                else
                {
                    return false; //indicates no object is updated
                }
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
                //delete account by AccountID
                if (Accounts.RemoveAll(item => item.AccountID == accountID) > 0)
                {
                    return true;  //indicates one or more accounts are deleted
                }
                else
                {
                    return false; //indicates no account is deleted
                }
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

