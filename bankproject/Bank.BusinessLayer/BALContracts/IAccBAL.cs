using bankentities;
using System;
using System.Collections.Generic;

namespace Bank.BusinessLayer.BALContracts
{
    public interface IAccBAL
    {

       
        List<Account> GetAccounts();

        
        List<Account> GetAccountsByCondition(Predicate<Account> predicate);

        
        Guid AddAccount(Account account);

        
        bool UpdateAccount(Account account);

       
        bool DeleteAccount(Guid accountID);
    }
}


