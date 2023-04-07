using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyLimitOrder;

internal class CreateBuyLimitOrderHandler : IRequestHandler<CreateBuyLimitOrderCommand>
{
    public Task Handle(CreateBuyLimitOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
