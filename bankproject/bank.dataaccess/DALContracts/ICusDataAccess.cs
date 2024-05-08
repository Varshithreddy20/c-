using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bankentities;
using bank.dataaccess;

namespace bank.dataaccess.DALContracts
{
    public interface ICusDataAccess
    {
        List<Customer> GetCustomer();
        List<Customer> GetCustomerByCondition(Predicate<Customer> predicate);
        Guid AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Guid customerID);
    }
}
