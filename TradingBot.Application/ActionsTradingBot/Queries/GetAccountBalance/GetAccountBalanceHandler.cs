using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Queries.GetAccountBalance
{
    internal class GetAccountBalanceHandler : IRequestHandler<GetAccountBalanceQuery>
    {
        public Task Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
