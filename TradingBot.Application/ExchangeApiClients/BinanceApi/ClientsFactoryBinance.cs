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
        (ConfigurationManager.AppSettings["apiKeyBinance"], ConfigurationManager.AppSettings["apiSecretBinance"]) = (_apiKey, _apiSecret);

    public static IExchangeApiClient GetClient(TypeBinanceClient typeBinanceClient)
    {
        switch (typeBinanceClient)
        {
            case TypeBinanceClient.USDFutures:
                return CreateBinanceUSDFutures();
            case TypeBinanceClient.USDSpot:
                return CreateBinanceUSDSpot();
            
            default: { throw new ArgumentException("Invalid factory type"); }
        }
    }
    private static BinanceApiCredentials CreateApiCredentials(string apiKey, string apiSecret)
        => new BinanceApiCredentials(apiKey, apiSecret);

    private static AllBinanceClients CreateBinanceClient()
    {
        var clientHttp = new BinanceClient();
        clientHttp.SetApiCredentials(CreateApiCredentials(_apiKey, _apiSecret));
        var socketClient = new BinanceSocketClient();

        return new AllBinanceClients(clientHttp, socketClient);
    }
    private static BinanceUsdFuturesApiClient CreateBinanceUSDFutures()
    {
        var allBinanceClients = CreateBinanceClient();
        
        return new BinanceUsdFuturesApiClient(allBinanceClients.ClientHttp.UsdFuturesApi,
            allBinanceClients.SocketClient.UsdFuturesStreams);
    }
    private static BinanceUsdSpotApiClient CreateBinanceUSDSpot()
    {
        var allBinanceClients = CreateBinanceClient();
        
        return new BinanceUsdSpotApiClient(allBinanceClients.ClientHttp.SpotApi,
            allBinanceClients.SocketClient.SpotStreams);
    }
}

internal class AllBinanceClients
{
    public AllBinanceClients(BinanceClient client, BinanceSocketClient socketClient)
        => (ClientHttp, SocketClient) = (client, socketClient);

    public BinanceClient ClientHttp { get; set; }
    public BinanceSocketClient SocketClient { get; set; }
}