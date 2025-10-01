using DocumentFormat.OpenXml.Packaging;

namespace ProjetoFaculdade6Semestre.Helpers
{
    public class DocxHelper
    {

        // Pega o arquivo docx e extrai o texto
        public static string ExtrairTexto(Stream docSrteam)
        {
            using var wordDoc = WordprocessingDocument.Open(docSrteam, false);
            return string.Join(Environment.NewLine,
                wordDoc.MainDocumentPart.Document.Body
                .Descendants<DocumentFormat.OpenXml.Wordprocessing.Text>()
                .Select(t => t.Text));
        }
    }
}
