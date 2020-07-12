using System.Collections.Generic;

namespace AIKI.CO.HelpDesk.WebAPI.Services.Interface
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}