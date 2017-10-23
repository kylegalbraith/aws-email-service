using System;

namespace EmailService.Models
{
    public class StoredEmailMessage
    {        
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class StoredEmailMessageWithReceipt
    {
        public string MessageReceiptHandle { get; set; }
        public StoredEmailMessage Message { get; set; }
    }
}
