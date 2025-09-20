using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Service
{
    public class RoleServices : IRole
    {

        private readonly AppDbContextLumi _context;

        public RoleServices(AppDbContextLumi context)
        {
            _context = context;
        }

        //listar todos Roles
        public async Task<IEnumerable<Role>> ListToAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        // listar Role por ID
        public async Task<Role> ListPorIdAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(c => c.RoleId == id);
            if (role == null)
                throw new Exception($"Role com ID {id} não encontrado.");
            return role;
        }

        //adicionar novo Role
        public async Task<Role> AdicionarAsync(Role role)
        {
            try
            {
                _context.Roles.Add(role);
                var result = await _context.SaveChangesAsync();
                
                if (result <= 0)
                    throw new Exception("Ocorreu um erro ao salvar o Role.");
                return role;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao Cadastrar: {ex.Message}");
            }
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
           
            if (role == null)
                throw new Exception($"Role com ID {id} não encontrado.");
            
           _context.Roles.Remove(role);
             await _context.SaveChangesAsync();
            return true;
        }

    }
}
