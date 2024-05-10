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
        private ICusDataAccess _cusDataAccess;

        public CusBAL()
        {
            _cusDataAccess = new CusDataAccess();
        }

        private ICusBAL CusDataAccess
        {
            set => _cusDataAccess = value;
            get => _cusDataAccess;
        }

        //methods
        public List<Customer> GetCustomer()
        {
            try
            {
                return _cusDataAccess.GetCustomer();
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
                return CusDataAccess.GetCustomerByCondition(predicate);
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
                //get all customers
                List<Customer> allCustomers = CusDataAccess.customer();
                long maxCustCode = 0;
                foreach (var item in allCustomers)
                {
                    if (item.CustomerCode > maxCustCode)
                    {
                        maxCustCode = item.CustomerCode;
                    }
                }

                //generate new customer no
                if (allCustomers.Count >= 1)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Bank.Configuration.Settings.BaseCustomerNo + 1;
                }

                //invoke DAL
                return CusDataAccess.AddCustomer(customer);
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

        //public Guid AddCustomer(Customer customer)
        //{
        //    try
        //    {
        //        customer.CustomerCode = GenerateCustomerCode();
        //        return _cusDataAccess.AddCustomer(customer);
        //    }
        //    catch (CustomerException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //private long GenerateCustomerCode()
        //{
        //    throw new NotImplementedException();
        //}

        //public bool UpdateCustomer(Customer customer)
        //{
        //    try
        //    {
        //        return _cusDataAccess.UpdateCustomer(customer);
        //    }
        //    catch (CustomerException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public bool DeleteCustomer(Guid customerID)
        //{
        //    try
        //    {
        //        return _cusDataAccess.DeleteCustomer(customerID);
        //    }
        //    catch (CustomerException)
        //    {
        //        throw;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
