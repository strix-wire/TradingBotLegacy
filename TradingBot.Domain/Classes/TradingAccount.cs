namespace TradingBot.Domain.Classes;

public class TradingAccount
{
    public TradingAccount(string accountId, string apiKey, string apiSecret)
    {
        AccountId = accountId;
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        Trades = new List<Trade>();
    }
    public string AccountId { get; set; } // идентификатор аккаунта
    public string ApiKey { get; set; } // ключ API для доступа к торговой платформе
    public string ApiSecret { get; set; } // секретный ключ API для доступа к торговой платформе
    public decimal Balance { get; set; } // баланс на аккаунте
    public decimal Equity { get; set; } // сумма всех открытых позиций на аккаунте
    public decimal FreeMargin { get; set; } // доступная для торговли сумма
    public decimal MarginLevel { get; set; } // уровень маржи на аккаунте
    public List<Trade> Trades { get; set; } // список сделок на аккаунте
}
