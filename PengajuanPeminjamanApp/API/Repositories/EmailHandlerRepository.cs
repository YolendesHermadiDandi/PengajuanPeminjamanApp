using API.Contracts;
using System.Net.Mail;

namespace API.Repositories
{
    public class EmailHandlerRepository : IEmailHandlerRepository
    {
        private string _server;
        private int _port;
        private string _fromEmailAddress;

        public EmailHandlerRepository(string server, int port, string fromEmailAddress)
        {
            _server = server;
            _port = port;
            _fromEmailAddress = fromEmailAddress;
        }

        public void Send(string subject, string body, string toEmail, string fromEmail)
        {
            var message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(new MailAddress(toEmail));

            using var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message);
        }
    }
}
