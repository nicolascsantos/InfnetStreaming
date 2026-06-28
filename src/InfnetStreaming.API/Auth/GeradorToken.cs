using InfnetStreaming.Application.Services.Usuario.LogarUsuario;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfnetStreaming.API.Auth
{
    public class GeradorToken : IGeradorToken
    {
        private readonly IConfiguration _configuration;

        public GeradorToken(IConfiguration configuration) => _configuration = configuration;

        public (string Token, DateTime ExpiraEm) Gerar(Guid usuarioId, string username)
        {
            var secretKey = _configuration["Jwt:SecretKey"]!;
            var issuer = _configuration["Jwt:Issuer"]!;
            var audience = _configuration["Jwt:Audience"]!;
            var expiraEmHoras = int.Parse(_configuration["Jwt:ExpiresInHours"] ?? "24");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiraEm = DateTime.UtcNow.AddHours(expiraEmHoras);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiraEm,
                signingCredentials: credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
        }
    }
}
