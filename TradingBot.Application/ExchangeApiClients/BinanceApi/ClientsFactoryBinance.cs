using Binance.Net.Clients;
using Binance.Net.Objects;
using System.Configuration;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ExchangeApiClients.BinanceApi;

public class ClientsFactoryBinance
{
    private static string _apiKey { get; set; }
    private static string _apiSecret { get; set; }
    public ClientsFactoryBinance() => 
        (ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]) = (_apiKey, _apiSecret);

    public static IExchangeApiClient GetFactory(string factoryType)
    {
        switch (factoryType)
        {
            case "USDFutures":
            {
                var clientHttp = new BinanceClient();
                clientHttp.SetApiCredentials(CreateApiCredentials(_apiKey, _apiSecret));
                var socketClient = new BinanceSocketClient();

                return new BinanceUsdFuturesApiClient(clientHttp.UsdFuturesApi, socketClient.UsdFuturesStreams);
            }
            default: { throw new ArgumentException("Invalid factory type"); }
        }
    }
    private static BinanceApiCredentials CreateApiCredentials(string apiKey, string apiSecret)
        => new BinanceApiCredentials(apiKey, apiSecret);
}
