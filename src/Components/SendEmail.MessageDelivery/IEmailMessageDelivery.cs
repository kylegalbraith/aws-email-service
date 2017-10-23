using EmailService.Models;
using System;
using System.Threading.Tasks;

namespace SendEmail.MessageDelivery
{
    public interface IEmailMessageDelivery
    {
        Task SendMessageAsync(StoredEmailMessage storedEmailMessage);
    }
}
