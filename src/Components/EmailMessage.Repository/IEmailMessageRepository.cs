using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailMessage.Repository
{
    public interface IEmailMessageRepository
    {
        Task<IList<StoredEmailMessageWithReceipt>> ReadMessagesAsync();
        Task DeleteMessageAsync(string receiptHandle);
    }
}
