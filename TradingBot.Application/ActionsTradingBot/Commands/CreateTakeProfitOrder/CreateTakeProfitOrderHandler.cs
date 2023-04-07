using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTakeProfitOrder;

internal class CreateTakeProfitOrderHandler : IRequestHandler<CreateTakeProfitOrderCommand>
{
    public Task Handle(CreateTakeProfitOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
