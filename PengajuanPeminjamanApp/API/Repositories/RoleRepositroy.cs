using API.Contracts;
using API.Data;
using API.Models;
using System.Data;

namespace API.Repositories;

public class RoleRepositroy : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepositroy(RequestFasilityDbContext context) : base(context)
    {

    }

    public Guid? GetDefaultRoleGuid()
    {
        return _context.Set<Role>().FirstOrDefault(r => r.Name == "employee")?.Guid;
    }
}
