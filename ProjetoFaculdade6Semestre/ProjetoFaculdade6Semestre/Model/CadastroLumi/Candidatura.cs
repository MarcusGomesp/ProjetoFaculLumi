using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class Candidatura
    {
        [Key]
        public int CandidaturaId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        public DateTime DataCandidatura { get; set; } = DateTime.UtcNow;
    }
}
