using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Models
{
    public class MessageLocation
    {
        public string Bucket { get; set; }
        public string Key { get; set; }
    }

    public class SendEmailCommand
    {
        public MessageLocation Location { get; set; }
        public DateTime Submitted { get; set; }
    }
}
