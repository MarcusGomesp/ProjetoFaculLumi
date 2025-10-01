using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjetoFaculdade6Semestre.Model.CadastroLumi
{
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        [ForeignKey("Cv")]
        public int CvId { get; set; }

        [JsonIgnore]
        public Cv? Cv { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Percentual { get; set; }

        public string? Resume { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? File { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

    }
}