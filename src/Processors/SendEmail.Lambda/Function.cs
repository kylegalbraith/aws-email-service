using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.SQS;
using Amazon.S3;
using SQS.Adapater;
using S3.Adapter;
using EmailMessage.Repository;
using SES.Adapter;
using Amazon.SimpleEmail;
using SES.Adapter.Config;
using SendEmail.MessageDelivery;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SendEmail.Lambda
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(string input, ILambdaContext context)
        {
            IAmazonSQS sqsClient = new AmazonSQSClient();
            ISqsAdapter sqsAdapter = new SqsAdapter(sqsClient, "");
            IAmazonS3 s3Client = new AmazonS3Client();
            IS3Adapter s3Adapter = new S3Adapter(s3Client);
            IEmailMessageRepository emailMessageRepository = new EmailMessageRepository(sqsAdapter, s3Adapter);

            IAmazonSimpleEmailService sesClient = new AmazonSimpleEmailServiceClient();
            IEmailDeliveryConfig emailConfig = new EmailDeliveryConfig()
            {
                FromAddress = ""
            };
            ISESAdapter sesAdapter = new SESAdapter(sesClient, emailConfig);
            IEmailMessageDelivery messageDelivery = new EmailMessageDelivery(sesAdapter);
            IEmailQueueProcessor emailProcessor = new EmailQueueProcessor(emailMessageRepository, messageDelivery);

            var emptyProcesses = 0;
            while(emptyProcesses <= 3 && context.RemainingTime > TimeSpan.FromSeconds(30))
            {
                var count = await emailProcessor.ProcessEmailMessages();
                if (count == 0)
                    emptyProcesses++;
            }
        }
    }
}
