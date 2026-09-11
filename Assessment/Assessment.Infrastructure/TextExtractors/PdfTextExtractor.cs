using Assessment.Core.Interfaces;
using System.Text;
using UglyToad.PdfPig;

namespace Assessment.Infrastructure.TextExtractors
{
    public class PdfTextExtractor: IDocumentTextExtractor
    {

        public string ExtractText(FileStream stream)
        {
           
            using var document = PdfDocument.Open(stream);

            var text = new StringBuilder();

            foreach (var pag in document.GetPages())
            {
                text.AppendLine(pag.Text);
            }

            return text.ToString();
        }
    }
}
