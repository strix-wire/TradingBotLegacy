using Microsoft.Extensions.Logging;
using System.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TradingBot.Application.Loggers.SenderTelegramBot;

public class SenderTelegramBot : ISenderAsync
{
    /// <summary>
    /// Токен доступа бота
    /// </summary>
    private readonly string _groupChatId;
    private readonly TelegramBotClient _botClient;
    private readonly ILogger<SenderTelegramBot> _logger;

    public SenderTelegramBot(ILogger<SenderTelegramBot> logger)
    {
        _logger = logger;
        _groupChatId = ConfigurationManager.AppSettings["groupChatIdTelegramBot"];
        _botClient = new(ConfigurationManager.AppSettings["accessTokenTelegramBot"]);

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = { } },
            new CancellationTokenSource().Token);

        _logger.LogInformation("SenderTelegramBot", "Telegram bot started successfully");
    }
    public async Task SendAsync(string message)
        => await SendTextMessageAsync(message);
    
    private async Task<Message> SendTextMessageAsync(string message)
    {
        try
        {
            var escapedString = EscapingCharactersForTelegram(message);

            using CancellationTokenSource cts = new();

            return await _botClient.SendTextMessageAsync(
                chatId: _groupChatId,
                text: escapedString,
                parseMode: ParseMode.MarkdownV2,
                disableNotification: true,
                cancellationToken: cts.Token);
        }
        catch (Exception e)
        {
            _logger.LogError("SendTextMessageAsync", $"Exception! При отправке в телеграмм message: {message}. Exception: {e}");
            return new Message();
        }
    }

    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        => _logger.LogInformation("HandleErrorAsync", Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Type != UpdateType.Message)
                return;
            var message = update.Message;
            if (message.Text is null)
                return;

            switch (message.Text.ToLower())
            {
                case "h cmd":
                    await GetAllCommand();
                    break;
                case "test":
                    await SendTextMessageAsync("TradingBot работает!");
                    break;
            }
        }
        catch (Exception e)
        {
            await SendTextMessageAsync("Возникла ошибка: " + e.Message);
            _logger.LogError("SendNotification", $"Exception! При отправке в телеграмм");
            _logger.LogError("SendNotification", e.ToString());
            return;
        }
    }

    private async Task GetAllCommand()
        => await SendTextMessageAsync("Пока нет комманд");
    
    /// <summary>
    /// Нельзя передавать некоторые символы в ТГ без экранирования
    /// </summary>
    /// <param name="message"></param>
    private string EscapingCharactersForTelegram(string message)
    {
        var escapedString = message.Replace("(", "\\(");
        escapedString = escapedString.Replace(")", "\\)");
        escapedString = escapedString.Replace("!", "\\!");
        escapedString = escapedString.Replace("-", "\\-");
        escapedString = escapedString.Replace(".", "\\.");
        escapedString = escapedString.Replace("=", "\\=");

        return escapedString;
    }
}
