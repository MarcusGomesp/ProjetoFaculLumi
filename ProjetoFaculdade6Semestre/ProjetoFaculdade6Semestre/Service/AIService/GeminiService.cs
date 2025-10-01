using ProjetoFaculdade6Semestre.Interface.AI;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using ProjetoFaculdade6Semestre.Model.DTO;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ProjetoFaculdade6Semestre.Service.AIService
{
    public class GeminiService : IOpenAIService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly string _model;
        private readonly AppDbContextLumi _context;

        public GeminiService(IConfiguration config, AppDbContextLumi context)
        {
            _apiKey = config["Gemini:ApiKey"]
                      ?? throw new ArgumentNullException("Gemini:ApiKey não configurado");
            _model = config["Gemini:Model"] ?? "gemini-2.0-flash";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://generativelanguage.googleapis.com")
            };

            _context = context;
        }

        public async Task<Result> AnalisarCurriculoAsync(string texto, int roleId, int cvId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);
            if (role == null)
                throw new Exception($"Nenhuma vaga encontrada com RoleId {roleId}");

            var url = $"/v1beta/models/{_model}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] {
                            new { text =
                                $@"Você é um avaliador de currículos.

                                Compare o currículo com a vaga abaixo e responda SOMENTE com JSON válido.

                                Vaga:
                                Nome: {role.RoleName}
                                Descrição: {role.RoleDescription}

                                Currículo:
                                {texto}

                                Formato esperado:
                                {{
                                  ""email"": string,
                                  ""percentual"": decimal (0 a 100),
                                  ""resume"": string,
                                  ""file"": ""avaliacao_cv{cvId}.pdf"",
                                  ""status"": string
                                }}"
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseString);

            var respostaJson = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(respostaJson))
                throw new Exception("Gemini não retornou dados válidos");

            respostaJson = respostaJson.Trim();
            if (respostaJson.StartsWith("```"))
            {
                var start = respostaJson.IndexOf("{");
                var end = respostaJson.LastIndexOf("}");
                if (start >= 0 && end > start)
                    respostaJson = respostaJson.Substring(start, end - start + 1);
            }

            var resultDto = JsonSerializer.Deserialize<ResultGeminiDto>(respostaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (resultDto == null)
                throw new Exception("Falha ao converter resposta do Gemini");

            return new Result
            {
                CvId = cvId,
                Percentual = resultDto.Percentual,
                Resume = resultDto.Resume,
                File = resultDto.File,
                Status = resultDto.Status
            };
        }
    }
}
