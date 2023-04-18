namespace TradingBot.Domain.Classes;

public class Glass
{
    //key - price, value - quantity
    private Dictionary<decimal, decimal> _bids; // хранение заявок на покупку
    private Dictionary<decimal, decimal> _asks; // хранение заявок на продажу

    public Glass(int capacity)
    {
        _bids = new Dictionary<decimal, decimal>(capacity);
        _asks = new Dictionary<decimal, decimal>(capacity);
    }

    public void UpdateWholeGlass(Dictionary<decimal, decimal> bids, Dictionary<decimal, decimal> asks)
    {
        _bids = bids;
        _asks = asks;
    }
    public void UpdateBid(decimal price, decimal quantity)
    {
        if (quantity == 0)
            _bids.Remove(price);  
        else
            _bids[price] = quantity;
    }
    public void UpdateAsk(decimal price, decimal quantity)
    {
        if (quantity == 0)
            _asks.Remove(price);
        else
            _asks[price] = quantity;
    }
    public decimal GetBestBid()
    {
        if (_bids.Count == 0)
            return 0;
        else
            return _bids.Keys.Max();
    }
    public decimal GetPriceByBestQuantityInBids()
    {
        var quantityMax = _bids.Values.Max();
        return _bids.FirstOrDefault(x=>x.Value == quantityMax).Key;
    }
    public decimal GetPriceByBestQuantityInAsks()
    {
        var quantityMax = _asks.Values.Max();
        return _asks.FirstOrDefault(x => x.Value == quantityMax).Key;
    }
    public decimal GetBestAsk()
    {
        if (_asks.Count == 0)
            return 0;
        else
            return _asks.Keys.Min();
    }
    public decimal GetBidQuantity(decimal price)
    {
        if (_bids.ContainsKey(price))
            return _bids[price];
        else
            return 0;
    }
    public decimal GetAskQuantity(decimal price)
    {
        if (_asks.ContainsKey(price))
            return _asks[price];
        else
            return 0;
    }
    public Dictionary<decimal, decimal> GetWholeAsks()
        => _asks;
    public Dictionary<decimal, decimal> GetWholeBids()
        => _bids;
}
