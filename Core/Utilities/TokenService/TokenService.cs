using Data.Entities.Account;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.TokenService
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
    public class TokenServises : ITokenService
    {

        public string CreateToken(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ie@kykKEcgyWpZaoQl50N*q!c6Hp#9b!9fM6DrzPU@CX7w3jq"));
            var claims = new List<Claim> // چیزایی که میخوایم ذخیره کنیم میره توی کلایم
            {
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Sid , user.Email),
            };
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


