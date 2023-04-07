using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateTrailingTakeProfitOrder;

internal class CreateTrailingTakeProfitOrderHandler : IRequestHandler<CreateTrailingTakeProfitOrderCommand>
{
    public Task Handle(CreateTrailingTakeProfitOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
