using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CP5.Services
{
    /// <summary>
    /// Serviço responsável pela geração de tokens JWT utilizados na autenticação de usuários.
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do TokenService.
        /// Recebe as configurações do aplicativo (como chave secreta, emissor e audiência).
        /// </summary>
        /// <param name="configuration">Instância de configuração do sistema (IConfiguration).</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Gera um token JWT válido com base no nome de usuário informado.
        /// O token contém claims (informações adicionais) e expira após 120 minutos.
        /// </summary>
        /// <param name="username">Nome de usuário autenticado.</param>
        /// <returns>Token JWT assinado digitalmente.</returns>
        public string GenerateToken(string username)
        {
            var key = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
