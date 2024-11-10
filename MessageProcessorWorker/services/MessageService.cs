using MessageAPI.Models;
using MessageProcessorWorker.services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql; // Npgsql bağlantı sınıfı
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MessageAPI.Models.Enums;

namespace MessageProcessorWorker.services
{
    public class MessageService(IConfiguration configuration, ILogger<MessageService> logger) : IMessageService
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");
        private readonly ILogger<MessageService> _logger = logger;

        public async Task<List<Message>> GetPendingMessagesAsync()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();
                    var messages = await connection.QueryAsync<Message>("select * from public.\"Messages\" where \"MsgStatus\" = 'Pending';");
                    return messages.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting pending messages", ex);
                return new List<Message>(); ;
            }
        }

        public async Task UpdateMessageStatusAsync(Message message, MsgStatus msgStatus)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    // Enum'u string olarak geçiriyoruz
                    await connection.ExecuteAsync(
                        "UPDATE public.\"Messages\" SET \"MsgStatus\" = @msgStatus WHERE \"Id\" = @Id;",
                        new { msgStatus = msgStatus.ToString(), message.Id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating message status", ex);
            }
        }

    }
}
