using API.Models;

namespace API.DTOs.Accounts
{
    public class CreateAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }

        public static implicit operator Account(CreateAccountDto createAccountDto)
        {
            return new Account
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                //OTP = createAccountDto.Otp,
                //IsUsed = createAccountDto.IsUsed,
                //ExpiredTime = createAccountDto.ExpiredTime,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
