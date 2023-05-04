using TradingBot.Application.Interfaces;
using TradingBot.Application.Loggers;
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
    private readonly ISenderAsync _senderAsync;
    public LevelAndGlassStrategy(IExchangeApiClient exchangeApiClient, ISenderAsync senderAsync, int distanceBetweenTouchingCandlesRequired,
        decimal tolerancePct, int getHistoryLimitCandles, int neededCountCandlesIsToleranceWick, decimal bidsAsksRatio) : base(exchangeApiClient)
    {
        _distanceBetweenTouchingCandlesRequired = distanceBetweenTouchingCandlesRequired;
        _tolerancePct = tolerancePct;
        _limitCandles = getHistoryLimitCandles;
        _neededCountCandlesIsToleranceWick = neededCountCandlesIsToleranceWick;
        _bidsAsksRatio = bidsAsksRatio;
        _senderAsync = senderAsync;

        _senderAsync.SendAsync($"TradingBot ВКЛючился").Wait();
    }
    public override async Task<bool> RunAsync(string symbol, Domain.Enums.KlineInterval timeFrame, Domain.Enums.Wick wick,
        IExchangeApiClient spotExchangeApiClient = null)
    {
        string addingMsg = $"{DateTime.Now} {symbol}. TimeFrame: {timeFrame}. Wick: {wick}. Price: {await ExchangeApiClient.GetPriceAsync(symbol)}";

        bool cond1 = await GetConditionByCountCandlesIsToleranceWick(symbol, timeFrame, wick);
        if (cond1)
            await _senderAsync.SendAsync($"УРОВЕНЬ == true. " + addingMsg);
        bool cond2 = await GetConditionByRationOnGlass(symbol);
        if (cond2)
            await _senderAsync.SendAsync($"Стакан фьюча == true. " + addingMsg);
        bool cond3 = await GetConditionByRationOnGlassSPOT(symbol, spotExchangeApiClient);
        if (cond3)
            await _senderAsync.SendAsync($"Стакан спота == true. " + addingMsg);
        if (cond1 && cond2 && cond3) 
            return true;

        return false;
    }
    private async Task<bool> GetConditionByCountCandlesIsToleranceWick(string symbol, Domain.Enums.KlineInterval timeFrame, Domain.Enums.Wick wick)
    {
        var candles = await ExchangeApiClient.GetCandlesHistoryAsync(symbol, timeFrame, _limitCandles);
        var levelDetection = new LevelDetection();
        int countCandlesIsToleranceWick = levelDetection.GetCountCandlesInToleranceWick(candles,
            await ExchangeApiClient.GetPriceAsync(symbol), _tolerancePct, wick, _distanceBetweenTouchingCandlesRequired);

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

    ~LevelAndGlassStrategy()
    {
        _senderAsync.SendAsync($"TradingBot зашел в ДЕСТРУКТОР").Wait();
    }
}