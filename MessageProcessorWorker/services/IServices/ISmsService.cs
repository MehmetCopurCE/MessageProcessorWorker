using MessageAPI.Models;
using MessageAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.services.IServices
{
    public interface ISmsService
    {
        Task SendXMLSmsAsync(Message message);

        string CreateXmlData(Message message);

        Task<string> SendHttpRequestAsync(string xmlData);

        Task UpdateMsgStatusAsync(Message message, MsgStatus msgStatus);

    }
}
