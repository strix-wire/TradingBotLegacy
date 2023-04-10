namespace TradingBot.Domain.Classes;

public class Candle
{
    /// <summary>
    /// Время закрытия свечки
    /// </summary>
    public DateTime DateTimeClosed { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }

    public Candle(DateTime dateTime, decimal open, decimal high, decimal low, decimal close)
    {
        DateTimeClosed = dateTime;
        Open = open;
        High = high;
        Low = low;
        Close = close;
    }
    /// <summary>
    /// Возвращает размер тела свечки
    /// </summary>
    public decimal Body => Math.Abs(Open - Close);
    /// <summary>
    /// Размер верхней тени свечки
    /// </summary>
    public decimal UpperWick => High - Math.Max(Open, Close);
    /// <summary>
    /// Размер нижней тени свечки
    /// </summary>
    public decimal LowerWick => Math.Min(Open, Close) - Low;
    /// <summary>
    /// true, если свечка бычья (цена закрытия выше цены открытия)
    /// </summary>
    public bool IsBullish => Close > Open;
    /// <summary>
    /// true, если свечка медвежья (цена закрытия ниже цены открытия)
    /// </summary>
    public bool IsBearish => Close < Open;
}
