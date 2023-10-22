using API.Models;
using API.Utilities.Enum;

namespace API.DTOs.Accounts
{
    public class RegisterAccountDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password {  get; set; }
        public string ConfirmPassword {  get; set; }

        public static implicit operator Employee(RegisterAccountDto createEmployeeDto)
        {
            return new Employee
            {
                //Nik = createEmployeeDto.Nik,
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                BirthDate = createEmployeeDto.BirthDate,
                Gender = createEmployeeDto.Gender,
                HiringDate = createEmployeeDto.HiringDate,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }

        public static implicit operator Account(RegisterAccountDto createAccountDto)
        {
            return new Account
            {
                Guid = Guid.NewGuid(),
                Password = createAccountDto.Password,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }

    }
}
