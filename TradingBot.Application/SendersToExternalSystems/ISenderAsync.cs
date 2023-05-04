namespace TradingBot.Application.Loggers;

public interface ISenderAsync
{
    Task SendAsync(string message);
}
