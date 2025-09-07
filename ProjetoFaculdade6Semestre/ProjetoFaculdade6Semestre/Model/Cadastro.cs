using System.ComponentModel.DataAnnotations;

namespace ProjetoFaculdade6Semestre.Models
{
    public class Cadastro
    {
        [Key]
        public int CadastroId { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)] 
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome{ get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string? Senha { get; set; }

        [Required]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string? ConfirmarSenha { get; set; }
    }
}
