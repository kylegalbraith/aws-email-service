using System;
using System.Collections.Generic;
using System.Text;

namespace SES.Adapter.Config
{
    public interface IEmailDeliveryConfig
    {
        string FromAddress { get; set; }
    }

    public class EmailDeliveryConfig : IEmailDeliveryConfig
    {
        public string FromAddress { get; set; }
    }
}
