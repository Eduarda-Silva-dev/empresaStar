using System.ComponentModel.DataAnnotations;

namespace empresaStar.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "informe o e-mail.")]
        [EmailAddress(ErrorMessage = "e-mail inválido")]
        public string Email { get; set; } = string.Empty;

        [Required (ErrorMessage = "informe a senha.")]
        [DataType (DataType.Password)]
        public string senha { get; set; }= string.Empty;
    }
}
