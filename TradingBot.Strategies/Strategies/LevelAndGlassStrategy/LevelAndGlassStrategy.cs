using TradingBot.Application.Interfaces;
using TradingBot.Domain.Enums;
using TradingBot.Strategies.PatternsOfExchange.Classes;

namespace TradingBot.Strategies.Strategies.LevelAndGlassStrategy;

public class LevelAndGlassStrategy : BaseStrategy
{
    /// <summary>
    /// меньше или равно количество свечек свечек которое должны быть между свечками,
    /// которые находятся в нужной окрестности
    /// </summary>
    private readonly int _distanceBetweenTouchingCandlesRequired;
    /// <summary>
    /// Окрестность(точность) в %
    /// </summary>
    private readonly decimal _tolerancePct;
    private readonly int _limitCandles;
    private readonly int _neededCountCandlesIsToleranceWick;
    private readonly decimal _bidsAsksRatio;
    public LevelAndGlassStrategy(IExchangeApiClient exchangeApiClient, int distanceBetweenTouchingCandlesRequired,
        decimal tolerancePct, int getHistoryLimitCandles, int neededCountCandlesIsToleranceWick, decimal bidsAsksRatio) : base(exchangeApiClient)
    {
        _distanceBetweenTouchingCandlesRequired = distanceBetweenTouchingCandlesRequired;
        _tolerancePct = tolerancePct;
        _limitCandles = getHistoryLimitCandles;
        _neededCountCandlesIsToleranceWick = neededCountCandlesIsToleranceWick;
        _bidsAsksRatio = bidsAsksRatio;
    }
    public override async Task<bool> RunAsync(string symbol, IExchangeApiClient spotExchangeApiClient = null)
    {
        bool cond1 = await GetConditionByCountCandlesIsToleranceWick(symbol);
        if (cond1)
            await Console.Out.WriteLineAsync($"cond1 == true. {DateTime.Now} {symbol}");
        bool cond2 = await GetConditionByRationOnGlass(symbol);
        if (cond2)
            await Console.Out.WriteLineAsync($"cond2 == true. {DateTime.Now} {symbol}");
        bool cond3 = await GetConditionByRationOnGlassSPOT(symbol, spotExchangeApiClient);
        if (cond3)
            await Console.Out.WriteLineAsync($"cond3 == true. {DateTime.Now} {symbol}");

        if (cond1 && cond2 && cond3) 
            return true;

        return false;
    }
    private async Task<bool> GetConditionByCountCandlesIsToleranceWick(string symbol)
    {
        var candles = await ExchangeApiClient.GetCandlesHistoryAsync(symbol, Domain.Enums.KlineInterval.FiveMinutes, _limitCandles);
        var levelDetection = new LevelDetection();
        int countCandlesIsToleranceWick = levelDetection.GetCountCandlesInToleranceWick(candles,
            await ExchangeApiClient.GetPriceAsync(symbol), _tolerancePct, Domain.Enums.Wick.Upper, _distanceBetweenTouchingCandlesRequired);

        return countCandlesIsToleranceWick > _neededCountCandlesIsToleranceWick;
    }

    private async Task<bool> GetConditionByRationOnGlass(string symbol)
    {
        var glass = await ExchangeApiClient.GetGlassAsync(symbol, 10);
        var bidsAsksRatio = new BidsAsksRatio();
        var asksBidsRation = new AsksBidsRatio();

        return bidsAsksRatio.Calculate(glass) > _bidsAsksRatio || asksBidsRation.Calculate(glass) > _bidsAsksRatio;
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
            return bidsAsksRatio.Calculate(glass) > _bidsAsksRatio || asksBidsRation.Calculate(glass) > _bidsAsksRatio;
        }

        return false;
    }
}
