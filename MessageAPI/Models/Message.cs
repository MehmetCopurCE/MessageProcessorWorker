
using MessageAPI.Enums;

namespace MessageAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public MsgType MessageType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

    }
}
