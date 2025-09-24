using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Interface
{
    public interface IResults
    {
        Task<IEnumerable<Result>> ListToAsync();
        Task<Result> ListPorIdAsync(int id);
        Task<Result> AdicionarAsync(Result cv);
        Task<bool> DeletarAsync(int id);
    }
}
