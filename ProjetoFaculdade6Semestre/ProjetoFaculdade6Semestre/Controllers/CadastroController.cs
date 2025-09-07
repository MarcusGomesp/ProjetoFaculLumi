using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Model.Log;
using ProjetoFaculdade6Semestre.Models;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastro _contextService;

        public CadastroController(ICadastro contextService)
        {
            _contextService = contextService;
        }

        // GET: api/Cadastro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> GetCadastro()
        {
            var cadastro = await _contextService.ListTodosAsync();
            return Ok(cadastro);
        }

        // GET: api/Cadastro/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Cadastro>> GetCadastrID(int id)
        {
            var cadastro = await _contextService.ListPorIdAsync(id);

            if (cadastro == null)
                return NotFound();

            return Ok(cadastro);
        }

        // POST: api/Cadastro
        [HttpPost]
        public async Task<ActionResult<Cadastro>> PostCadastro([FromBody ]Cadastro cadastro)
        {
            try
            {
                var result = await _contextService.AdicionarAsync(cadastro);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao carregar cadastro: {ex.Message}");
            }
        }

        // POST: api/Cadastro/login
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login login)
        {
            try
            {
                var result = await _contextService.LoginAsync(login);

                if (result == null)
                {
                    throw new Exception("Email ou senha Inválido.");
                }

                return Ok(result);
            }
            catch (Exception ex )
            {
                return BadRequest($"Erro ao logar: {ex.Message}");
            }
        }

        // DELETE: api/Cadastro/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastro([FromRoute] int id)
        {
            var success = await _contextService.DeletarAsync(id);
           
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
