using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace async_assignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var stocks = new List<Stock>
            {
                new Stock { Symbol = "AAPL", Price = 150 },
                new Stock { Symbol = "GOOG", Price = 2500 }
            };

            var priceGenerator = new StockPriceGenerator(stocks);

            // Subscribe to event
            priceGenerator.OnPriceExceedThreshold += PriceExceedThresholdHandler;

            // Start updating prices
            var priceUpdateTask = priceGenerator.StartUpdatingPrices(TimeSpan.FromSeconds(1));

            // Display stock data
            DisplayStocks(stocks);

            // User interaction (buy/sell stocks)
            // Implement user interaction logic

            await priceUpdateTask;
        }

        static void DisplayStocks(List<Stock> stocks)
        {
            foreach (var stock in stocks)
            {
                Console.WriteLine($"Symbol: {stock.Symbol}, Price: {stock.Price}");
            }
        }

        static void PriceExceedThresholdHandler(object sender, StockEventArgs e)
        {
            Console.WriteLine($"Price of {e.Stock.Symbol} exceeded threshold: {e.Stock.Price}");
        }
    }
}
