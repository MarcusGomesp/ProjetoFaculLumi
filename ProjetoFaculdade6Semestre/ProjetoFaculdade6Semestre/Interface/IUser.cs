using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using ProjetoFaculdade6Semestre.Model.Log;


namespace ProjetoFaculdade6Semestre.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> ListTodosAsync();
        Task<User> ListPorIdAsync(int id);
        Task<User> AdicionarAsync(User user);
        Task<object> LoginAsync(Login login);
        Task<bool> DeletarAsync(int id);

    }
}
