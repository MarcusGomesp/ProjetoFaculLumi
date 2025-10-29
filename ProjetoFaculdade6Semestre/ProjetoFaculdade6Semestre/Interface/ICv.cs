using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Interface
{
    public interface ICv
    {
        Task<IEnumerable<Cv>> ListToAsync();
        Task<Cv> ListPorIdAsync(int id);
        Task<Cv> AdicionarAsync(IFormFile file, int userId);
        Task<bool> DeletarAsync(int id);
    }
}
