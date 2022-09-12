using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Configurations;
using NotesApp.DataModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Helpers
{
    public class SecurityHandler : ISecurityHandler
    {
        private AppSettings _appSettings;

        public SecurityHandler(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        public string GenerateSecurityToken(UserDto user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName} {user.LastName}"),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
    }
}
