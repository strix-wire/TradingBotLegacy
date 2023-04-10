using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTakeProfitOrder;

internal class CreateTakeProfitOrderHandler : IRequestHandler<CreateTakeProfitOrderCommand>
{
    public async Task Handle(CreateTakeProfitOrderCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.CreateTakeProfitOrderAsync(request.Symbol, request.OrderSide, request.Quantity, request.Price);
    }
}
