using EmailMessage.Repository;
using System.Threading.Tasks;

namespace SendEmail.MessageDelivery
{
    public class EmailQueueProcessor : IEmailQueueProcessor
    {
        private readonly IEmailMessageRepository _emailMessageRepository;
        private readonly IEmailMessageDelivery _emailMessageDelivery;

        public EmailQueueProcessor(IEmailMessageRepository emailMessageRepository, IEmailMessageDelivery emailMessageDelivery)
        {
            _emailMessageRepository = emailMessageRepository;
            _emailMessageDelivery = emailMessageDelivery;
        }

        public async Task<int> ProcessEmailMessages()
        {
            var emailMessages = await _emailMessageRepository.ReadMessagesAsync();
            var processedMessages = 0;
            foreach (var msg in emailMessages)
            {
                await _emailMessageDelivery.SendMessageAsync(msg.Message);
                await _emailMessageRepository.DeleteMessageAsync(msg.MessageReceiptHandle);
                processedMessages++;
            }
            return processedMessages;
        }
    }
}
