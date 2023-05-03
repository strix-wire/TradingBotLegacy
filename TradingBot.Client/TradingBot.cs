using System.Configuration;
using TradingBot.Application.ExchangeApiClients.BinanceApi;
using TradingBot.Application.Interfaces;
using TradingBot.Strategies.PatternsOfExchange.Classes;
using TradingBot.Strategies.Strategies.LevelAndGlassStrategy;

await RunAsync();

async Task RunAsync()
{
    //1 clientSpot
    var binanceUSDTFuturesLevelAndGlassStrategy = Task.Run(RunBinanceUsdtFuturesAsync);

    //2 clientSpot
    

    await Task.WhenAll(binanceUSDTFuturesLevelAndGlassStrategy);
}
async Task RunBinanceUsdtFuturesAsync()
{
    //Arrange
    var clientsFactoryBinance = new ClientsFactoryBinance();
    var clientFutures = clientsFactoryBinance.GetClient(TypeBinanceClient.UsdtFutures);
    var levelAndGlassStrategy = new LevelAndGlassStrategy(clientFutures,
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
                var resbuy = await levelAndGlassStrategy.RunAsync(symbol, clientSpot);
                //if (resbuy == true)
                //    await InDealBinanceUsdtFuturesAsync(symbol, clientFutures, TradingBot.Domain.Enums.OrderSide.Buy, quantity: 0.0001m, capacityGlass: 10);
                await Task.Delay(200);
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync(e.Message);
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