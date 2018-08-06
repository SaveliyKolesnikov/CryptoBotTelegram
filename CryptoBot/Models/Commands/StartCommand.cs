using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoBot.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "start";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            client.SendTextMessageAsync(chatId, "Напиши /rate чтобы увидеть текущий курс криптовалют", replyToMessageId: messageId);
        }
    }
}