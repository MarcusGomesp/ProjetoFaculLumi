using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Helpers;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Interface.AI;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : ControllerBase
    {
        private readonly ICv _cvService;
        private readonly IOpenAIService _openAIService;
        private readonly AppDbContextLumi _context;

        public CvController(ICv cvService, IOpenAIService openAIService, AppDbContextLumi context)
        {
            _cvService = cvService;
            _openAIService = openAIService;
            _context = context;
        }

        // GET: api/Cv
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cv>>> GetCv()
        {
            var cv = await _cvService.ListToAsync();
            return Ok(cv);
        }

        //GET: api/Cv/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cv>> GetCvById(int id)
        {
            var cv = await _cvService.ListPorIdAsync(id);
            if (cv == null)
                return NotFound($"Cv com ID {id} não encontrado.");
            return Ok(cv);
        }

        // POST: api/Cv/upload-e-aplicar/{roleId}
        [HttpPost("upload-e-aplicar/{roleId}")]
        public async Task<IActionResult> UploadEAplicar(int roleId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            try
            {
                var cvSalvo = await _cvService.AdicionarAsync(file);

                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                    return NotFound("Vaga não encontrada.");

                string textoExtraido;
                using (var stream = file.OpenReadStream())
                {
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension == ".pdf")
                        textoExtraido = PdfHelper.ExtrairTexto(stream);
                    else if (extension == ".docx")
                        textoExtraido = DocxHelper.ExtrairTexto(stream);
                    else
                        return BadRequest("Formato não suportado.");
                }

               
                var result = await _openAIService.AnalisarCurriculoAsync(textoExtraido, roleId, cvSalvo.CvId);

                _context.Results.Add(result);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Mensagem = "Currículo enviado e avaliado com sucesso",
                    Cv = cvSalvo,
                    Resultado = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        //DELETE: api/Cv/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCvAsync([FromRoute] int id)
        {
            var success = await _cvService.DeletarAsync(id);

            if (!success)
                return NotFound($"Cv com ID {id} não encontrado.");

            return NoContent();
        }
    }
}
