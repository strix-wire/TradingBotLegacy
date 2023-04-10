using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.ClosePositionConcreteOrderBySymbol;

internal class ClosePositionConcreteOrderBySymbolHandler : IRequestHandler<ClosePositionConcreteOrderBySymbolCommand>
{
    public async Task Handle(ClosePositionConcreteOrderBySymbolCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.ClosePositionConcreteOrderBySymbolAsync(request.Symbol, request.OrderId);
    }
}
