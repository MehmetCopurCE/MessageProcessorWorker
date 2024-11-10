using MessageAPI.Models;
using MessageAPI.Models.Enums;
using MessageProcessorWorker.ErrorHandling.ErrorMessages;
using MessageProcessorWorker.Models;
using MessageProcessorWorker.services.IServices;
using Microsoft.Extensions.Options;
using System.Text;

namespace MessageProcessorWorker.services
{
    public class SmsService(IOptions<NetGsmConfig> netGsmConfig, ILogger<SmsService> logger, IMessageService messageService) : ISmsService
    {
        private readonly ILogger<SmsService> _logger = logger;
        private readonly NetGsmConfig _netGsmConfig = netGsmConfig.Value;
        private readonly IMessageService _messageService = messageService;

        public async Task SendXMLSmsAsync(Message message)
        {
            try
            {
                string xmlData = CreateXmlData(message);

                if (!string.IsNullOrEmpty(xmlData))
                {
                    string response = await SendHttpRequestAsync(xmlData);

                    if (!string.IsNullOrEmpty(response))
                    {
                        var errorModel = ErrorMessages.ErrorList.FirstOrDefault(e => e.Code == response);
                        bool isSuccess = true;


                        if (errorModel != null)
                        {
                            _logger.LogError("Sending SMS failed. Error Code: " + errorModel.Code + " Error Description: " + errorModel.Description);

                            isSuccess = false;
                        }

                        if (isSuccess)
                        {
                            _logger.LogInformation("SMS sent successfully.");
                        }

                       
                        await UpdateMsgStatusAsync(message, isSuccess ? MsgStatus.Success : MsgStatus.Failed);

            
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error sending SMS", ex);

                await UpdateMsgStatusAsync(message, MsgStatus.Failed);

            }
        }


        public string CreateXmlData(Message message)
        {
            try
            {
                var xmlBuilder = new StringBuilder();
                xmlBuilder.Append("<?xml version='1.0' encoding='UTF-8'?>");
                xmlBuilder.Append("<mainbody>");
                xmlBuilder.Append("<header>");
                xmlBuilder.Append("<company dil='TR'>Envest</company>");
                xmlBuilder.Append($"<usercode>{_netGsmConfig.NetGsmUser}</usercode>");
                xmlBuilder.Append($"<password>{_netGsmConfig.NetGsmPassword}</password>");
                xmlBuilder.Append("<type>1:n</type>");
                xmlBuilder.Append($"<msgheader>{_netGsmConfig.NetGsmHeader}</msgheader>");
                xmlBuilder.Append("<appkey>xxx</appkey>");
                xmlBuilder.Append("</header>");
                xmlBuilder.Append("<body>");
                xmlBuilder.Append("<msg><![CDATA[").Append(message.Content).Append("]]></msg>");
                xmlBuilder.Append($"<no>{message.MsgReceiver}</no>");
                xmlBuilder.Append("</body>");
                xmlBuilder.Append("</mainbody>");

                return xmlBuilder.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error creating xml data", ex);
                return string.Empty;
            }

        }

        public async Task<string> SendHttpRequestAsync(string xmlData)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(xmlData, Encoding.UTF8, "application/x-www-form-urlencoded");
                    var response = await client.PostAsync(_netGsmConfig.NetGsmXMLPostAdress, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        _logger.LogError($"HTTP request failed with status code: {response.StatusCode}", new Exception($"HTTP request failed with status code: {response.StatusCode}"));
                        return string.Empty;
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError("An error occurred during the HTTP request (HttpRequestException).", httpEx);
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occurred during the HTTP request.", ex);
                return string.Empty;
            }
        }

        public async Task UpdateMsgStatusAsync(Message message, MsgStatus msgStatus)
        {
            try
            {

                await _messageService.UpdateMessageStatusAsync(message, msgStatus);
                _logger.LogInformation("Message status updated successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError("Error updating message status", ex);
            }
        }

    }
}
