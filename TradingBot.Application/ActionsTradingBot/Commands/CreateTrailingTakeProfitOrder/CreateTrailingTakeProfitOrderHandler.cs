using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTrailingTakeProfitOrder;

internal class CreateTrailingTakeProfitOrderHandler : IRequestHandler<CreateTrailingTakeProfitOrderCommand>
{
    public async Task Handle(CreateTrailingTakeProfitOrderCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.CreateTrailingTakeProfitOrderAsync(request.Symbol, request.OrderSide, request.Quantity, request.Price);
    }
}
