using MessageAPI.Models.Enums;
using MessageProcessorWorker.services;
using MessageProcessorWorker.services.IServices;

namespace MessageProcessorWorker
{
    public sealed class MessageWorker(
        IMessageService messageService,
        ILogger<MessageWorker> logger            
        //IEmailService emailService,
        //ISmsService smsService,
        //INotificationService notificationService
            ) : BackgroundService
    {
        private readonly IMessageService _messageService = messageService;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Pending status'undaki mesajları çekiyoruz
                    var messages = await _messageService.GetPendingMessagesAsync();

                    foreach (var message in messages)
                    {
                        
                        try
                        {
                            logger.LogInformation("Processing message {MessageId}", message.Id);

                            // Mesaj türüne göre işleme yapıyoruz
                            switch (message.MsgType)
                            {
                                case MsgType.Email:
                                    //await _emailService.SendEmailAsync(message.Content);
                                    break;
                                case MsgType.Sms:
                                    //await _smsService.SendSmsAsync(message.Content);
                                    break;
                                case MsgType.Notification:
                                    //await _notificationService.SendNotificationAsync(message.Content);
                                    break;
                            }

                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine($"Mesaj işlenirken hata oluştu: {ex.Message}");

                        }
                    }

                    // Her 10 -> 10000 saniyede bir tekrar dene
                    await Task.Delay(5000, stoppingToken);
                }

            }
            catch (OperationCanceledException)
            {
                // When the stopping token is canceled, for example, a call made from services.msc,
                // we shouldn't exit with a non-zero exit code. In other words, this is expected...
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);
            }
        }
    }

}
