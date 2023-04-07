using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateStopLossOrder;

internal class CreateStopLossOrderHandler : IRequestHandler<CreateStopLossOrderCommand>
{
    public Task Handle(CreateStopLossOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
