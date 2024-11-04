using MessageCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MessageCore.Models
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
