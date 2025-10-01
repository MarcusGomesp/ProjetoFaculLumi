using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Interface.AI
{
    public interface IOpenAIService
    {
        Task<Result> AnalisarCurriculoAsync(string texto, int roleId, int cvId);
    }
}
