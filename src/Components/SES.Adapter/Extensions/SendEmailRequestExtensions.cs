using Amazon.SimpleEmail.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SES.Adapter.Extensions
{
    public static class SendEmailRequestExtensions
    {
        public static SendEmailRequest CreateRequest(string sender, string recipient, string subject, string body)
        {
            return new SendEmailRequest()
            {
                Source = sender,
                Destination = CreateDestination(recipient),
                Message = CreateMessage(body, subject)
            };
        }

        private static Message CreateMessage(string body, string subject)
        {
            return new Message()
            {
                Body = new Body() { Text = CreateContent(body) },
                Subject = CreateContent(subject)
            };
        }
        
        private static Destination CreateDestination(string recipient)
        {
            return new Destination()
            {
                ToAddresses = new List<string> { recipient }
            };
        }

        private static Content CreateContent(string content)
        {
            return new Content()
            {
                Data = content
            };
        }
    }
}
