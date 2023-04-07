using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CloseAllPositionsAndOrders;

internal class CloseAllPositionsAndOrdersHandler : IRequestHandler<CloseAllPositionsAndOrdersCommand>
{
    public Task Handle(CloseAllPositionsAndOrdersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
