using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Model.Log;
using ProjetoFaculdade6Semestre.Models;

namespace ProjetoFaculdade6Semestre.Service
{
    public class CadastroServices : ICadastro
    {
        private readonly AppDbContext _context;

        public CadastroServices(AppDbContext context)
        {
            _context = context;
        }

        //listar todos cadastros
        public async Task<IEnumerable<Cadastro>> ListTodosAsync()
        {
            return await _context.Cadastros.ToListAsync();
        }

        // listar cadastro por ID
        public async Task<Cadastro> ListPorIdAsync(int id)
        {
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.CadastroId == id);
            if (cadastro == null)
                throw new Exception($"Cadastro com ID {id} não encontrado.");

            return cadastro;
        }

        //adicionar novo cadastro
        public async Task<Cadastro> AdicionarAsync(Cadastro cadastro)
        {
            try
            {
                cadastro.Senha = Utils.PasswordHasher.HashPassword(cadastro.Senha);
                cadastro.ConfirmarSenha = Utils.PasswordHasher.HashPassword(cadastro.ConfirmarSenha);
                _context.Cadastros.Add(cadastro);

                var result = _context.SaveChangesAsync();

                if (result == null)
                    throw new Exception("Ocorreu um erro ao salvar o cadastro.");

                return cadastro;
            }
            catch (Exception ex)
            {

                throw new Exception($"Erro ao Cadastrar: {ex.Message}");
            }
        }

        public async Task<object> LoginAsync(Login login)
        {
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.Email == login.Email);
            if (cadastro == null)
                throw new Exception("Email ou senha inválidos.");
            
            // Verificar a senha usando o PasswordHasher
            bool isPasswordValid = Utils.PasswordHasher.VerifyPassword(login.Senha, cadastro.Senha);
            bool isConfirmarSenhaValid = Utils.PasswordHasher.VerifyPassword(login.Senha, cadastro.ConfirmarSenha); //adicioado confirmação de senha

            if (!isPasswordValid || !isConfirmarSenhaValid)
                throw new Exception("Email ou senha inválidos.");
           
            return new
            {
                Message = "Login realizado com sucesso.",
                CadastroId = cadastro.CadastroId,
                Nome = cadastro.Nome,
                Email = cadastro.Email
            };
        }

        //deletar cadastro
        public async Task<bool> DeletarAsync(int id)
        {
            var cadastro = await _context.Cadastros.FirstOrDefaultAsync(c => c.CadastroId == id);

            if (cadastro == null)
               return false;

            _context.Cadastros.Remove(cadastro);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
