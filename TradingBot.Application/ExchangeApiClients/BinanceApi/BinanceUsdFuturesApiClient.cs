using Binance.Net.Enums;
using Binance.Net.Interfaces.Clients.UsdFuturesApi;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ExchangeApiClients.BinanceApi;

internal class BinanceUsdFuturesApiClient : IExchangeApiClient
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
    public async Task CreateBuyLimitOrderAsync(string symbol, Common.Enum.OrderSide orderSide, decimal quantity, decimal price)
    {
        await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.Limit, quantity, price);
    }
    public async Task CreateBuyMarketOrderAsync(string symbol, Common.Enum.OrderSide orderSide, decimal quantity, decimal price)
    {
        await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.Market, quantity, price);
    }
    public async Task CreateStopLossOrderAsync(string symbol, Common.Enum.OrderSide orderSide, decimal quantity, decimal price)
    {
        await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.StopMarket, quantity, price);
    }
    public async Task CreateTakeProfitOrderAsync(string symbol, Common.Enum.OrderSide orderSide, decimal quantity, decimal price)
    {
        await _clientHttp.Trading.PlaceOrderAsync(symbol, (Binance.Net.Enums.OrderSide)orderSide, FuturesOrderType.TakeProfitMarket, quantity, price);
    }
    /// <summary>
    /// To do
    /// </summary>
    public Task CreateTrailingTakeProfitOrderAsync(string symbol, Common.Enum.OrderSide orderSide, decimal quantity, decimal price)
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
}
