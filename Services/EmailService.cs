using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace trackingApi.Services;
   public class EmailService  : IEmailSender
    {
    public Task SendEmailAsync(string recipientEmail, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("Theron.stokes73@ethereal.email"));
        email.To.Add(MailboxAddress.Parse(recipientEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = message };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("theron.stokes73@ethereal.email", "KRsAj8Q5JxuCbsWCPK");
        smtp.Send(email);
        smtp.Disconnect(true);
        return Task.FromResult(0);  
        
    }
    }

