using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyLimitOrder;

internal class CreateBuyLimitOrderCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
