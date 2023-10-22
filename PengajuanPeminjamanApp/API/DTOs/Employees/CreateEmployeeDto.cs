using API.Models;
using API.Utilities.Enum;

namespace API.DTOs.Employees
{
    public class CreateEmployeeDto
    {
        //setter getter
        //public string Nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /* method implicit yang digunaakan untuk create Account Role
        * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
        *
        */
        public static implicit operator Employee(CreateEmployeeDto createEmployeeDto)
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
    }
}
