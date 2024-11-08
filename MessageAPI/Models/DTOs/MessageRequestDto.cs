using MessageAPI.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageAPI.Models.DTOs
{
    public class MessageRequestDto
    {
        public string Content { get; set; }
        public MsgType MessageType { get; set; }
    }
}
