namespace ProjetoFaculdade6Semestre.Helpers
{
    public class CsvHelper
    {
        // Pega o arquivo csv e extrai o texto
        public static string ExtrairTexto(Stream csvStream)
        {
            using var reader = new StreamReader(csvStream);
            return reader.ReadToEnd();
        }
    }
}
