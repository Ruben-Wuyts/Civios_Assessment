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
        private readonly IDocumentStorage _documentStorage;
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentTextExtractor textExtractor, IExtractedTextValidator textValidator, IDocumentClassifier classifier, IDocumentStorage documentStorage, IDocumentRepository documentRepository)
        {
            _textExtractor = textExtractor;
            _textValidator = textValidator;
            _classifier = classifier;
            _documentStorage = documentStorage;
            _documentRepository = documentRepository;
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

        public async Task<DocumentAnalysisResult> SaveAnalyzedDocumentAsync(Stream stream, string fileName, DocumentMetadata metadata, DataClassificationResult classificationResult)
        {
            var temporaryPath = await _documentStorage.SaveTemporaryAsync(stream, fileName);

            var document = new Document(fileName, classificationResult.Classification, classificationResult.Reason, metadata);

            document.StoragePath = temporaryPath;

            var savedDocument = await _documentRepository.AddAsync(document);

            return new DocumentAnalysisResult
            {
                DocumentId = savedDocument.Id,
                ClassificationResult = classificationResult,
                Status = savedDocument.Status,
            };
        }

    }
}
