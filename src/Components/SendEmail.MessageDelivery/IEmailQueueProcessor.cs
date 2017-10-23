using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendEmail.MessageDelivery
{
    public interface IEmailQueueProcessor
    {
        Task<int> ProcessEmailMessages();
    }
}
