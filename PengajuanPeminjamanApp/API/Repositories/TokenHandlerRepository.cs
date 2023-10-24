using API.Contracts;
using API.DTOs.Accounts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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

        public ClaimsDto ExtractClaimsFromJwt(string token)
        {
            if (token.Equals("") || token.Equals(null))
            {
                return new ClaimsDto(); // If the JWT token is empty, return an empty dictionary
            }

            try
            {
                // Configure the token validation parameters
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWTServices:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWTServices:Issuer"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTServices:Secretkey"]))
                };

                // Parse and validate the JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                // Extract the claims from the JWT token
                if (securityToken != null && claimsPrincipal.Identity is ClaimsIdentity identity)
                {
                    var claims = new ClaimsDto
                    {
                        UserGuid = identity.FindFirst("UserGuid").Value,
                        Name = identity.FindFirst("FullName").Value,
                        Email = identity.FindFirst("Email")!.Value,
                    };

                    var roles = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(claim => claim.Value).ToList();
                    claims.Role = roles;

                    return claims;
                }
            }
            catch
            {
                // If an error occurs while parsing the JWT token, return an empty dictionary
                return new ClaimsDto();
            }

            return new ClaimsDto();
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
