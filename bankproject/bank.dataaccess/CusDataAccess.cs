using bank.dataaccess.DALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;

namespace bank.dataaccess
{
    public class CusDataAccess : ICusDataAccess
    {
        #region Fields
        private static List<Customer> _customers;
        #endregion


        #region Constructors
        static CusDataAccess()
        {
            _customers = new List<Customer>() 
            {
                new Customer() { CustomerID = Guid.Parse("8C12BEA9-8FB0-4744-8422-1996533805E8"), CustomerCode = 1001, CustomerName = "Accion", Country = "India", Address = "Mumbai", Mobile = "9876598765" }
             };
        }
        #endregion


        #region Properties
     
        private static List<Customer> Customers
        {
            set => _customers = value;
            get => _customers;
        }
        #endregion


        #region Methods
    
        public List<Customer> GetCustomers()
        {
            try
            {
                //create a new customers list
                List<Customer> customersList = new List<Customer>();

                //copy all customers from the soruce collection into the newCustomers list
                Customers.ForEach(item => customersList.Add(item.Clone() as Customer));
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

        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                //create a new customers list
                List<Customer> customersList = new List<Customer>();

                //filter the collection
                List<Customer> filteredCustomers = Customers.FindAll(predicate);

                //copy all customers from the soruce collection into the newCustomers list
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
                //generate new Guid
                customer.CustomerID = Guid.NewGuid();

                //add customer
                Customers.Add(customer);

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
                //find existing customer by CustomerID
                Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);

                //update all details of customer
                if (existingCustomer != null)
                {
                    existingCustomer.CustomerCode = customer.CustomerCode;
                    existingCustomer.CustomerName = customer.CustomerName;
                    existingCustomer.Address = customer.Address;
                    existingCustomer.Country = customer.Country;
                    existingCustomer.Mobile = customer.Mobile;

                    return true; //indicates the customer is updated
                }
                else
                {
                    return false; //indicates no object is updated
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
                //delete customer by CustomerID
                if (Customers.RemoveAll(item => item.CustomerID == customerID) > 0)
                {
                    return true;  //indicates one or more customers are deleted
                }
                else
                {
                    return false; //indicates no customer is deleted
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
        #endregion
    }
}
