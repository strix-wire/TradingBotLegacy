using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.ClosePositionConcreteOrderBySymbol
{
    internal class ClosePositionConcreteOrderBySymbolCommand : IRequest
    {
        public ClosePositionConcreteOrderBySymbolCommand(IExchangeApiClient exchangeApiClient, string symbol, long orderId)
            => (ExchangeApiClient, Symbol, OrderId) = (exchangeApiClient, symbol, orderId);
        public IExchangeApiClient ExchangeApiClient { get; set; }
        public string Symbol { get; set; }
        public long OrderId { get; set; }
    }
}
