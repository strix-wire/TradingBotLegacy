using Binance.Net.Interfaces.Clients.SpotApi;
using TradingBot.Application.Interfaces;
using TradingBot.Domain.Classes;
using TradingBot.Domain.Enums;

namespace TradingBot.Application.ExchangeApiClients.BinanceApi;

internal class BinanceUsdSpotApiClient : IExchangeApiClient
{
    private readonly IBinanceClientSpotApi _clientHttp;
    private readonly IBinanceSocketClientSpotStreams _clientSocket;

    public BinanceUsdSpotApiClient(IBinanceClientSpotApi clientHttp,
        IBinanceSocketClientSpotStreams clientSocket) =>
            (_clientHttp, _clientSocket) = (clientHttp, clientSocket);

    public Task ClosePositionAllOrderBySymbolAsync(string symbol)
    {
        throw new NotImplementedException();
    }

    public Task ClosePositionConcreteOrderBySymbolAsync(string symbol, long orderId)
    {
        throw new NotImplementedException();
    }

    public Task CreateBuyLimitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
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

    public Task<IEnumerable<Candle>> GetCandlesHistoryAsync(string symbol, KlineInterval klineInterval, int limit)
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

    public async Task<Glass> GetGlassAsync(string symbol, int capacity)
    {
        var orderBook = await _clientHttp.ExchangeData.GetOrderBookAsync(symbol, capacity);
        var glass = new Glass(capacity);
        glass.UpdateWholeGlass(orderBook.Data.Bids.ToDictionary(x => x.Price, x => x.Quantity),
            orderBook.Data.Asks.ToDictionary(x => x.Price, x => x.Quantity));

        return glass;
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
