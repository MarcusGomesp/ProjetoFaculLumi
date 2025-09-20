using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Model.CadastroLumi;
using ProjetoFaculdade6Semestre.Model.Log;
using ProjetoFaculdade6Semestre.Utils;

namespace ProjetoFaculdade6Semestre.Service
{
    public class UserServices : IUser
    {
        private readonly AppDbContextLumi _context;

        public UserServices(AppDbContextLumi context)
        {
            _context = context;
        }

        //listar todos cadastros
        public async Task<IEnumerable<User>> ListTodosAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // listar cadastro por ID
        public async Task<User> ListPorIdAsync(int id)
        {
            var cadastro = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);
            if (cadastro == null)
                throw new Exception($"Cadastro com ID {id} não encontrado.");

            return cadastro;
        }

        //adicionar novo cadastro
        public async Task<User> AdicionarAsync(User cadastro)
        {
            try
            {
              
                cadastro.PasswordHash = PasswordHasher.HashPassword(cadastro.PasswordHash);

                _context.Users.Add(cadastro);

                var result = await _context.SaveChangesAsync(); 

                if (result <= 0) 
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
            var cadastro = await _context.Users
                .FirstOrDefaultAsync(c => c.Email == login.Email);


            if (cadastro == null)
                throw new Exception("Email ou senha inválidos.");

            
            
            // Verificar a senha usando o PasswordHasher
            bool isPasswordValid = PasswordHasher.VerifyPassword(login.Senha, cadastro.PasswordHash);

            if (!isPasswordValid)
                throw new Exception("Email ou senha inválidos.");

            return new
            {
                Message = "Login realizado com sucesso.",
                CadastroId = cadastro.UserId,
                Nome = cadastro.UserName,
                Email = cadastro.Email
            };
        }

        //deletar cadastro
        public async Task<bool> DeletarAsync(int id)
        {
            var cadastro = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);

            if (cadastro == null)
                return false;

            _context.Users.Remove(cadastro);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
