using TradingBot.Application.Interfaces;
using TradingBot.Domain.Enums;

namespace TradingBot.Strategies.PatternsOfExchange.Classes;

internal class CheckerStAndTp
{
    private readonly IExchangeApiClient _exchangeApiClient;
    private readonly string _symbol;
    public long OrderIdST { get; set; }
    public long OrderIdTPHalf { get; set; }
    public long OrderIdTPFirstQuarter { get; set; }
    public long OrderIdTPSecondQuarter { get; set; }
    public CheckerStAndTp(IExchangeApiClient exchangeApiClient, string symbol)
        => (_exchangeApiClient, _symbol) = (exchangeApiClient, symbol);
    
    public async Task<bool> Run()
        => await CheckST() || await CheckTP();
    
    private async Task<bool> CheckTP()
    {
        OrderStatus orderStatus = await _exchangeApiClient.GetOrderStatus(_symbol, OrderIdTPSecondQuarter);
        return await IsClosePostionAllOrderAsync(orderStatus);
    }
    private async Task<bool> CheckST()
    {
        OrderStatus orderStatus = await _exchangeApiClient.GetOrderStatus(_symbol, OrderIdST);
        return await IsClosePostionAllOrderAsync(orderStatus);
    }
    private async Task<bool> IsClosePostionAllOrderAsync(OrderStatus orderStatus)
    {
        if (orderStatus != OrderStatus.New)
        {
            await _exchangeApiClient.ClosePositionAllOrderBySymbolAsync(_symbol);
            return true;
        }
        return false;
    }
}
