using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Service
{
    public class CandidaturaService : ICandidatura
    {
        private readonly AppDbContextLumi _context;

        public CandidaturaService(AppDbContextLumi context)
        {
            _context = context;
        }

        // Criar candidatura a partir de UserId e RoleId
        public async Task<Candidatura> AdicionarAsync(int userId, int roleId)
        {
            var usuario = await _context.Users.FindAsync(userId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
            {
                throw new Exception("Vaga não encontrada.");
            }

            var candidatura = new Candidatura
            {
                UserId = userId,
                RoleId = roleId
            };

            _context.Candidaturas.Add(candidatura);
            await _context.SaveChangesAsync();

            return candidatura;
        }

        // Listar todas candidaturas
        public async Task<IEnumerable<Candidatura>> ListarAsync()
        {
            return await _context.Candidaturas
                .Include(c => c.User)
                .Include(c => c.Role)
                .ToListAsync();
        }

        // Listar candidaturas de um usuário específico
        public async Task<IEnumerable<Candidatura>> ListarPorUsuarioAsync(int userId)
        {
            return await _context.Candidaturas
                .Include(c => c.User)
                .Include(c => c.Role)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
