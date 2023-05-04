using TradingBot.Domain.Classes;
using TradingBot.Domain.Enums;

namespace TradingBot.Strategies.PatternsOfExchange.Classes;

internal class LevelDetection
{
    /// <summary>
    /// Получает количество свечек в заданной окрестности. Определение по тени
    /// </summary>
    /// <param name="candles"></param>
    /// <param name="resistanceLevelPrice">уровень цены, относительно к-го окрестность</param>
    /// <param name="tolerancePct">Окрестность(точность) в %</param>
    /// <param name="wick">Уровень по верхней тени или по нижней смотреть</param>
    /// <param name="distanceBetweenTouchingCandlesRequired">меньше или равно количество свечек свечек которое должны быть между свечками,
    /// которые находятся в нужной окрестности</param>
    /// <returns></returns>
    public int GetCountCandlesInToleranceWick(IEnumerable<Candle> candles, decimal resistanceLevelPrice, decimal tolerancePct,
        Wick wick, int distanceBetweenTouchingCandlesRequired)
    {
        decimal tolerance = tolerancePct / 100;
        decimal lowerBound = resistanceLevelPrice * (1 - tolerance);
        decimal upperBound = resistanceLevelPrice * (1 + tolerance);
        var res = IsHighestOrLowestBound(wick, candles, lowerBound, upperBound);
        if (!res)
            return 0;

        return GetCountCandlesInBoundsByWick(candles, wick, lowerBound, upperBound, distanceBetweenTouchingCandlesRequired);
    }

    /// <summary>
    /// Проверка, что за весь переданный список свеч не было цены выше/ниже, чем Upper/Lower
    /// </summary>
    /// <returns></returns>
    private static bool IsHighestOrLowestBound(Wick wick, IEnumerable<Candle> candles, decimal lowerBound, decimal upperBound)
    {
        if (wick == Wick.Upper)
            return candles.FirstOrDefault(x => x.High > upperBound)==null ? true : false;

        if (wick == Wick.Lower)
            return candles.FirstOrDefault(x => x.Low < lowerBound) == null ? true : false;

        return false;
        
    }

    private static int GetCountCandlesInBoundsByWick(IEnumerable<Candle> candles, Wick wick, decimal lowerBound, decimal upperBound,
        int distanceBetweenTouchingCandlesRequired)
    {
        //Количество теней свечек цен в границах
        int countWickCandlesInTolerance = 0;
        int distanceBetweenTouchingCandles = 0;
        Func<Candle, bool> isInTolerance = wick switch
        {
            Wick.Upper => c => c.High >= lowerBound && c.High <= upperBound,
            Wick.Lower => c => c.Low >= lowerBound && c.Low <= upperBound,
            _ => throw new ArgumentException("Invalid PositionSide specified."),
        };
        bool isToleranceHotyabiOneRaz = false;
        foreach (var candle in candles)
        {
            if (isToleranceHotyabiOneRaz && distanceBetweenTouchingCandles > distanceBetweenTouchingCandlesRequired)
            {
                distanceBetweenTouchingCandles++;
                continue;
            }

            if (isInTolerance(candle))
            {
                countWickCandlesInTolerance++;
                distanceBetweenTouchingCandles = 0;
            }

            distanceBetweenTouchingCandles++;
        }

        return countWickCandlesInTolerance;
    }
}
