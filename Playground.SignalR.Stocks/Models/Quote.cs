using System;

namespace Playground.SignalR.Stocks.Models
{
    public class Quote
    {
        public string Symbol { get; private set; }
        public decimal Price { get; private set; }
        public DateTime Time { get; private set; }
        
        public static Quote Create(string symbol) => 
            new Quote { Symbol = symbol };

        public void Update(decimal price)
        {
            Price = price;
            Time = DateTime.Now;
        }
    }
}