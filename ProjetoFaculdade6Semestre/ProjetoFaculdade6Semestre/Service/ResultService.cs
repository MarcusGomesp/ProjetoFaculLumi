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


        //listar todos result
        public async Task<IEnumerable<Result>> ListToAsync()
        {
            return await _context.Results.ToListAsync();
        }

        // listar result por ID
        public async Task<Result> ListPorIdAsync(int id)
        {
            var result = await _context.Results.FirstOrDefaultAsync(c => c.ResultId == id);
            if (result == null)
                throw new Exception($"Result com ID {id} não encontrado.");
            return result;
        }

        //adicionar Result
        public async Task<Result> AdicionarAsync(Result res)
        {
            try
            {
                _context.Results.Add(res);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                    throw new Exception("Ocorreu um erro ao salvar o Role.");
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro Results: {ex.Message}");
            }
        }

        // Deletar Result
        public async Task<bool> DeletarAsync(int id)
        {
            var resul = await _context.Results.FirstOrDefaultAsync(x => x.ResultId == id);

            if (resul == null)
                throw new Exception($"Role com ID {id} não encontrado.");

            _context.Results.Remove(resul);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}