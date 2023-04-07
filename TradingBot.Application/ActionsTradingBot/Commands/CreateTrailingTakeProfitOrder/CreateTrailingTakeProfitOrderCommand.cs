using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTrailingTakeProfitOrder;

internal class CreateTrailingTakeProfitOrderCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
