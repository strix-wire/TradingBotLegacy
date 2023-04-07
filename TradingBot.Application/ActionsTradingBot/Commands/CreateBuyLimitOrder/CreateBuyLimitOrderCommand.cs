using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyLimitOrder;

internal class CreateBuyLimitOrderCommand : IRequest
{
    public CreateBuyLimitOrderCommand(IExchangeApiClient exchangeApiClient) => ExchangeApiClient = exchangeApiClient;
    public IExchangeApiClient ExchangeApiClient { get; init; }
}
