using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {

        private readonly IResults _context;

        public ResultController(IResults context)
        {
            _context = context;
        }

        //Listar Result

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultAsync()
        {
            var result = await _context.ListToAsync();
            return Ok(result);
        }

        // Listar result ID
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetResultIdAsync(int id)
        {
            var result = await _context.ListPorIdAsync(id);
            return Ok(result);
        }

        //Adicionar Result
        [HttpPost("adicionar")]
        public async Task<ActionResult<Result>> PostResultAsync([FromBody] Result result)
        {
            try
            {
                var resp = await _context.AdicionarAsync(result);
                return Ok(resp);
            }
            catch (Exception ex)
            {

                return BadRequest($"Error ao carregar Result: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResutAsync([FromRoute] int id)
        {

            var success = await _context.DeletarAsync(id);
            if (!success)
                return NotFound($"Result with ID {id} not found.");

            return Ok(success);

        }

    }
}
