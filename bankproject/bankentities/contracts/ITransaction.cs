using System;

namespace bankentities.contracts
{
    public interface ITransaction
    {
        Guid TransactionID { get; set; }
        decimal Amount { get; set; }
        Guid DestinationAccountID { get; set; }
        Guid SourceAccountID { get; set; }
        DateTime TransactionDateTime { get; set; }
    }
}
