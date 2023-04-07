using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyMarketOrder;

internal class CreateBuyMarketOrderCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
