using MessageAPI.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageAPI.Models
{
    public class Message
    {
        public int Id { get; set; }

        public MsgType MsgType { get; set; }

        public string MsgReceiver { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public MsgStatus MsgStatus { get; set; } = MsgStatus.Pending;

    }
}
