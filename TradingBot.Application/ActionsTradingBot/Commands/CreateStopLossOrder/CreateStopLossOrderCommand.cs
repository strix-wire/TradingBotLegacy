using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateStopLossOrder;

internal class CreateStopLossOrderCommand : IRequest
{
    public CreateStopLossOrderCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
