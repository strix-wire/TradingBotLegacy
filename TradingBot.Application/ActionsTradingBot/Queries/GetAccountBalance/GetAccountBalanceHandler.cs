using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Queries.GetAccountBalance
{
    internal class GetAccountBalanceHandler : IRequestHandler<GetAccountBalanceQuery>
    {
        public async Task Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            await request.ExchangeApiClient.GetAccountBalanceAsync(request.CurrencyCode);
        }
    }
}
