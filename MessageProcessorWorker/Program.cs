using MessageProcessorWorker;
using MessageProcessorWorker.services;
using MessageProcessorWorker.services.IServices;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<MessageWorker>();
builder.Services.AddSingleton<IMessageService, MessageService>();

var host = builder.Build();
host.Run();
