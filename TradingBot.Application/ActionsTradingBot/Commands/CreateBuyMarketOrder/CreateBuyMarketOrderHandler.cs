using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyMarketOrder;

internal class CreateBuyMarketOrderHandler : IRequestHandler<CreateBuyMarketOrderCommand>
{
    public Task Handle(CreateBuyMarketOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
