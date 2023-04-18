using System.Configuration;
using TradingBot.Application.Common.Enum;
using TradingBot.Application.Interfaces;

namespace TradingBot.Strategies.PatternsOfExchange.Classes
{
    /// <summary>
    /// Закрывает сделку частями используя стакан
    /// т.е. цену закрытия выбирает на основе стакана
    /// </summary>
    internal class CloseOrderByGlass
    {
        private readonly IExchangeApiClient _exchangeApiClient;
        private readonly string _symbol;
        private readonly OrderSide _orderSide;
        private readonly decimal _quantity;
        private readonly decimal _priceOpenOrder;
        private decimal _priceST = 0m;
        private short _capacityGlass;
        /// <summary>
        /// Шаг цены
        /// </summary>
        private decimal _priceStep;
        public CloseOrderByGlass(IExchangeApiClient exchangeApiClient, string symbol, OrderSide orderSide,
            decimal quantity, decimal priceOpenOrder, short capacityGlass)
        {
            _exchangeApiClient = exchangeApiClient;
            _symbol = symbol;
            _orderSide = orderSide;
            _quantity = quantity;
            _priceOpenOrder = priceOpenOrder;
            _capacityGlass = capacityGlass;
            _priceStep = _exchangeApiClient.GetPriceStep(_symbol).Result;
        }

        public async Task SetStopLossAndTakeProfit()
        {
            await SetStopLoss();
            await CascadeCloseTakeProfit();
        }

        /// <summary>
        /// ST by best glass MINUS one step price
        /// </summary>
        /// <returns></returns>
        private async Task SetStopLoss()
        {
            var glass = await _exchangeApiClient.GetGlassAsync(_symbol, _capacityGlass);
            switch (_orderSide)
            {
                case OrderSide.Buy:
                    {
                        _priceST = glass.GetPriceByBestQuantityInBids();
                        _priceST -= _priceStep;
                        break;
                    }
                case OrderSide.Sell:
                    {
                        _priceST = glass.GetPriceByBestQuantityInAsks();
                        _priceST += _priceStep;
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
            
            await _exchangeApiClient.CreateStopLossOrderAsync(_symbol, _orderSide, _quantity, _priceST);
        }

        private async Task CascadeCloseTakeProfit()
        {
            var fee = await _exchangeApiClient.GetFeeMarket();
            var priceTP = await CloseAtBreakeventHalf(fee);
            await CloseProfitQuarter(priceTP);
        }
        /// <summary>
        /// Закрыть в безубыток с помощью
        /// половины позиции
        /// </summary>
        /// <returns></returns>
        private async Task<decimal> CloseAtBreakeventHalf(decimal fee)
        {
            //_priceOpenOrder*fee/100 который в конце - это приблизительная комиссия за продажу уже с тейк профитом
            decimal zatratiKromeTP = (_priceOpenOrder * fee / 100 + _priceST * fee / 100 + (_priceOpenOrder - _priceST) + _priceOpenOrder * fee / 100) / 2;
            var priceTP = zatratiKromeTP + _priceOpenOrder;
            await _exchangeApiClient.CreateTakeProfitOrderAsync(_symbol, _orderSide, _quantity / 2, priceTP);
            return priceTP;
        }
        private async Task CloseProfitQuarter(decimal priceTP)
        {
            var resPrice = _priceOpenOrder
        }
    }
}
