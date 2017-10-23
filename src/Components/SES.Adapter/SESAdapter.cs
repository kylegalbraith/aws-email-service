using Amazon.SimpleEmail;
using SES.Adapter.Config;
using System.Threading.Tasks;
using SES.Adapter.Extensions;
using AWS.Common.Extensions;
using Amazon.SimpleEmail.Model;

namespace SES.Adapter
{
    public class SESAdapter : ISESAdapter
    {
        private readonly IAmazonSimpleEmailService _sesClient;
        private readonly IEmailDeliveryConfig _emailDeliveryConfig;

        public SESAdapter(IAmazonSimpleEmailService sesClient, IEmailDeliveryConfig emailDeliveryConfig)
        {
            _sesClient = sesClient;
            _emailDeliveryConfig = emailDeliveryConfig;
        }

        public async Task SendEmailAsync(string recipient, string subject, string body)
        {
            var emailRequest = SendEmailRequestExtensions.CreateRequest(_emailDeliveryConfig.FromAddress, recipient, subject, body);
            var result = await _sesClient.SendEmailAsync(emailRequest);
            result.ThrowIfNotSuccess();
        }

    }
}
