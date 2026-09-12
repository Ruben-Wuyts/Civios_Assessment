using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentService
    {
        public Task<ExtractedTextResult> ExtractText(Stream stream);
    }
}
