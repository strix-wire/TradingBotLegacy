using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyMarketOrder;

internal class CreateBuyMarketOrderCommand : IRequest
{
    public CreateBuyMarketOrderCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
