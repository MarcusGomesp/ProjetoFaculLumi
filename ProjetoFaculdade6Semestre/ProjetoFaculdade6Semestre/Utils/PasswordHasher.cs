namespace ProjetoFaculdade6Semestre.Utils
{
    public class PasswordHasher
    {

        //Método para criptografar a senha
        public static string  HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //Método para verificar a senha
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
