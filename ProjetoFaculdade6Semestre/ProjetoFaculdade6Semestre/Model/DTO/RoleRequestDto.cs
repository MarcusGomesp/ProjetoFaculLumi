namespace ProjetoFaculdade6Semestre.DTOs
{
    public class RoleRequestDto
    {
        public string? RoleName { get; set; }
        public string? RoleDescription { get; set; }

        public int? CvId { get; set; }
        public CvDto? Cv { get; set; }

        public int? OwnerId { get; set; }
        public UserDto? Owner { get; set; }
    }

    public class CvDto
    {
        public int CvId { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
    }
}
