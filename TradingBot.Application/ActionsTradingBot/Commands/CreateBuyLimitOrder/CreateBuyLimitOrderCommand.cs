using MediatR;

namespace TradingBot.Application.ActionsTradingBot.Commands.CreateBuyLimitOrder;

internal class CreateBuyLimitOrderCommand : IRequest
{
    //Тут нужно сделать типо кто вызывает Тинькофф API, Binance API.
    //Может быть СДЕЛАТЬ ОБЩИЙ КЛАСС для этого. Для Всех комманд и наследовать его/реализовывать
}
