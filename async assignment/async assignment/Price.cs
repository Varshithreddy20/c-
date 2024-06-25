using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace async_assignment
{
    public class StockPriceGenerator
    {
        private readonly Random _random = new Random();
        private readonly List<Stock> _stocks;
        private decimal threshold;

        public StockPriceGenerator(List<Stock> stocks)
        {
            _stocks = stocks;
        }

        public async Task StartUpdatingPrices(TimeSpan interval)
        {
            while (true)
            {
                foreach (var stock in _stocks)
                {
                    UpdateStockPrice(stock);
                }
                await Task.Delay(interval);
            }
        }

        private void UpdateStockPrice(Stock stock)
        {
            // Simulate price change
            decimal change = (decimal)(_random.NextDouble() * 10 - 5);
            stock.Price += change;
            Console.WriteLine($"Updated price of {stock.Symbol} to {stock.Price}");
            // Check if price exceeds threshold
            if (stock.Price > threshold)
            {
                // Signal event
                OnPriceExceedThreshold?.Invoke(this, new StockEventArgs(stock));
            }
        }

        public event EventHandler<StockEventArgs> OnPriceExceedThreshold;
    }
}
