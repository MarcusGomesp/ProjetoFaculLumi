using System.ComponentModel.DataAnnotations;
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
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;
        
        [JsonIgnore]
        public ICollection<Role>? Roles { get; set; }
       
        [JsonIgnore]
        public ICollection<Result>? Results{ get; set; }
    }
}
