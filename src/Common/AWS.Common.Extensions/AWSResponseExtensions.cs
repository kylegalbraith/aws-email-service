using Amazon.Runtime;
using System;

namespace AWS.Common.Extensions
{
    public static class AWSResponseExtensions
    {
        public static void ThrowIfNotSuccess(this AmazonWebServiceResponse response)
        {
            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Unexpected AWS response: {response.HttpStatusCode}");
        }
    }
}
