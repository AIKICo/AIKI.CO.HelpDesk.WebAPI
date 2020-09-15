using System.Collections.Generic;
using AIKI.CO.HelpDesk.WebAPI.Services.ServiceConfiguration;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}