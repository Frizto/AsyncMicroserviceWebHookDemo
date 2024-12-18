using Shared.DTOs;

namespace EmailNotificationWebHook.Service;

public interface IEmailService
{
    string SendEmail(EmailDTO emailDTO);
}
