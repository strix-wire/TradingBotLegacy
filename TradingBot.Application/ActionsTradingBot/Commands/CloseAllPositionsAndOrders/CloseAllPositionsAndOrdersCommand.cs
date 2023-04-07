using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CloseAllPositionsAndOrders;

internal class CloseAllPositionsAndOrdersCommand : IRequest
{
    public CloseAllPositionsAndOrdersCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;

    public IExchangeApiClient ExchangeApiClient { get; init; }
}
