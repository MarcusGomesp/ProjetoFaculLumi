using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CvId é obrigatório.")]
        [ForeignKey("Cv")]
        public int CvId { get; set; }

        public Cv? Cv { get; set; }

        [StringLength(255)]
        public string? RoleDescription { get; set; }

        [Required(ErrorMessage = "O campo OwnerId é obrigatório.")]
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        public User? Owner { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
