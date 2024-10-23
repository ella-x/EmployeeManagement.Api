using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Employeemanagement.Web.Interfaces;

public interface IEmailSender 
{
    Task SendEmailAsync(string toEmail, string subject, string message);
}