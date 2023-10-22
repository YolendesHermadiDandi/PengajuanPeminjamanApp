using API.Models;

namespace API.DTOs.AccountRoles
{
    public class AccountRolesDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static explicit operator AccountRolesDto(AccountRole accountRoles)
        {
            return new AccountRolesDto
            {
                Guid = accountRoles.Guid,
                AccountGuid = accountRoles.AccountGuid,
                RoleGuid = accountRoles.RoleGuid,

            };
        }

        public static implicit operator AccountRole(AccountRolesDto accountRoleDto)
        {
            return new AccountRole
            {
                Guid = accountRoleDto.Guid,
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                ModifiedDate = DateTime.Now,
            };
        }
    }
}
