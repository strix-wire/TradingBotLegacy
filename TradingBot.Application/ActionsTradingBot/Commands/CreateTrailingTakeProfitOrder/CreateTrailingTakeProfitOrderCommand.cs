using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTrailingTakeProfitOrder;

internal class CreateTrailingTakeProfitOrderCommand : IRequest
{
    public CreateTrailingTakeProfitOrderCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
