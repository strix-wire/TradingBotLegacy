using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Queries.GetAccountBalance;

internal class GetAccountBalanceQuery : IRequest
{
    public IExchangeApiClient ExchangeApiClient { get; set; }
}
