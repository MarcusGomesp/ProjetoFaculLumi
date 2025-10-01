using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Interface
{
    public interface ICandidatura
    {
        Task<Candidatura> AdicionarAsync(int userId, int roleId);
        Task<IEnumerable<Candidatura>> ListarAsync();
        Task<IEnumerable<Candidatura>> ListarPorUsuarioAsync(int userId);
    }
}
