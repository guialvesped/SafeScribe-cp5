
using CP5.Models;
using CP5.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CP5.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários e pelo acesso a dados protegidos.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        /// <summary>
        /// Construtor do controlador que injeta o serviço de geração de token JWT.
        /// </summary>
        /// <param name="tokenService">Serviço para geração de tokens JWT.</param>
        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Realiza a autenticação de um usuário com base nas credenciais fornecidas.
        /// Se as credenciais forem válidas, gera e retorna um token JWT.
        /// </summary>
        /// <param name="model">Objeto contendo nome de usuário e senha.</param>
        /// <returns>Token JWT em caso de sucesso, ou 401 Unauthorized em caso de falha.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin model)
        {
            // Simulação de validação — em ambiente real, faria consulta ao banco de dados
            if (model.Username == "RM555357" && model.Password == "555357")
            {
                var token = _tokenService.GenerateToken(model.Username);
                return Ok(new { token });
            }

            return Unauthorized("Usuário ou senha inválidos.");
        }

        /// <summary>
        /// Endpoint protegido que só pode ser acessado com um token JWT válido.
        /// Retorna uma mensagem personalizada ao usuário autenticado.
        /// </summary>
        /// <returns>Mensagem de sucesso com o nome do usuário autenticado.</returns>
        [HttpGet("dados-protegidos")]
        [Authorize]
        public IActionResult GetDadosProtegidos()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok($"Olá {username}! Você pode acessar as notas protegidas!");
        }

    }
}
