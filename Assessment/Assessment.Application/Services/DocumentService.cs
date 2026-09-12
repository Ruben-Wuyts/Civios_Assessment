using Assessment.Core.Interfaces;
using Assessment.Core.Results;
namespace Assessment.Application.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly IDocumentTextExtractor _textExtractor;
        private readonly IExtractedTextValidator _textValidator;
        public DocumentService(IDocumentTextExtractor textExtractor, IExtractedTextValidator textValidator)
        {
            _textExtractor = textExtractor;
            _textValidator = textValidator;
        }

        public Task<ExtractedTextResult> ExtractText(Stream stream)
        {
            string text = _textExtractor.ExtractText(stream);
            return Task.FromResult(_textValidator.IsTextValid(text));
            
        }

    }
}
