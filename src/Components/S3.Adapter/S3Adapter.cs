using Amazon.S3;
using Amazon.S3.Model;
using AWS.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace S3.Adapter
{
    public class S3Adapter : IS3Adapter
    {
        private readonly IAmazonS3 _s3Client;

        public S3Adapter(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<T> ReadObjectAsync<T>(string bucket, string key)
        {
            var response = await _s3Client.GetObjectAsync(bucket, key);
            response.ThrowIfNotSuccess();
            var responseBody = "";
            using (StreamReader reader = new StreamReader(response.ResponseStream))
            {
                responseBody = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
