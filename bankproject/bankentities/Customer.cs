using Bank.Expections;
using bankentities.Contracts;
using System;


namespace bankentities
{
    public class Customer : ICustomer, ICloneable
    {
        #region Private Fields

        private Guid _customerID;
        private long _customerCode;
        private string _customerName;
        private string _address;
        private string _country;
        private string _mobile;

        #endregion

        #region Properties
        public Guid CustomerID { get => _customerID; set => _customerID = value; }

       
        public long CustomerCode
        {
            get => _customerCode;
            set
            {
                if (value > 0)
                {
                    _customerCode = value;
                }
                else
                {
                    throw new CustomerException("Customer should not be in negative");
                }
            }
        }

        public string CustomerName
        {
            get => _customerName;
            set
            {
                if (value.Length <= 40)
                {
                    _customerName = value;
                }
                else
                {
                    throw new CustomerException("Name is exceeding the given length");
                }
            }
        }
        public string Address { get => _address; set => _address = value; }
        public string Country { get => _country; set => _country = value; }

        public string Mobile
        {
            get => _mobile;
            set
            {
                if (value.Length == 10)
                {
                    _mobile = value;
                }
                else
                {
                    throw new CustomerException("Moble number should only contain 10 didgits");
                }
            }
        }
        #endregion

        #region Methods
        public object Clone()
        {
            return new Customer() { CustomerID = this.CustomerID, CustomerCode = this.CustomerCode, CustomerName = this.CustomerName,Address=this.Address,Country=this.Country,Mobile=this.Mobile };
        }
        #endregion
    }
}
