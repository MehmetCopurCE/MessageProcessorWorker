using MessageAPI.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageAPI.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public MsgType MessageType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public MsgStatus Status { get; set; } = MsgStatus.Pending;

    }
}
