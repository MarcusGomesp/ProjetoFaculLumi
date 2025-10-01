using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Interface
{
    public interface IRole
    {
        Task<IEnumerable<Role>> ListToAsync();
        Task<Role> ListPorIdAsync(int id);
        Task<Role> AdicionarAsync(Role role);
        Task<bool> DeletarAsync(int id);

    }
}
