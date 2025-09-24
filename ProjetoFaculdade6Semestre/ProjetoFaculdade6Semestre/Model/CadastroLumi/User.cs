using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string? UserName { get; set; }

        [Required, StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required, StringLength(255), MinLength(6)]
        public string? PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        
        [JsonIgnore]
        public Role? Role { get; set; }



    }
}
