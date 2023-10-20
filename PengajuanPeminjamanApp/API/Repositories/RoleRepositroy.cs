using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class RoleRepositroy : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepositroy(RequestFasilityDbContext context) : base(context)
    {

    }
}
