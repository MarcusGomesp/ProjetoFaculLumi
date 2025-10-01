using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interface;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;

namespace ProjetoFaculdade6Semestre.Service
{
    public class CvServices : ICv
    {
        private readonly AppDbContextLumi _context;
        private readonly IWebHostEnvironment _env;

        public CvServices(AppDbContextLumi context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //listar todos Cv
        public async Task<IEnumerable<Cv>> ListToAsync()
        {
            return await _context.Cvs.ToListAsync();
        }

        // listar Cv por ID
        public async Task<Cv> ListPorIdAsync(int id)
        {
            var cv = await _context.Cvs.FirstOrDefaultAsync(c => c.CvId == id);
            if (cv == null)
                throw new Exception($"Cv com ID {id} não encontrado.");
            return cv;
        }

        // adicionar novo CV via upload
        public async Task<Cv> AdicionarAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("Nenhum arquivo enviado.");

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var cv = new Cv
            {
                FileName = file.FileName,
                FilePath = filePath
            };

            _context.Cvs.Add(cv);
            await _context.SaveChangesAsync();

            return cv;
        }

        // deletar cv
        public async Task<bool> DeletarAsync(int id)
        {
            var cv = await _context.Cvs.FirstOrDefaultAsync(x => x.CvId == id);

            if (cv == null)
                throw new Exception($"CV com ID {id} não encontrado.");

            if (File.Exists(cv.FilePath))
                File.Delete(cv.FilePath);

            _context.Cvs.Remove(cv);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}