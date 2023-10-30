namespace Client.DTOs.Accounts
{
    public class UpdateDataUserDto
    {
        public Guid Guid {  get; set; }
        public string Password {  get; set; }
        public string ConfirmPassword {  get; set; }
    }
}
