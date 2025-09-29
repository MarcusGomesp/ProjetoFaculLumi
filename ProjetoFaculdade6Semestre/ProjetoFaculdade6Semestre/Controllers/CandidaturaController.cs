using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidaturaController : ControllerBase
    {
        private readonly ICandidatura _service;

        public CandidaturaController(ICandidatura service)
        {
            _service = service;
        }

        [HttpPost("{userId:int}/{roleId:int}")]
        public async Task<IActionResult> CriarCandidatura(int userId, int roleId)
        {
            try
            {
                var candidatura = await _service.AdicionarAsync(userId, roleId);
                return Ok(new { Mensagem = "Candidatura criada com sucesso", CandidaturaId = candidatura.CandidaturaId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarCandidaturas()
        {
            var candidaturas = await _service.ListarAsync();
            return Ok(candidaturas);
        }

        [HttpGet("usuario/{userId:int}")]
        public async Task<IActionResult> ListarPorUsuario(int userId)
        {
            var candidaturas = await _service.ListarPorUsuarioAsync(userId);
            return Ok(candidaturas);
        }
    }
}
