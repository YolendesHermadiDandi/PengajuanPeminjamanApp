using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        //setter getter
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int Otp { get; set; }
        public string ImgProfile { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator AccountDto(Account accounts)
        {
            return new AccountDto
            {
                Guid = accounts.Guid,
                Password = accounts.Password,
                ImgProfile = accounts.ImgProfile,
                Otp = accounts.Otp,
                IsUsed = accounts.IsUsed,
                ExpiredTime = accounts.ExpiredTime,


            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                ImgProfile = accountDto.ImgProfile,
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                Otp = accountDto.Otp,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
