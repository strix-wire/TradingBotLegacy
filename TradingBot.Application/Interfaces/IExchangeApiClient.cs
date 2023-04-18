using TradingBot.Application.Common.Enum;
using TradingBot.Domain.Classes;

namespace TradingBot.Application.Interfaces;

public interface IExchangeApiClient
{
    Task ClosePositionAllOrderBySymbolAsync(string symbol);
    Task ClosePositionConcreteOrderBySymbolAsync(string symbol, long orderId);
    Task CreateBuyLimitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task CreateBuyMarketOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task CreateStopLossOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task CreateTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task CreateTrailingTakeProfitOrderAsync(string symbol, OrderSide orderSide, decimal quantity, decimal price);
    Task<Glass> GetGlassAsync(string symbol, int capacity);
    Task<decimal> GetAccountBalanceAsync(string currencyCode);
    Task<IEnumerable<Candle>> GetCandlesHistoryAsync(string symbol, Domain.Enums.KlineInterval klineInterval, int limit);
    Task<decimal> GetPriceAsync(string symbol);
    Task<decimal> GetPriceStep(string symbol);
    Task<decimal> GetFeeMarket();
    Task<decimal> GetFeeLimit();
}
