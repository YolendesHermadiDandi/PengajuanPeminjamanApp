using API.DTOs.Accounts;
using System.Security.Claims;

namespace API.Contracts
{
    public interface ITokenHandlerRepository
    {
        string Generate(IEnumerable<Claim> claims);
        ClaimsDto ExtractClaimsFromJwt(string token);
    }
}
