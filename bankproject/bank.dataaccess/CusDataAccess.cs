using System;
using System.Collections.Generic;
using bankentities;
using Bank.Expections;
using bank.dataaccess.DALContracts;

namespace bank.dataaccess
{
    public class CusDataAccess : ICusDataAccess
    {
        private List<Customer> _customers;

        public CusDataAccess()
        {
            _customers = new List<Customer>();
        }

        public List<Customer> Customers
        {
            set => _customers = value;
            get => _customers;
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                List<Customer> customersList = new List<Customer>();
                _customers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
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
                List<Customer> filteredCustomers = _customers.FindAll(predicate);
                List<Customer> customersList = new List<Customer>();
                filteredCustomers.ForEach(item => customersList.Add(item.Clone() as Customer));
                return customersList;
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
                customer.CustomerID = Guid.NewGuid();
                _customers.Add(customer);
                return customer.CustomerID;
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
                Customer existingCustomer = _customers.Find(item => item.CustomerID == customer.CustomerID);
                if (existingCustomer != null)
                {
                    existingCustomer.CustomerCode = customer.CustomerCode;
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.Mobile = customer.Mobile;
                    return true;
                }
                else
                {
                    return false;
                }
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
                return _customers.RemoveAll(item => item.CustomerID == customerID) > 0;
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
