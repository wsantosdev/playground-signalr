using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Playground.SignalR.Stocks.Hubs;
using Playground.SignalR.Stocks.Models;

namespace Playground.SignalR.Stocks.Workers
{
    public class QuoteWorker : BackgroundService
    {
        private readonly Quote[] _quotes = { Quote.Create("PETR4"), Quote.Create("VALE3"), Quote.Create("ITUB4"), Quote.Create("BBDC4"), Quote.Create("BBAS3") };
        private readonly IHubContext<QuoteHub, IQuoteHub> _hub;
        private readonly QuotePriceGenerator _priceGenerator;

        public QuoteWorker(IHubContext<QuoteHub, IQuoteHub> hub, QuotePriceGenerator priceGenerator)
        {
            _hub = hub;
            _priceGenerator = priceGenerator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                foreach(Quote quote in _quotes)
                {
                    quote.Update(_priceGenerator.Generate(quote.Price));
                    
                    await _hub.Clients.Group(quote.Symbol).SendQuote(quote);
                }
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}