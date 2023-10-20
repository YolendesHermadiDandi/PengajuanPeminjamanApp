using API.Models;

namespace API.DTOs.Roles
{
    public class CreateRoleDto
    {
        public string Name { get; set; }


        /* method implicit yang digunaakan untuk create Account Role
        * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
        *
        */
        public static implicit operator Role(CreateRoleDto createRoleDto)
        {
            return new Role
            {
                Name = createRoleDto.Name,
                CreateDate = DateTime.Now,
                ModifiedeDate = DateTime.Now,
            };
        }
    }
}
