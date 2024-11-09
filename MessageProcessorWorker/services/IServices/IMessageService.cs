using MessageAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.services.IServices
{
    public interface IMessageService
    {
        Task<List<Message>> GetPendingMessagesAsync();

        Task UpdateMessageAsync(Message message);
    }
}
