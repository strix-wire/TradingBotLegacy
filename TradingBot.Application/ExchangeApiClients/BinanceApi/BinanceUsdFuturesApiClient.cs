using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using System.Configuration;
using TradingBot.Application.Interfaces;
using TradingBot.Domain.Classes;

namespace TradingBot.Application.ExchangeApiClients.BinanceApi;

public class BinanceUsdFuturesApiClient : IExchangeApiClient
{
    private readonly IBinanceClientUsdFuturesApi _clientHttp;
    private readonly IBinanceSocketClientUsdFuturesStreams _clientSocket;
    public BinanceUsdFuturesApiClient(IBinanceClientUsdFuturesApi clientHttp,
        IBinanceSocketClientUsdFuturesStreams clientSocket) => 
            (_clientHttp, _clientSocket) = (clientHttp, clientSocket);

    public async Task ClosePositionAllOrderBySymbolAsync(string symbol)
    {
        await _clientHttp.Trading.CancelAllOrdersAsync(symbol);
    }
    public async Task ClosePositionConcreteOrderBySymbolAsync(string symbol, long orderId)
    {
        await _clientHttp.Trading.CancelOrderAsync(symbol, orderId);
    }
    public async Task CreateBuyLimitOrderAsync(string symbol, TradingBot.Domain.Enums.OrderSide orderSide, decimal quantity, decimal price)
    {
        await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.Limit, quantity, price);
    }
    public async Task<decimal> CreateBuyMarketOrderAsync(string symbol, TradingBot.Domain.Enums.OrderSide orderSide, decimal quantity)
    {
        var res = await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.Market, quantity);
        return res.Data.Price;
    }
    public async Task<long> CreateStopLossOrderAsync(string symbol, TradingBot.Domain.Enums.OrderSide orderSide, decimal price)
    {
        var res = await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.StopMarket, null, price, closePosition:true);
        return res.Data.Id;
    }
    public async Task<long> CreateTakeProfitOrderAsync(string symbol, TradingBot.Domain.Enums.OrderSide orderSide, decimal quantity, decimal price)
    {
        var res = await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.TakeProfitMarket, quantity, price);
        return res.Data.Id;
    }
    /// <summary>
    /// To do
    /// </summary>
    public Task<long> CreateTrailingTakeProfitOrderAsync(string symbol, TradingBot.Domain.Enums.OrderSide orderSide, decimal quantity, decimal price)
    {
        throw new NotImplementedException();
    }
    /// <param name="currencyCode">"USD", "RUB"...</param>
    /// <returns></returns>
    public async Task<decimal> GetAccountBalanceAsync(string currencyCode)
    {
        var balancesGeneralInfo = await _clientHttp.Account.GetBalancesAsync();
        var balance = balancesGeneralInfo.Data.FirstOrDefault(x => x.Asset == currencyCode)?.WalletBalance ?? 0;

        return balance;
    }

    public async Task<decimal> GetPriceAsync(string symbol)
    { 
        var priceBinance = await _clientHttp.ExchangeData.GetPriceAsync(symbol);
        
        return priceBinance.Data.Price;
    }
    

    /// <summary>
    /// Последний candle в списке - самый поздний
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="klineInterval"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Candle>> GetCandlesHistoryAsync(string symbol, Domain.Enums.KlineInterval klineInterval, int limit)
    {
        var klines = await _clientHttp.ExchangeData.GetKlinesAsync(symbol, (KlineInterval)klineInterval, limit: limit);
        
        return klines.Data.Select(kline => new Candle(kline.CloseTime, kline.OpenPrice, kline.HighPrice, kline.LowPrice, kline.ClosePrice));
    }

    /// <summary>
    /// Стакан выглядит как обычный стакан по структуре
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public async Task<Glass> GetGlassAsync(string symbol, int capacity)
    {
        var orderBook = await _clientHttp.ExchangeData.GetOrderBookAsync(symbol, capacity);
        //чтобы была как обычная структура стакана
        orderBook.Data.Asks = orderBook.Data.Asks.Reverse();
        var glass = new Glass(capacity);
        glass.UpdateWholeGlass(orderBook.Data.Bids.ToDictionary(x => x.Price, x => x.Quantity),
            orderBook.Data.Asks.ToDictionary(x => x.Price, x => x.Quantity));
        
        return glass;
    }

    /// <summary>
    /// Шаг цены
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public async Task<decimal> GetPriceStep(string symbol)
    {
        var exchangeInfo = await _clientHttp.ExchangeData.GetExchangeInfoAsync();
        var symbolInfo = exchangeInfo.Data.Symbols.FirstOrDefault(x => x.Name == symbol);
        return symbolInfo!.PriceFilter!.TickSize;
    }

    public Task<decimal> GetFeeMarket()
    {
        return Task.FromResult(Convert.ToDecimal(ConfigurationManager.AppSettings["feeMarketPercentUsdtFururesBinance"]));
    }

    public Task<decimal> GetFeeLimit()
    {
        throw new NotImplementedException();
    }
    public async Task<TradingBot.Domain.Enums.OrderStatus> GetOrderStatus(string symbol, long orderId)
    {
        var webCallResultInfo = await _clientHttp.Trading.GetOrderAsync(symbol, orderId);
        return (TradingBot.Domain.Enums.OrderStatus)webCallResultInfo.Data.Status;
    }
}
