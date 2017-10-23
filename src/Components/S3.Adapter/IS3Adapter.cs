using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace S3.Adapter
{
    public interface IS3Adapter
    {
        Task<T> ReadObjectAsync<T>(string bucket, string key);
    }
}
