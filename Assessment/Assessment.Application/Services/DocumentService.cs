using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;
namespace Assessment.Application.Services
{
    public class DocumentService: IDocumentService
    {
        private readonly IDocumentTextExtractor _textExtractor;
        private readonly IExtractedTextValidator _textValidator;
        private readonly IDocumentClassifier _classifier;
        public DocumentService(IDocumentTextExtractor textExtractor, IExtractedTextValidator textValidator, IDocumentClassifier classifier)
        {
            _textExtractor = textExtractor;
            _textValidator = textValidator;
            _classifier = classifier;
        }

        public Task<ExtractedTextResult> ExtractText(Stream stream)
        {
            string text = _textExtractor.ExtractText(stream);
            return Task.FromResult(_textValidator.IsTextValid(text));
            
        }

        public Task<DataClassificationResult> Classify(string text, DocumentMetadata documentMetadata) 
        {
            return Task.FromResult(_classifier.AssignClassification(text, documentMetadata));
        }

    }
}
