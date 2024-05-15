using System;
using System.Collections.Generic;
using bankentities;

namespace Bank.BusinessLayer.BALContracts
{
    public interface ICusBAL
    {
        List<Customer> GetCustomers();

        
        List<Customer> GetCustomersByCondition(Predicate<Customer> predicate);

        
        Guid AddCustomer(Customer customer);

       
        bool UpdateCustomer(Customer customer);

        
        bool DeleteCustomer(Guid customerID);
    }
}
