using System.Text;
using UglyToad.PdfPig;

namespace ProjetoFaculdade6Semestre.Helpers
{
    public class PdfHelper
    {

        // pega o arquivo pdf e extrai o texto
        public static string ExtrairTexto(Stream pdfStream)
        {
            using var pdf = PdfDocument.Open(pdfStream);
            var texto = new StringBuilder();

            foreach (var page in pdf.GetPages())
            {
                texto.AppendLine(page.Text);
            }

            return texto.ToString();
        }
    }
}
