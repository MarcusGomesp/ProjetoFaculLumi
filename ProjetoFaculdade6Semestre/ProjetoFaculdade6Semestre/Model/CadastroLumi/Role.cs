using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, StringLength(50)]
        public string? RoleName { get; set; }

        [ForeignKey("CV")]
        public int CvId { get; set; }

        [JsonIgnore]
        public Cv? Cv { get; set; }
        public string? RoleDescription { get; set; }

       
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }

        [JsonIgnore]
        public User? Owner { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
