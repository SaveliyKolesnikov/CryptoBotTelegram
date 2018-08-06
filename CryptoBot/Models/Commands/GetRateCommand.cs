using CryptoBot.Models.Parse;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoBot.Models.Commands
{
    public class GetRateCommand : Command
    {
        public override string Name => "rate";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var reply = CoinsInfo.Instance.CreateMessage();

            client.SendTextMessageAsync(chatId, reply, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
        }
    }
}