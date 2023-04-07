namespace TradingBot.Domain;

public class Trade
{
    public Trade(string tradeId, string symbol, decimal volume, decimal price, DateTime openTime)
    {
        TradeId = tradeId;
        Symbol = symbol;
        Volume = volume;
        Price = price;
        OpenTime = openTime;
    }
    public string TradeId { get; set; } // идентификатор сделки
    public string Symbol { get; set; } // символ инструмента, по которому проводилась сделка
    public decimal Volume { get; set; } // объем сделки
    public decimal Price { get; set; } // цена по которой была проведена сделка
    public DateTime OpenTime { get; set; } // время открытия сделки
    public DateTime? CloseTime { get; set; } // время закрытия сделки, null если сделка еще не закрыта
    public decimal Profit { get; set; } // прибыль/убыток по сделке
}
