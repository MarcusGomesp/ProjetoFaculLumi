using ProjetoFaculdade6Semestre.Model.Log;
using ProjetoFaculdade6Semestre.Models;

namespace ProjetoFaculdade6Semestre.Interfaces
{
    public interface ICadastro
    {
        Task<IEnumerable<Cadastro>> ListTodosAsync();
        Task<Cadastro> ListPorIdAsync(int id);
        Task<Cadastro> AdicionarAsync(Cadastro cadastro);

        Task<object> LoginAsync(Login login);
        Task<bool> DeletarAsync(int id);

    }
}
