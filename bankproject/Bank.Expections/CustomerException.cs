using System;

namespace Bank.Expections
{
    
    public class CustomerException:ApplicationException
    {
        public CustomerException(string message) : base(message)
        {

        }
        public CustomerException(string message,Exception innerExpection):base(message, innerExpection) 
        { 

        }

    }
}
