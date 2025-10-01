using System.ComponentModel.DataAnnotations;

namespace ProjetoFaculdade6Semestre.Model.Log
{
    public class Login
    {
        [Required(ErrorMessage = "Email Obrigatorio")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Senha Obrigatorio")]
        public string? Senha { get; set; }
    }
}
