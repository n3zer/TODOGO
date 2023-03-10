using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


namespace TODOGO
{
    public class TelegramBot
    {
        public static TelegramBot Bot;

        public TelegramBotClient Client;



        private string _id;
        CancellationTokenSource cts = new ();
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
        };

        public TelegramBot(string token, string id) 
        {
            _id = id;
            Client = new TelegramBotClient(token);
            Client.StartReceiving(
                updateHandler: SendId,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
                );
        }

        private async Task SendId(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;
            var chatId = message.Chat.Id;
            if (chatId.ToString() != _id)
            {
                Message sentMessage = await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "User id:\n" + chatId,
                                    cancellationToken: cancellationToken);
            }
        }

        
        public async Task SendMessageAsync(TaskViewModel tv)
        {
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "✅", callbackData:"✅"),
            });
            Message message = await Client.SendTextMessageAsync(
                chatId: 739646468,
                text: "Задание:"+"\n"+ tv.Name + "\n"+ tv.Description, 
                parseMode: ParseMode.Html,
                replyMarkup: inlineKeyboard);
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
