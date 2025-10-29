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

        // Listar todas as Roles
        public async Task<IEnumerable<Role>> ListToAsync()
        {
            return await _context.Roles
                .Include(r => r.Owner)
                .Include(r => r.Cv)
                .ToListAsync();
        }

        // Listar Role por ID
        public async Task<Role> ListPorIdAsync(int id)
        {
            var role = await _context.Roles
                .Include(r => r.Owner)
                .Include(r => r.Cv)
                .FirstOrDefaultAsync(c => c.RoleId == id);

            if (role == null)
                throw new Exception($"Role com ID {id} não encontrado.");

            return role;
        }

        // Adicionar nova Role
        public async Task<Role> AdicionarAsync(Role role)
        {
            try
            {
                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var cvExiste = await _context.Cvs.AnyAsync(c => c.CvId == role.CvId);
                if (!cvExiste)
                    throw new Exception($"O CvId {role.CvId} não existe na tabela Cvs.");

                var userExiste = await _context.Users.AnyAsync(u => u.UserId == role.OwnerId);
                if (!userExiste)
                    throw new Exception($"O OwnerId {role.OwnerId} não existe na tabela Users.");

                _context.Roles.Add(role);
                await _context.SaveChangesAsync();

                return role;
            }
            catch (DbUpdateException ex)
            {
                var inner = ex.InnerException?.Message ?? ex.Message;
                throw new Exception($"Erro ao salvar Role no banco: {inner}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao salvar Role: {ex.Message}");
            }
        }

        // Deletar Role
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
