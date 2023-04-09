using System.Configuration;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using TradingBot.Application.Common.Enum;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ExchangeApiClients.TinkoffApi;

internal class TinkoffApiClient : IExchangeApiClient
{
    private readonly InvestApiClient _investApiClient;
    public TinkoffApiClient() => _investApiClient = InvestApiClientFactory.Create(ConfigurationManager.AppSettings["tokenTinkoff"]);
    public Task ClosePositionAllOrderBySymbolAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task ClosePositionConcreteOrderBySymbolAsync(string symbol, long orderId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateBuyLimitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        //Заполнить PostOrderRequest
        await _investApiClient.Orders.PostOrderAsync(new PostOrderRequest());
    }

    public Task CreateBuyMarketOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task CreateStopLossOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task CreateTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task CreateTrailingTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetAccountBalanceAsync(string currencyCode)
    {
        throw new NotImplementedException();
    }
}
