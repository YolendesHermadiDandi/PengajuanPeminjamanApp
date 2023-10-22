using API.Models;

namespace API.DTOs.Roles
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        /*
         * Method explicit digunakan supaya ketika melakukan konversi data
         * perlu melakukan casting (penjelasan dari tipe data yang akan di konvert)
         * sehingga jika ingin melakukan konversi data perlu melakukan casting
         * supaya memasukan data yang dikonvert itu benar.
         * 
         */
        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name,
            };
        }

        /*
         * method implicit yang digunaakan untuk create university
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator Role(RoleDto roleDto)
        {
            return new Role
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
