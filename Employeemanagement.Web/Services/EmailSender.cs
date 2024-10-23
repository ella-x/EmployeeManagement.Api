using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Employeemanagement.Web.Interfaces;

namespace Employeemanagement.Web.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        var smtpClient = new SmtpClient("mail.smtp2go.com", 2525)
        {
            Credentials = new NetworkCredential(_configuration["SMTP2GO:Username"], _configuration["SMTP2GO:Password"]),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["SMTP2GO:FromEmail"]),
            Subject = subject,
            Body = message,
            IsBodyHtml = true // Set to true if you're sending HTML emails
        };

        mailMessage.To.Add(toEmail);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }
}
