using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CloseAllPositionsAndOrders;

internal class ClosePositionAllOrderBySymbolHandler : IRequestHandler<ClosePositionAllOrderBySymbolCommand>
{
    public async Task Handle(ClosePositionAllOrderBySymbolCommand request, CancellationToken cancellationToken)
    {
        await request.ExchangeApiClient.ClosePositionAllOrderBySymbolAsync(request.Symbol);
    }
}
