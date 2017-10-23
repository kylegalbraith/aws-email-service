using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQS.Adapater
{
    public interface ISqsAdapter
    {
        Task<IEnumerable<Message>> ReceiveMessagesAsync();
        Task DeleteMessageAsync(string receiptHandle);
    }
}
