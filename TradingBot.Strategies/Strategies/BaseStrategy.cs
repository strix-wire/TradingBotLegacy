using TradingBot.Application.Interfaces;

namespace TradingBot.Strategies.Strategies;

public abstract class BaseStrategy
{
    protected IExchangeApiClient ExchangeApiClient { get; set; }

    public BaseStrategy(IExchangeApiClient exchangeApiClient)
        => ExchangeApiClient = exchangeApiClient;

    /// <summary>
    /// Запускает работу
    /// </summary>
    public abstract string Run { get; }
}
