using API.Models;
using API.Utilities.Enum;

namespace API.DTOs.Employees
{
    public class EmployeeDto
    {
        //setter getter
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        /*
        * Method explicit digunakan supaya ketika melakukan konversi data
        * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
        * sehingga jika ingin melakukan konversi data perlu melakukan casting
        * supaya memasukan data yang dikonvert itu benar.
        * 
        */
        public static explicit operator EmployeeDto(Employee employees)
        {
            return new EmployeeDto
            {
                Guid = employees.Guid,
                Nik = employees.Nik,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                BirthDate = employees.BirthDate,
                Gender = employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber,
            };
        }

        /*
         * method implicit yang digunaakan untuk create
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Employee(EmployeeDto employeeDto)
        {
            return new Employee
            {
                Guid = employeeDto.Guid,
                Nik = employeeDto.Nik,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                BirthDate = employeeDto.BirthDate,
                Gender = employeeDto.Gender,
                HiringDate = employeeDto.HiringDate,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
