using System;

namespace async_assignment
{
    public class StockEventArgs : EventArgs
    {
        public Stock Stock { get; set; }

        public StockEventArgs(Stock stock)
        {
            Stock = stock;
        }
    }
}
