using bankentities;
using System;
using System.Collections.Generic;

namespace bank.dataaccess.DALContracts
{
    public interface ICusDataAccess
    {
        List<Customer> GetCustomers();
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);
        Guid AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Guid customerID);
    }
}
