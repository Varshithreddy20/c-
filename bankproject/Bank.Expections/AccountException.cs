using System;

namespace Bank.Expections
{
    public class AccountException : ApplicationException
 
    {
            public AccountException(string message) : base(message)
            {
            }

           
            public AccountException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }


