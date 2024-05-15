using bankentities.contracts;
using System;

namespace bankentities
{
    public class Transaction : ITransaction, ICloneable
    {
        #region Fields

        private Guid _transactionID;
        private Guid _sourceAccountID;
        private Guid _destinationAccountID;
        private decimal _amount;
        private DateTime _transactionDateTime;

        #endregion


        #region Properties

       
        public Guid TransactionID
        {
            get { return _transactionID; }
            set { _transactionID = value; }
        }

       
        public Guid SourceAccountID
        {
            get { return _sourceAccountID; }
            set { _sourceAccountID = value; }
        }

       
        public Guid DestinationAccountID
        {
            get { return _destinationAccountID; }
            set { _destinationAccountID = value; }
        }

     
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public DateTime TransactionDateTime
        {
            get { return _transactionDateTime; }
            set { _transactionDateTime = value; }
        }

        #endregion


        #region Constructors

        
        public Transaction()
        {
            // Initialize properties
            SourceAccountID = Guid.Empty;
            DestinationAccountID = Guid.Empty;
            Amount = 0.0m;
            TransactionDateTime = DateTime.MinValue;
        }

       
        public Transaction(Guid sourceAccountID, Guid destinationAccountID, decimal amount, DateTime transactionDateTime)
        {
            SourceAccountID = sourceAccountID;
            DestinationAccountID = destinationAccountID;
            Amount = amount;
            TransactionDateTime = transactionDateTime;
        }

        #endregion



        #region Methods
        public object Clone()
        {
            return new Transaction() { SourceAccountID = SourceAccountID, DestinationAccountID = DestinationAccountID, Amount = Amount, TransactionDateTime = TransactionDateTime };
        }
        #endregion

    }
}
