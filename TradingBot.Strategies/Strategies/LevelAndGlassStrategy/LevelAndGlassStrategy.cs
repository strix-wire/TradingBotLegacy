using TradingBot.Application.Interfaces;
using TradingBot.Strategies.PatternsOfExchange;

namespace TradingBot.Strategies.Strategies.LevelAndGlassStrategy;

internal class LevelAndGlassStrategy : BaseStrategy
{
    public LevelAndGlassStrategy(IExchangeApiClient exchangeApiClient) : base(exchangeApiClient)
    {
    }

    public override async Task RunAsync(string symbol, IExchangeApiClient spotExchangeApiClient = null)
    {
        while (true)
        {
            bool cond1 = await GetConditionByCountCandlesIsToleranceWick(symbol);
            bool cond2 = await GetConditionByRationOnGlass(symbol);
            bool cond3 = await GetConditionByRationOnGlassSPOT(symbol, spotExchangeApiClient);

            if (cond1 && cond2 && cond3) 
            {
                
            }
        }
    }
    private async Task<bool> GetConditionByCountCandlesIsToleranceWick(string symbol)
    {
        var candles = await ExchangeApiClient.GetCandlesHistoryAsync(symbol, Domain.Enums.KlineInterval.FiveMinutes, 30);
        var levelDetection = new LevelDetection();
        int countCandlesIsToleranceWick = levelDetection.GetCountCandlesInToleranceWick(candles,
            await ExchangeApiClient.GetPriceAsync(symbol), 0.1m, Domain.Enums.Wick.Upper, 3);

        return countCandlesIsToleranceWick > 3;
    }

    private async Task<bool> GetConditionByRationOnGlass(string symbol)
    {
        var glass = await ExchangeApiClient.GetGlassAsync(symbol, 10);
        var bidsAsksRatio = new BidsAsksRatio();
        var asksBidsRation = new AsksBidsRatio();

        return bidsAsksRatio.Calculate(glass) > 10 || asksBidsRation.Calculate(glass) > 10;
    }

    private async Task<bool> GetConditionByRationOnGlassSPOT(string symbol, IExchangeApiClient spotExchangeApiClient = null)
    {
        if (spotExchangeApiClient == null)
            return true;
        
        if (spotExchangeApiClient != null)
        {
            var glass = await ExchangeApiClient.GetGlassAsync(symbol, 10);
            var bidsAsksRatio = new BidsAsksRatio();
            var asksBidsRation = new AsksBidsRatio();
            return bidsAsksRatio.Calculate(glass) > 10 || asksBidsRation.Calculate(glass) > 10;
        }

        return false;
    }
}
