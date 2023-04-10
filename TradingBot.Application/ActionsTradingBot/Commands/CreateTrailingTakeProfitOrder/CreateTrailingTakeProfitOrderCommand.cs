using MediatR;
using TradingBot.Application.Common.Enum;
using TradingBot.Application.Interfaces;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTrailingTakeProfitOrder;

internal class CreateTrailingTakeProfitOrderCommand : IRequest
{
    public CreateTrailingTakeProfitOrderCommand(IExchangeApiClient exchangeApiClient, string symbol,
        OrderSide orderSide, decimal quantity, decimal price) =>
            (ExchangeApiClient, Symbol, OrderSide, Quantity, Price) = (exchangeApiClient, symbol, orderSide, quantity, price);
    public IExchangeApiClient ExchangeApiClient { get; init; }
    public string Symbol { get; set; }
    public OrderSide OrderSide { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}
