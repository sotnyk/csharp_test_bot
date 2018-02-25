using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
//using Telegram.Bot.Types.InputMessageContents;
using Telegram.Bot.Types.ReplyMarkups;

namespace csharp_test_bot
{
    class Program
    {
        const string BotApiKeyName = "csharp_test_bot_key";
        private static TelegramBotClient Bot;
        private static User _me;

        private static void Initialize()
        {
            string apiKey = Environment.GetEnvironmentVariable(BotApiKeyName);
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine($"Could not find environment variable with API token: " + BotApiKeyName);
                Console.WriteLine("Please, enter it with proper value and run app again.");
                throw new ArgumentException($"Environment variable '{BotApiKeyName}' not found or empty.");
            }

            Bot = new TelegramBotClient(apiKey);

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            _me = Bot.GetMeAsync().Result;

            Console.Title = _me.Username;
        }

        static void Main(string[] args)
        {
            Initialize();

            Bot.StartReceiving();
            Console.WriteLine($"Start listening for @{_me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            Console.WriteLine($"MessageReceived: Type={message.Type}, From='{message.From.Id}', Text='{message.Text}', ");

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First())
            {
                // send inline keyboard
                case "/help":
                    const string usage = @"Usage:
/help     - usage help
/start    - greeting message
some text - returns flipped ones (ʇxǝʇ ǝɯos)
";

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        usage,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;

                case "/start":
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Hello! I'm chars flipper bot (ʇoq ɹǝddılɟ sɹɐɥɔ).",
                        replyMarkup: new ReplyKeyboardRemove());
                    break;

                default:
                    var answer = CharsFlipper.Flip(message.Text);

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        answer,
                        replyMarkup: new ReplyKeyboardRemove());
                    break;
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}

