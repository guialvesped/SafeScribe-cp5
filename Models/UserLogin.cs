namespace CP5.Models
{
    /// <summary>
    /// Representa o modelo de dados usado no login.
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Nome de usuário utilizado para autenticação.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }
    }

}
