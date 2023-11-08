using API.DTOs.Requests;

namespace API.Contracts
{
    public interface IEmailHandlerRepository
    {
        void Send(string subject, DetailEmailDto detailEmail, string toEmail, string fromEmail);
    }
}
