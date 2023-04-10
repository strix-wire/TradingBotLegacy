using TradingBot.Application.Interfaces;

namespace TradingBot.Strategies.Strategies.LevelAndGlassStrategy;

internal class LevelAndGlassStrategy : BaseStrategy
{
    public LevelAndGlassStrategy(IExchangeApiClient exchangeApiClient) : base(exchangeApiClient)
    {
    }

    public override void Run()
    {
        while (true)
        {
            
        }
    }
}
