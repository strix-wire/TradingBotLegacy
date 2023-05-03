using System.Configuration;
using Tinkoff.InvestApi;
using Tinkoff.InvestApi.V1;
using TradingBot.Application.Interfaces;
using TradingBot.Domain.Classes;
using TradingBot.Domain.Enums;

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

    public Task<decimal> CreateBuyMarketOrderAsync(string symbol, OrderSide orderSide, decimal quantity)
    {
        throw new NotImplementedException();
    }

    public Task CreateStopLossOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    public Task<long> CreateStopLossOrderAsync(string symbol, OrderSide orderSide, decimal price)
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

    public Task<IEnumerable<Domain.Classes.Candle>> GetCandlesHistoryAsync(string symbol, KlineInterval klineInterval, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetFeeLimit()
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetFeeMarket()
    {
        throw new NotImplementedException();
    }

    public Task GetGlass(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task<Glass> GetGlassAsync(string symbol, int capacity)
    {
        throw new NotImplementedException();
    }

    public Task<OrderStatus> GetOrderStatus(string symbol, long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetPriceAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task<decimal> GetPriceStep(string symbol)
    {
        throw new NotImplementedException();
    }

    Task<long> IExchangeApiClient.CreateTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }

    Task<long> IExchangeApiClient.CreateTrailingTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }
}
