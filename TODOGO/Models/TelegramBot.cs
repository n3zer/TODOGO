
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace TODOGO
{
    public class TelegramBot
    {
        public static TelegramBot Bot;

        public TelegramBotClient Client;
        public TelegramBot(string token) 
        {
            Client = new TelegramBotClient(token);
        }

        public async Task SendMessageAsync(string name)
        {
            Message message = await Client.SendTextMessageAsync(
                chatId: 739646468,
                text: name, 
                parseMode: ParseMode.Html);
        }
    }
}
