using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CloseAllPositionsAndOrders;

internal class CloseAllPositionsAndOrdersCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
