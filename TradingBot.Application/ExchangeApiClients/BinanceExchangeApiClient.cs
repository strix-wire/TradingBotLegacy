using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradingBot.Application.Interfaces;

internal class BinanceExchangeApiClient : IExchangeApiClient
{
    private readonly BinanceClient _client;

    public BinanceExchangeApiClient(string apiKey, string apiSecret)
    {
        _client = new BinanceClient(new BinanceClientOptions
        {
            ApiCredentials = new BinanceApiCredentials(apiKey, apiSecret)
        });
    }

    public async Task CloseAllPositionsAndOrders()
    {
        var accountInfo = await _client.Spot.AccountInfoAsync();

        foreach (var balance in accountInfo.Data.Balances)
        {
            if (balance.Total > 0)
            {
                var symbol = balance.Asset + "USDT";

                var openOrders = await _client.Spot.Order.GetOpenOrdersAsync(symbol);
                foreach (var openOrder in openOrders.Data)
                {
                    await _client.Spot.Order.CancelOrderAsync(openOrder.OrderId, symbol);
                }

                var position = await _client.Spot.Margin.GetMarginAccountAsync(symbol);
                if (position.Data.Amount > 0)
                {
                    await _client.Spot.Margin.CloseMarginPositionAsync(symbol);
                }
            }
        }
    }

    public async Task CreateBuyLimitOrder(string symbol, decimal quantity, decimal price)
    {
        var order = await _client.Spot.Order.PlaceLimitOrderAsync(symbol + "USDT", OrderSide.Buy, quantity, price);
        if (!order.Success)
            throw new Exception($"Failed to place buy limit order: {order.Error?.Message}");
    }

    public async Task CreateBuyMarketOrder(string symbol, decimal quantity)
    {
        var order = await _client.Spot.Order.PlaceMarketOrderAsync(symbol + "USDT", OrderSide.Buy, quantity);
        if (!order.Success)
            throw new Exception($"Failed to place buy market order: {order.Error?.Message}");
    }

    public async Task CreateStopLossOrder(string symbol, decimal quantity, decimal stopPrice)
    {
        var order = await _client.Spot.Order.PlaceStopLossOrderAsync(symbol + "USDT", OrderSide.Sell, quantity, stopPrice);
        if (!order.Success)
            throw new Exception($"Failed to place stop loss order: {order.Error?.Message}");
    }

    public async Task CreateTakeProfitOrder(string symbol, decimal quantity, decimal takeProfitPrice)
    {
        var order = await _client.Spot.Order.PlaceTakeProfitOrderAsync(symbol + "USDT", OrderSide.Sell, quantity, takeProfitPrice);
        if (!order.Success)
            throw new Exception($"Failed to place take profit order: {order.Error?.Message}");
    }

    public async Task CreateTrailingTakeProfitOrder(string symbol, decimal quantity, decimal trailValue)
    {
        var order = await _client.Spot.Order.PlaceTrailingStopOrderAsync(symbol + "USDT", OrderSide.Sell, quantity, trailValue);
        if (!order.Success)
            throw new Exception($"Failed to place trailing take profit order: {order.Error?.Message}");
    }

    public async Task<decimal> GetAccountBalance()
    {
        var accountInfo = await _client.UsdFuturesApi.Account.GetBalancesAsync();

        var balance = accountInfo.Data.FirstOrDefault(b => b.Asset == "USDT");

        if (balance == null)
            throw new Exception("USDT balance not found");

        return balance.WalletBalance;
    }
}
