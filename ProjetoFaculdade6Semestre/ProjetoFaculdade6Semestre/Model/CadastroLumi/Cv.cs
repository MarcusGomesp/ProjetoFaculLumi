using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class Cv
    {
        [Key]
        public int CvId { get; set; }

        [Required, StringLength(255)]
        public string? FileName { get; set; }

        [Required, StringLength(255)]
        public string? FilePath { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }


        [JsonIgnore]
        public ICollection<Role>? Roles { get; set; }

        [JsonIgnore]
        public ICollection<Result>? Results { get; set; }
    }
}