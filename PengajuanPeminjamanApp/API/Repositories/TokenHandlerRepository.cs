using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Repositories
{
    public class TokenHandlerRepository : ITokenHandlerRepository
    {
        private readonly IConfiguration _configuration;
        public TokenHandlerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //pembuatan generate encode Token dengan menggunakan variabel secret key, siging credentials dan token option
        public string Generate(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTServices:Secretkey"]));
            var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTServices:Issuer"],
                                                    audience: _configuration["JWTServices:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddDays(1),
                                                    signingCredentials: sigingCredentials);
            var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return encodedToken;
        }
    }
}
