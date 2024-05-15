using System;

namespace bankentities.Contracts
{
    public interface ICustomer
    {
        Guid CustomerID { get; set; }
        long CustomerCode {  get; set; }
        string CustomerName { get; set; }
        string Address {  get; set; }
        string Country {  get; set; }
        string Mobile {  get; set; }

    }
}
