using bank.dataaccess;
using bank.dataaccess.DALContracts;
using Bank.BusinessLayer.BALContracts;
using Bank.Expections;
using bankentities;
using System;
using System.Collections.Generic;

namespace Bank.BusinessLayer
{
    public class CusBAL : ICusBAL
    {

        #region Private Fields
        private ICusDataAccess _customersDataAccessLayer;
        #endregion

        #region Constructors
        
        public CusBAL()
        {
            _customersDataAccessLayer = new CusDataAccess();
        }
        #endregion


        #region Properties
       
        private ICusDataAccess CustomersDataAccessLayer
        {
            set => _customersDataAccessLayer = value;
            get => _customersDataAccessLayer;
        }
        #endregion


        #region Methods
       
        public List<Customer> GetCustomers()
        {
            try
            {
               
                return CustomersDataAccessLayer.GetCustomers();
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
               
                return CustomersDataAccessLayer.GetCustomersByCondition(predicate);
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
                
                List<Customer> allCustomers = CustomersDataAccessLayer.GetCustomers();
                long maxCustCode = 0;
                foreach (var item in allCustomers)
                {
                    if (item.CustomerCode > maxCustCode)
                    {
                        maxCustCode = item.CustomerCode;
                    }
                }

                
                if (allCustomers.Count >= 1)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Bank.Configuration.Settings.BaseCustomerNo + 1;
                }

               
                return CustomersDataAccessLayer.AddCustomer(customer);
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
                
                return CustomersDataAccessLayer.UpdateCustomer(customer);
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
                //invoke DAL
                return CustomersDataAccessLayer.DeleteCustomer(customerID);
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
