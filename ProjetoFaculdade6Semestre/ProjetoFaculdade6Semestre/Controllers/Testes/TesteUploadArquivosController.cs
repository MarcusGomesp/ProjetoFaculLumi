using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoFaculdade6Semestre.Helpers;
using ProjetoFaculdade6Semestre.Interface.AI;

namespace ProjetoFaculdade6Semestre.Controllers.Testes
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteUploadArquivosController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;

        public TesteUploadArquivosController(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        // POST: api/TesteUploadArquivos/analise-arquivo
        // Recebe o arquivo, extrai o texto e envia para a API do OpenAI ---> OBS Isso é um teste,  NÃO USAR EM PRODUÇÃO <--

        [HttpPost ("analise-arquivo")]
        public async Task<IActionResult> UploaArquivo(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nenhum arquivo enviado.");
            }             

            string textoExtraido;

            var extension = Path.GetExtension(file.FileName).ToLower();
            using (var stream = file.OpenReadStream())
            {
                if (extension == ".pdf")
                    textoExtraido = PdfHelper.ExtrairTexto(stream);
                else if (extension == ".docx")
                    textoExtraido = DocxHelper.ExtrairTexto(stream);
                else if (extension == ".csv")
                    textoExtraido = CsvHelper.ExtrairTexto(stream);
                else
                    return BadRequest("Formato não suportado.");
            }

            var resposta = await _openAIService.AnalisarTexto(textoExtraido);

            return Ok(new { Resultado = resposta });
        }

    }
}
