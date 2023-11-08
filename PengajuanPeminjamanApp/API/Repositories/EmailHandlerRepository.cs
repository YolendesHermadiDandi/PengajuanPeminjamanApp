using API.Contracts;
using API.DTOs.Requests;
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

        public void Send(string subject, DetailEmailDto detailEmail, string toEmail, string fromEmail)
        {
            string FilePath;
            switch (detailEmail.TipeEmail)
            {
                case "Admin":
                    FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "AdminEmail.html");
                    break;
                case "Employee":
                    FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "EmployeeEmail.html");
                    break;
                case "CompleteEmployee":
                    FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "CompleteEmployeeEmail.html");
                    break;
                case "CompleteAdmin":
                    FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "CompleteAdminEmail.html");
                    break;
                default:
                    FilePath = Path.Combine(Environment.CurrentDirectory, @"Utilities\Template\", "OTPEmail.html");
                    break;
            }
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[titleMassage]", subject);
            MailText = MailText.Replace("[Nama]", detailEmail.Name);
            MailText = MailText.Replace("[TanggalMulai]", detailEmail.TanggalMulai);
            MailText = MailText.Replace("[TanggalAkhir]", detailEmail.TanggalAkhir);
            MailText = MailText.Replace("[QrMassage]", detailEmail.QrMassage);
            string Status = subject?.Split(' ').Last() ?? "";
            MailText = MailText.Replace("[Status]", Status);
            if(detailEmail.OTP == null)
            {
                detailEmail.OTP = "";
            }
            MailText = MailText.Replace("[OTP]", detailEmail.OTP);
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
