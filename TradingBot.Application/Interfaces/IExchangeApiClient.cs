using TradingBot.Application.Common.Enum;

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
    Task<decimal> GetAccountBalanceAsync(string currencyCode);
}
