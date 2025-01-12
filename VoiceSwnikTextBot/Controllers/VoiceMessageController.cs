using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceSwnikTextBot.Configuration;
using VoiceSwnikTextBot.Services;

namespace VoiceSwnikTextBot.Controllers
{
    internal class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly AppSettings _appSettings;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(AppSettings appSettings, ITelegramBotClient telegramBotClient,
            IFileHandler audioFileHandler)
        {
            _appSettings = appSettings;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _audioFileHandler.Download(fileId, ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение загружено", cancellationToken: ct);
            //await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Получено голосовое сообщение", cancellationToken: ct);
        }
    }
}
