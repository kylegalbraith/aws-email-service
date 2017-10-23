using System.Threading.Tasks;
using EmailService.Models;
using SES.Adapter;

namespace SendEmail.MessageDelivery
{
    public class EmailMessageDelivery : IEmailMessageDelivery
    {
        private readonly ISESAdapter _sesAdapter;

        public EmailMessageDelivery(ISESAdapter sesAdapter)
        {
            _sesAdapter = sesAdapter;
        }        

        public async Task SendMessageAsync(StoredEmailMessage storedEmailMessage)
        {
            await _sesAdapter.SendEmailAsync(storedEmailMessage.Recipient, storedEmailMessage.Subject, storedEmailMessage.Body);
        }
    }
}
