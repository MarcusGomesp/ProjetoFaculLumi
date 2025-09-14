namespace ProjetoFaculdade6Semestre.Interface.AI
{
    public interface IOpenAIService
    {
        Task<string> AnalisarTexto(string textoCurriculo);
    }
}
