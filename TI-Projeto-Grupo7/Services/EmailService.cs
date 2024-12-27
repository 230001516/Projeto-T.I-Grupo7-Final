using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TI_Projeto_Grupo7.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _password;

        public EmailService(string smtpHost, int smtpPort, string fromEmail, string password)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _fromEmail = fromEmail;
            _password = password;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string body){
            
            try{

                using (MailMessage message = new MailMessage()){

                    message.From = new MailAddress(_fromEmail);
                    message.To.Add(email);
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = body;

                    using (SmtpClient smtpClient = new SmtpClient(_smtpHost, _smtpPort)){

                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);

                        await smtpClient.SendMailAsync(message);
                    }
                }

                return true;

            }catch (Exception ex){

                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            
            }
        }
    }
}
