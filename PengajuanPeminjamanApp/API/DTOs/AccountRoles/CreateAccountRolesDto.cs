using API.Models;

namespace API.DTOs.AccountRoles
{
    public class CreateAccountRolesDto
    {
        //setter getter
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }


        /*
         * method implicit yang digunaakan untuk create Account Role
         * tanpa melakukan casting terhadap konversi tipe data ke tipe data lainnya
         * 
         */
        public static implicit operator AccountRole(CreateAccountRolesDto createAccountRoleDto)
        {
            return new AccountRole
            {
                AccountGuid = createAccountRoleDto.AccountGuid,
                RoleGuid = createAccountRoleDto.RoleGuid,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
