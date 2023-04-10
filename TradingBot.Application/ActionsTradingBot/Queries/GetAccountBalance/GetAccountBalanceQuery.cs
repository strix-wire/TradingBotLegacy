using MediatR;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Queries.GetAccountBalance;

internal class GetAccountBalanceQuery : IRequest
{
    public GetAccountBalanceQuery(IExchangeApiClient exchangeApiClient, string currencyCode) => (ExchangeApiClient, CurrencyCode) = (exchangeApiClient, currencyCode);
    public IExchangeApiClient ExchangeApiClient { get; init; }
    public string CurrencyCode { get; set; }
}
