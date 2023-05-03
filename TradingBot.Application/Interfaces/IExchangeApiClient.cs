using TradingBot.Domain.Classes;
using TradingBot.Domain.Enums;

namespace TradingBot.Application.Interfaces;

public interface IExchangeApiClient
{
    Task ClosePositionAllOrderBySymbolAsync(string symbol);
    Task ClosePositionConcreteOrderBySymbolAsync(string symbol, long orderId);
    Task CreateBuyLimitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderSide"></param>
    /// <param name="quantity"></param>
    /// <returns>price buy</returns>
    Task<decimal> CreateBuyMarketOrderAsync(string symbol, OrderSide orderSide, decimal quantity);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderSide"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    /// <returns>OrderId</returns>
    Task<long> CreateStopLossOrderAsync(string symbol, OrderSide orderSide, decimal price);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderSide"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    /// <returns>OrderId</returns>
    Task<long> CreateTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <param name="orderSide"></param>
    /// <param name="quantity"></param>
    /// <param name="price"></param>
    /// <returns>OrderId</returns>
    Task<long> CreateTrailingTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task<Glass> GetGlassAsync(string symbol, int capacity);
    Task<decimal> GetAccountBalanceAsync(string currencyCode);
    Task<IEnumerable<Candle>> GetCandlesHistoryAsync(string symbol, Domain.Enums.KlineInterval klineInterval, int limit);
    Task<decimal> GetPriceAsync(string symbol);
    Task<decimal> GetPriceStep(string symbol);
    Task<decimal> GetFeeMarket();
    Task<decimal> GetFeeLimit();
    Task<OrderStatus> GetOrderStatus(string symbol, long orderId);
}
