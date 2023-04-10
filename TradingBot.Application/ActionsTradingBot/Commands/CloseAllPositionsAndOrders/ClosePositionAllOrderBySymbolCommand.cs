using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CloseAllPositionsAndOrders;

internal class ClosePositionAllOrderBySymbolCommand : IRequest
{
    public ClosePositionAllOrderBySymbolCommand(IExchangeApiClient exchangeApiClient, string symbol)
        => (ExchangeApiClient, Symbol) = (exchangeApiClient, symbol);

    public string Symbol { get; set; }
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
