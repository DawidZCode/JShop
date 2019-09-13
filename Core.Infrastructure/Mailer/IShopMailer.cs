using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mailer
{
    public interface IShopMailer
    {
        Task<bool> SendMailAsync(string mailToName, string mailTo, string title, string desctiption);

        Task<bool> SendMailWithAttachment(string mailToName, string mailTo, string title, string desctiption, string fileName, string attachmentBase64);

    }
}
