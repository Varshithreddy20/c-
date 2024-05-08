using System;
using System.Collections.Generic;
using bankentities;
using Bank.BusinessLayer.BALContracts;
using bank.dataaccess;
using bank.dataaccess.DALContracts;
using Bank.Expections;

namespace Bank.BusinessLayer
{
    public class CusBAL : ICusBAL
    {
        private ICusDataAccess _customersDataAccess;

        public CusBAL(ICusDataAccess customersDataAccess)
        {
            _customersDataAccess = customersDataAccess;
        }

        public List<Customer> GetCustomer()
        {
            try
            {
                return _customersDataAccess.GetCustomers();
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Customer> GetCustomerByCondition(Predicate<Customer> predicate)
        {
            try
            {
                return _customersDataAccess.GetCustomerByCondition(predicate);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid AddCustomer(Customer customer)
        {
            try
            {
                // Implement your logic for generating customer code here
                // For example:
                // customer.CustomerCode = GenerateCustomerCode();
                return _customersDataAccess.AddCustomer(customer);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                return _customersDataAccess.UpdateCustomer(customer);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                return _customersDataAccess.DeleteCustomer(customerID);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
