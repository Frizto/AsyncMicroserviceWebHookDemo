using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Shared.DTOs;

namespace EmailNotificationWebHook.Service;

public class EmailService : IEmailService
{
    public string SendEmail(EmailDTO emailDTO)
    {
        var email = new MimeMessage();

        email.From.Add(MailboxAddress.Parse(""));
        email.To.Add(MailboxAddress.Parse(""));
        email.Subject = emailDTO.Title;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = emailDTO.Content
        };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("", "", CancellationToken.None);
        smtp.Send(email);
        smtp.Disconnect(true);

        return "Email sent successfully";
    }
}
