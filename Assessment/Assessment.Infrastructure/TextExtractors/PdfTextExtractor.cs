using Assessment.Core.Interfaces;
using System.Text;
using UglyToad.PdfPig;

namespace Assessment.Infrastructure.TextExtractors
{
    public class PdfTextExtractor: IDocumentTextExtractor
    {
        public string ExtractText(Stream stream)
        {
            
            using var document = PdfDocument.Open(stream);

            var text = new StringBuilder();

            foreach (var page in document.GetPages())
            {
                text.AppendLine(page.Text);
                text.AppendLine();
            }
            return text.ToString();
            
        }
    }
}
