namespace API.DTOs.Requests
{
    public class SendEmailDto
    {
        public string FromEmail { get; set; }
        public string RecipientEmail { get; set; }
        public string Message { get; set; }
        public Guid RequestGuid { get; set; }
    }
}
