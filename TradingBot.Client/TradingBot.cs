using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Configuration;
using TradingBot.Application.ExchangeApiClients.BinanceApi;
using TradingBot.Application.Interfaces;
using TradingBot.Application.Loggers;
using TradingBot.Application.Loggers.SenderTelegramBot;
using TradingBot.Domain.Enums;
using TradingBot.Strategies.PatternsOfExchange.Classes;
using TradingBot.Strategies.Strategies.LevelAndGlassStrategy;

await RunAsync();

async Task RunAsync()
{
    var services = new ServiceCollection();
    services.AddSingleton<ILoggerFactory, LoggerFactory>();
    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
    services.AddSingleton<ISenderAsync, SenderTelegramBot>();
    services.AddLogging(builder =>
    {
        builder.AddConsole();
    });

    var serviceProvider = services.BuildServiceProvider();

    var senderInfo = serviceProvider.GetRequiredService<ISenderAsync>();

    //1 clientSpot
    var binanceUSDTFuturesLevelAndGlassStrategy = Task.Run(() => RunBinanceUsdtFuturesAsync(senderInfo));

    //2 clientSpot
    

    await Task.WhenAll(binanceUSDTFuturesLevelAndGlassStrategy);
}
async Task RunBinanceUsdtFuturesAsync(ISenderAsync senderTelegram)
{
    //Arrange
    var clientsFactoryBinance = new ClientsFactoryBinance();
    var clientFutures = clientsFactoryBinance.GetClient(TypeBinanceClient.UsdtFutures);
    var levelAndGlassStrategy = new LevelAndGlassStrategy(clientFutures, senderTelegram,
        distanceBetweenTouchingCandlesRequired:3, tolerancePct:0.1m, getHistoryLimitCandles:200, neededCountCandlesIsToleranceWick:3,
        bidsAsksRatio:1.5m);

    var clientSpot = clientsFactoryBinance.GetClient(TypeBinanceClient.UsdtSpot);

    string symbols = ConfigurationManager.AppSettings["symbolsBinanceFurures"];
    string[] symbolArray = symbols.Split(',');
    //Act
    while (true)
    {
        foreach  (var symbol in symbolArray)
        {
            try
            {
                //5 min timeFrame
                await levelAndGlassStrategy.RunAsync(symbol, KlineInterval.FiveMinutes, Wick.Upper, clientSpot);
                await levelAndGlassStrategy.RunAsync(symbol, KlineInterval.FifteenMinutes, Wick.Upper, clientSpot);
                await levelAndGlassStrategy.RunAsync(symbol, KlineInterval.FiveMinutes, Wick.Lower, clientSpot);
                await levelAndGlassStrategy.RunAsync(symbol, KlineInterval.FifteenMinutes, Wick.Lower, clientSpot);
                //if (resbuy == true)
                //    await InDealBinanceUsdtFuturesAsync(symbol, clientFutures, TradingBot.Domain.Enums.OrderSide.Buy, quantity: 0.0001m, capacityGlass: 10);
                await Task.Delay(200);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
                await senderTelegram.SendAsync($"Ошибка! {e.Message}");
            }

        }
    }
}

async Task InDealBinanceUsdtFuturesAsync(string symbol, IExchangeApiClient futuresClient, TradingBot.Domain.Enums.OrderSide orderSide,
    decimal quantity, short capacityGlass)
{
    var priceBuy = await futuresClient.CreateBuyMarketOrderAsync(symbol, orderSide, quantity);
    var closeOrderByGlass = new CloseOrderByGlass(futuresClient, symbol, orderSide, quantity, priceBuy, capacityGlass);
    await closeOrderByGlass.SetStopLossAndTakeProfit();
    await closeOrderByGlass.RunChecker();
}