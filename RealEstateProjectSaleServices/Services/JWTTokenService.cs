using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateProjectSaleBusinessObject.Admin;
using RealEstateProjectSaleBusinessObject.BusinessObject;
using RealEstateProjectSaleBusinessObject.ViewModels;
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
        private readonly AdminAccountConfig _adminAccountConfig;
        public JWTTokenService(IConfiguration config, IOptions<AdminAccountConfig> adminAccountConfig)
        {
            _config = config;
            _adminAccountConfig = adminAccountConfig.Value;
        }

        public string CreateAdminJWTToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, "Admin"),
                new Claim(ClaimTypes.Email, _adminAccountConfig.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddYears(2), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string CreateJWTToken(Account account)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.AccountID.ToString()),
                new Claim(ClaimTypes.Email, account.Email ?? ""),
                new Claim(ClaimTypes.Role, account.Role?.RoleName ?? ""),
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddYears(2), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthVM ParseJwtToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            if (validatedToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var tokenId = principal.FindFirst("jti");
            var accountId = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            var emailClaim = principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            var roleClaim = principal.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (tokenId == null || accountId == null || emailClaim == null || roleClaim == null)
            {
                throw new SecurityTokenException("Invalid token claims");
            }

            var account = new AuthVM
            {
                Email = emailClaim.Value,
                RoleName = roleClaim.Value
            };

            if (accountId.Value == "Admin")
            {
                account.AccountID = null;
            }
            else
            {
                account.AccountID = new Guid(accountId.Value);
            }

            return account;

        }


    }
}
