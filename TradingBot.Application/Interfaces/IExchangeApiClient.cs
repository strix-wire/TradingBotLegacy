namespace TradingBot.Application.Interfaces;

internal interface IExchangeApiClient
{
    Task CloseAllPositionsAndOrders();
    Task CreateBuyLimitOrder();
    Task CreateBuyMarketOrder();
    Task CreateStopLossOrder();
    Task CreateTakeProfitOrder();
    Task CreateTrailingTakeProfitOrder();
    Task<double> GetAccountBalance();
}
