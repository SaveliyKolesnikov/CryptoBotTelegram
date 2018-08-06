using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CryptoBot.Models.Commands
{
    public class ErrorMessage : Command
    {
        public override string Name => null;

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            client.SendTextMessageAsync(chatId, "Извините, но такой команды не существует.", replyToMessageId: messageId);
        }
    }
}