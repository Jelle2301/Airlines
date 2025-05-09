using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Mail.interfaces
{
    public interface IEmailSend
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAttachmentAsync(string email, string subject, string message, Stream attachementStream, string attachmentName, bool isBodyHtml = false);
        Task SendEmailWithPDFSAsync(string email, string subject, string message, List<MemoryStream> attachementStream, List<string> attachmentName, bool isBodyHtml = false);
    }
}
