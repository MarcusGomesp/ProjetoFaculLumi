using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Service
{
    public class ResultService : IResults
    {
        private readonly AppDbContextLumi _context;

        public ResultService(AppDbContextLumi context)
        {
            _context = context;
        }

        // Listar todos resultados 
        public async Task<IEnumerable<Result>> ListToAsync()
        {
            var results = await _context.Results
                .Include(r => r.Cv)
                    .ThenInclude(cv => cv.User)
                .ToListAsync();

            foreach (var r in results)
            {
                if (r.Cv?.User != null)
                {
                    r.EmailCandidato = r.Cv.User.Email;
                }
                else
                {
                    var candidatura = await _context.Candidaturas
                        .Include(c => c.User)
                        .Include(c => c.Role)
                        .FirstOrDefaultAsync(c =>
                            c.Role != null &&
                            _context.Roles.Any(role => role.CvId == r.CvId && role.RoleId == c.RoleId)
                        );

                    if (candidatura?.User != null)
                        r.EmailCandidato = candidatura.User.Email;
                    else
                        r.EmailCandidato = "E-mail não informado";
                }
            }

            return results;
        }

        // Buscar por ID
        public async Task<Result> ListPorIdAsync(int id)
        {
            var result = await _context.Results
                .Include(r => r.Cv)
                    .ThenInclude(cv => cv.User)
                .FirstOrDefaultAsync(c => c.ResultId == id);

            if (result == null)
                throw new Exception($"Result com ID {id} não encontrado.");

            if (result.Cv?.User != null)
                result.EmailCandidato = result.Cv.User.Email;
            else
                result.EmailCandidato = "E-mail não informado";

            return result;
        }

        // Adicionar resultado
        public async Task<Result> AdicionarAsync(Result res)
        {
            try
            {
                _context.Results.Add(res);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                    throw new Exception("Ocorreu um erro ao salvar o Result.");

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Results: {ex.Message}");
            }
        }

        // Deletar resultado
        public async Task<bool> DeletarAsync(int id)
        {
            var resul = await _context.Results.FirstOrDefaultAsync(x => x.ResultId == id);

            if (resul == null)
                throw new Exception($"Result com ID {id} não encontrado.");

            _context.Results.Remove(resul);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
