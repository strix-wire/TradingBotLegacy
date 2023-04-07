using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTakeProfitOrder;

internal class CreateTakeProfitOrderCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
