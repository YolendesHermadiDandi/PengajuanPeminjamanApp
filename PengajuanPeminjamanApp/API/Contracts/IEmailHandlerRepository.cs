namespace API.Contracts
{
    public interface IEmailHandlerRepository
    {
        void Send(string subject, string body, string toEmail, string fromEmail);
    }
}
