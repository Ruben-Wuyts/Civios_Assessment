using Assessment.Core.Interfaces;

namespace Assessment.Application.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly IDocumentTextExtractor _textExtractor;
        private readonly IExtractedTextValidator _validator;
        public DocumentService(IDocumentTextExtractor textExtractor, IExtractedTextValidator validator) 
        { 
            _textExtractor = textExtractor;
            _validator = validator;
        }

        public Task<String> ExtractText(Stream stream)
        {
            string text = _textExtractor.ExtractText(stream);
            if(!_validator.IsTextValid(text).IsValid)
            {
                string errorText = _validator.IsTextValid(text).ErrorMessage ?? "";
                return Task.FromResult(errorText);
            }
            return Task.FromResult(text);
        }

    }
}
