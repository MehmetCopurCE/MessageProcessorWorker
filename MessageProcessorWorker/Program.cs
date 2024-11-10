using MessageProcessorWorker;
using MessageProcessorWorker.Models;
using MessageProcessorWorker.services;
using MessageProcessorWorker.services.IServices;
using Serilog;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // NetGsmConfig ve NetGsmConfig modelini DI konteynýrýna ekle
        services.Configure<EmailConfig>(hostContext.Configuration.GetSection("EmailConfig"));
        services.Configure<NetGsmConfig>(hostContext.Configuration.GetSection("NetGsmConfig"));


        services.AddHostedService<MessageWorker>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddTransient<ISmsService, SmsService>();
    })
    .UseSerilog((hostContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .WriteTo.Console() // Konsola da log yazmak için (isteðe baðlý)
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
    });

var host = builder.Build();
await host.RunAsync();
