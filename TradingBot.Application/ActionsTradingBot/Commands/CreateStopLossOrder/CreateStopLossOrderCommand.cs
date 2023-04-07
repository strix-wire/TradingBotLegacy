using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateStopLossOrder;

internal class CreateStopLossOrderCommand : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
