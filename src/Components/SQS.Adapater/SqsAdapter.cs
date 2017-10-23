using Amazon.SQS;
using Amazon.SQS.Model;
using AWS.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQS.Adapater
{
    public class SqsAdapter : ISqsAdapter
    {
        private readonly IAmazonSQS _amazonSqsClient;
        private readonly string _queueUrl;

        public SqsAdapter(IAmazonSQS amazonSqsClient, string queueUrl)
        {
            _amazonSqsClient = amazonSqsClient;
            _queueUrl = queueUrl;
        }

        public async Task<IEnumerable<Message>> ReceiveMessagesAsync()
        {
            var response = await _amazonSqsClient.ReceiveMessageAsync(_queueUrl);
            response.ThrowIfNotSuccess();
            var sqsMessages = response.Messages;
            return sqsMessages;
        }

        public async Task DeleteMessageAsync(string receiptHandle)
        {
            var request = new DeleteMessageRequest()
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = receiptHandle
            };

            var response = await _amazonSqsClient.DeleteMessageAsync(request);
            response.ThrowIfNotSuccess();
        }
    }
}
