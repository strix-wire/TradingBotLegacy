using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTakeProfitOrder;

internal class CreateTakeProfitOrderCommand : IRequest
{
    public CreateTakeProfitOrderCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
