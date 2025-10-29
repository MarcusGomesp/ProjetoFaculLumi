using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.DTOs;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using ProjetoFaculdade6Semestre.Model.DTO;
using ProjetoFaculdade6Semestre.Service;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleServices _roleService;
        private readonly AppDbContextLumi _context;

        public RoleController(RoleServices roleService, AppDbContextLumi context)
        {
            _roleService = roleService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var roles = await _roleService.ListToAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var role = await _roleService.ListPorIdAsync(id);
            return Ok(role);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] RoleRequestDto dto)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Dados inválidos.");

                int? cvId = dto.Cv?.CvId ?? dto.CvId;
                int? ownerId = dto.Owner?.UserId ?? dto.OwnerId;

                if (cvId == null || ownerId == null)
                    return BadRequest("Os campos CvId e OwnerId são obrigatórios.");

                var cvExistente = await _context.Cvs.FirstOrDefaultAsync(c => c.CvId == cvId);
                if (cvExistente == null)
                    return BadRequest($"CV com ID {cvId} não encontrado.");

                var ownerExistente = await _context.Users.FirstOrDefaultAsync(u => u.UserId == ownerId);
                if (ownerExistente == null)
                    return BadRequest($"Usuário com ID {ownerId} não encontrado.");

                var role = new Role
                {
                    RoleName = dto.RoleName,
                    RoleDescription = dto.RoleDescription,
                    CvId = cvId.Value,
                    OwnerId = ownerId.Value
                };

                var novaRole = await _roleService.AdicionarAsync(role);
                return Ok(novaRole);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar Role: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                var result = await _roleService.DeletarAsync(id);
                return Ok(new { sucesso = result });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar role: {ex.Message}");
            }
        }
    }
}
