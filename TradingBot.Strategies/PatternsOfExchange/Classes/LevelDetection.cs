using TradingBot.Domain.Classes;
using TradingBot.Domain.Enums;

namespace TradingBot.Strategies.PatternsOfExchange.Classes;

internal class LevelDetection
{
    /// <summary>
    /// Получает количество свечек в заданной окрестности. Определение по тени
    /// </summary>
    /// <param name="candles"></param>
    /// <param name="resistanceLevel">уровень цены, относительно к-го окрестность</param>
    /// <param name="tolerancePct">Окрестность(точность) в %</param>
    /// <param name="wick">Уровень по верхней тени или по нижней смотреть</param>
    /// <param name="distanceBetweenTouchingCandlesRequired">меньше или равно количество свечек свечек которое должны быть между свечками,
    /// которые находятся в нужной окрестности</param>
    /// <returns></returns>
    public int GetCountCandlesInToleranceWick(IEnumerable<Candle> candles, decimal resistanceLevel, decimal tolerancePct,
        Wick wick, int distanceBetweenTouchingCandlesRequired)
    {
        decimal tolerance = tolerancePct / 100;
        decimal lowerBound = resistanceLevel * (1 - tolerance);
        decimal upperBound = resistanceLevel * (1 + tolerance);
        return GetCountCandlesInBoundsByWick(candles, wick, lowerBound, upperBound, distanceBetweenTouchingCandlesRequired);
    }

    private static int GetCountCandlesInBoundsByWick(IEnumerable<Candle> candles, Wick wick, decimal lowerBound, decimal upperBound,
        int distanceBetweenTouchingCandlesRequired)
    {
        //Количество теней свечек цен в границах
        int countWickCandlesInTolerance = 0;
        int distanceBetweenTouchingCandles = 0;
        Func<Candle, bool> isInTolerance = wick switch
        {
            Wick.Upper => c => c.UpperWick >= lowerBound && c.UpperWick <= upperBound,
            Wick.Lower => c => c.LowerWick >= lowerBound && c.LowerWick <= upperBound,
            _ => throw new ArgumentException("Invalid PositionSide specified."),
        };
        foreach (var candle in candles)
        {
            if (distanceBetweenTouchingCandles < distanceBetweenTouchingCandlesRequired)
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
