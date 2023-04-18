using System.Configuration;
using TradingBot.Application.Common.Enum;
using TradingBot.Application.Interfaces;

namespace TradingBot.Strategies.PatternsOfExchange.Classes
{
    /// <summary>
    /// Закрывает сделку частями
    /// </summary>
    internal class CloseOrder
    {
        private readonly IExchangeApiClient _exchangeApiClient;
        private readonly string _symbol;
        private readonly OrderSide _orderSide;
        private readonly decimal _quantity;
        private readonly decimal _priceOpenOrder;
        private decimal _priceST = 0m;
        public CloseOrder(IExchangeApiClient exchangeApiClient, string symbol, OrderSide orderSide, decimal quantity, decimal price) =>
            (_exchangeApiClient, _symbol, _orderSide, _quantity, _priceOpenOrder) = (exchangeApiClient, symbol, orderSide, quantity, price);

        public async Task SetStopLossAndTakeProfit()
        {
            await SetStopLoss();
            await CascadeCloseTakeProfit();
        }

        private async Task SetStopLoss()
        {
            var glass = await _exchangeApiClient.GetGlassAsync(_symbol, 20);
            if (_orderSide == OrderSide.Buy)
                _priceST = glass.GetPriceByBestQuantityInBids();
            if (_orderSide == OrderSide.Sell)
                _priceST = glass.GetPriceByBestQuantityInAsks();

            if (_priceST == 0m)
                throw new NotImplementedException();

            await _exchangeApiClient.CreateStopLossOrderAsync(_symbol, _orderSide, _quantity, _priceST);
        }

        private async Task CascadeCloseTakeProfit()
        {
            var fee = Convert.ToDecimal(ConfigurationManager.AppSettings["feePercentUsdtFururesBinance"]);
            await CloseAtBreakeventHalf(fee);

        }
        /// <summary>
        /// Закрыть в безубыток с помощью
        /// половины позиции
        /// </summary>
        /// <returns></returns>
        private async Task CloseAtBreakeventHalf(decimal fee)
        {
            //_priceOpenOrder*fee/100 который в конце - это приблизительная комиссия за продажу уже с тейк профитом
            decimal zatratiKromeTP = (_priceOpenOrder * fee / 100 + _priceST * fee / 100 + (_priceOpenOrder - _priceST) + _priceOpenOrder * fee / 100) / 2;
            var priceTP = zatratiKromeTP + _priceOpenOrder;
            await _exchangeApiClient.CreateTakeProfitOrderAsync(_symbol, _orderSide, _quantity / 2, priceTP);
        }
        private async Task CloseProfitQuarter(decimal )
        {

        }
    }
}
