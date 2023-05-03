using TradingBot.Domain.Classes;

namespace TradingBot.Strategies.PatternsOfExchange.Classes
{
    internal class BidsAsksRatio
    {
        private const decimal DEFAULT_RATIO = 1.0m; // значение по умолчанию для случаев, когда нет данных

        public decimal Calculate(Glass glass)
        {
            decimal bidsTotal = glass.GetWholeBids().Keys.Sum();
            decimal asksTotal = glass.GetWholeAsks().Keys.Sum();

            return asksTotal > 0 ? bidsTotal / asksTotal : DEFAULT_RATIO;
        }
    }
    internal class AsksBidsRatio
    {
        private const decimal DEFAULT_RATIO = 1.0m; // значение по умолчанию для случаев, когда нет данных

        public decimal Calculate(Glass glass)
        {
            decimal bidsTotal = glass.GetWholeAsks().Keys.Sum();
            decimal asksTotal = glass.GetWholeAsks().Keys.Sum();

            return asksTotal > 0 ? asksTotal / bidsTotal : DEFAULT_RATIO;
        }
    }
}
