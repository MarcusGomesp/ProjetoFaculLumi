using ProjetoFaculdade6Semestre.Interface.AI;
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


        // Construtor para inicializar o HttpClient e configurar a chave da API e o modelo
        public GeminiService(IConfiguration config)
        {
            _apiKey = config["Gemini:ApiKey"]
                      ?? throw new ArgumentNullException("Gemini:ApiKey não configurado");
            _model = config["Gemini:Model"] ?? "gemini-2.0-flash";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://generativelanguage.googleapis.com")
            };
        }


        // Método para analisar o texto usando a API Gemini ** OBS -> testar conectividade com a API do Gemini <-
        public async Task<string> AnalisarTexto(string texto)
        {
            var url = $"/v1beta/models/{_model}:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] {
                            new { text = $"Você é um avaliador de currículos. Avalie o seguinte texto:\n\n{texto}" }
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
            var root = doc.RootElement;

            var resposta = root
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return resposta ?? "Não houve resposta do Gemini.";
        }
    }
}

