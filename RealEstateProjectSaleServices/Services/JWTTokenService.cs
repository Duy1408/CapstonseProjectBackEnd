using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleServices.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateProjectSaleServices.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly IConfiguration _config;
        public JWTTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateJWTToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.Email ?? ""),
                new Claim(ClaimTypes.Role, account.RoleID.ToString()!),
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddMinutes(120), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
