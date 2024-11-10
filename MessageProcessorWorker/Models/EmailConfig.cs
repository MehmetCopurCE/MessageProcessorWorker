using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessorWorker.Models
{

    public class EmailConfig
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpSenderMail { get; set; }
        public string SmtpSenderPass { get; set; }
        public string MailReplyTo { get; set; }
        public string MailSubject { get; set; }
        public bool EnableSsl { get; set; }

    }
}
