using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Playground.SignalR.Stocks.Hubs
{
    public class QuoteHub : Hub<IQuoteHub>
    {
        public async Task ChangeSubscription(string oldSymbol, string newSymbol)
        {
            if(!string.IsNullOrEmpty(oldSymbol))
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, oldSymbol);
            
            await Groups.AddToGroupAsync(Context.ConnectionId, newSymbol);
        }
    }
}