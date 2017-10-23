using S3.Adapter;
using SQS.Adapater;
using System;
using System.Threading.Tasks;
using EmailService.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EmailMessage.Repository
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly ISqsAdapter _sqsAdapter;
        private readonly IS3Adapter _s3Adapter;

        public EmailMessageRepository(ISqsAdapter sqsAdapter, IS3Adapter s3Adapter)
        {
            _sqsAdapter = sqsAdapter;
            _s3Adapter = s3Adapter;
        }

        public async Task<IList<StoredEmailMessageWithReceipt>> ReadMessagesAsync()
        {
            var messages = (await _sqsAdapter.ReceiveMessagesAsync()).ToList();
            var receiptWithMessage = new Dictionary<string, SendEmailCommand>();
            messages.ForEach(m => receiptWithMessage.Add(m.ReceiptHandle, JsonConvert.DeserializeObject<SendEmailCommand>(m.Body)));
            return await LoadMessagesFromS3(receiptWithMessage);
        }

        public async Task DeleteMessageAsync(string receiptHandle)
        {
            await _sqsAdapter.DeleteMessageAsync(receiptHandle);
        }

        private async Task<IList<StoredEmailMessageWithReceipt>> LoadMessagesFromS3(IDictionary<string, SendEmailCommand> emailCommands)
        {
            var result = new List<StoredEmailMessageWithReceipt>();
            foreach (var key in emailCommands.Keys)
            {
                var msg = emailCommands[key];
                var emailMsg = await _s3Adapter.ReadObjectAsync<StoredEmailMessage>(msg.Location.Bucket, msg.Location.Key);
                result.Add(new StoredEmailMessageWithReceipt()
                {
                    Message = emailMsg,
                    MessageReceiptHandle = key
                });
            }
            return result;
        }
    }
}
