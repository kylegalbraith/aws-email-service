using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SES.Adapter
{
    public interface ISESAdapter
    {
        Task SendEmailAsync(string recipient, string subject, string body);
    }
}
