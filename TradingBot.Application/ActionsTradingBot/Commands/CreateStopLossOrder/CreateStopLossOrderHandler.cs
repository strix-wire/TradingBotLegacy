using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateStopLossOrder;

internal class CreateStopLossOrderHandler : IRequestHandler<CreateStopLossOrderCommand>
{
    public async Task Handle(CreateStopLossOrderCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.CreateStopLossOrderAsync(request.Symbol, request.OrderSide, request.Price);
    }
}
