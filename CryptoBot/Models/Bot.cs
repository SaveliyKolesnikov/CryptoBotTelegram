using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using CryptoBot.Models.Commands;

namespace CryptoBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient _client;
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (!(_client is null))
            {
                return _client;
            }

            //TODO: Add more commands
            _commandsList = new List<Command>
            {
                new StartCommand(),
                new GetRateCommand()
            };

            _client = new TelegramBotClient(AppSettings.Key);
            var hook = string.Format(AppSettings.Url, "api/message/update");
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}