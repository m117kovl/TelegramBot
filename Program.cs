using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot
{
    class Program
    {
        private static readonly string Token = "1949690323:AAEHr4gHp3uGYvf_PcO2mHaLTMKW4Dfp8xw";
        private static readonly string[] stic = { "https://cdn.tlgrm.app/stickers/a27/0a5/a270a568-e006-43c0-bd9c-f9534c0fc785/256/6.webp", "https://cdn.tlgrm.app/stickers/a27/0a5/a270a568-e006-43c0-bd9c-f9534c0fc785/256/5.webp", "https://cdn.tlgrm.app/stickers/a27/0a5/a270a568-e006-43c0-bd9c-f9534c0fc785/256/11.webp", "https://cdn.tlgrm.app/stickers/a27/0a5/a270a568-e006-43c0-bd9c-f9534c0fc785/256/9.webp" };
        private static readonly string[] pho = { "https://i.pinimg.com/474x/f2/62/b8/f262b87cc12b5a8fb6a9727401ce7d75.jpg", "https://i.pinimg.com/564x/af/4c/52/af4c5288bafe618e0b8dc7deda544633.jpg", "https://bipbap.ru/wp-content/uploads/2019/07/3ef1f84df73631aa62103e862464a008.jpg", "https://i.pinimg.com/originals/56/7a/4c/567a4cc9870f8d8e3d03362732760e2c.jpg" };
        private static  TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(Token);

            client.StartReceiving();
            client.OnMessage += OnMessageHandler;

            Console.ReadLine();
            client.StopReceiving();
        }

        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            var msg = e.Message; //переменная где будет наше сообщение

            Random rand = new Random();
            int a = rand.Next(0, stic.Length - 1);



            if (msg.Text != null)
            {
                Console.WriteLine($"Пришло сообщение с текстом: {msg.Text}");
                switch (msg.Text)
                {
                    case "Стикер":
                        await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: stic[a],
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons());
                        break;
                    case "Картинка":
                        await client.SendPhotoAsync(
                            chatId: msg.Chat.Id,
                            photo: pho[a],
                            replyMarkup: GetButtons());
                        break;

                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Выберите команду: ", replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>{ new KeyboardButton { Text = "Стикер"}, new KeyboardButton { Text = "Картинка"} },
                }
            };
        }
    }
}