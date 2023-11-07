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
            string FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "sendEmail.html");
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[titleMassage]", subject);
            var message = new MailMessage()
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = MailText,
                IsBodyHtml = true,
            };
           
            message.To.Add(new MailAddress(toEmail));

            using var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message);
        }
    }
}
