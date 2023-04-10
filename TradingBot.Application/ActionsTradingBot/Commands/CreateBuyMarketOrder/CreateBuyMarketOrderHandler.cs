using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyMarketOrder;

internal class CreateBuyMarketOrderHandler : IRequestHandler<CreateBuyMarketOrderCommand>
{
    public async Task Handle(CreateBuyMarketOrderCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.CreateBuyMarketOrderAsync(request.Symbol, request.OrderSide, request.Quantity, request.Price);
    }
}
