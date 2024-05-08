using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Expections
{
    /// <summary>
    /// represnts errors that are raised in customer class
    /// </summary>
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
