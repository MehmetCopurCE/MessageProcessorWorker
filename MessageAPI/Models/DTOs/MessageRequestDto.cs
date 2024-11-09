using MessageAPI.Models.Enums;

namespace MessageAPI.Models.DTOs
{
    public class MessageRequestDto
    {
        public MsgType MsgType { get; set; }

        public string MsgReceiver { get; set; }

        public string Content { get; set; }
        
    }
}
