using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyLimitOrder;

internal class CreateBuyLimitOrderHandler : IRequestHandler<CreateBuyLimitOrderCommand>
{
    public async Task Handle(CreateBuyLimitOrderCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.CreateBuyLimitOrderAsync(request.Symbol, request.OrderSide, request.Quantity, request.Price);
    }
}
