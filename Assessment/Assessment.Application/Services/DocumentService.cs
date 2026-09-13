using Assessment.Core.Entities;
using Assessment.Core.Enums;
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
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IDocumentPolicy _policy;

        public DocumentService
            (IDocumentTextExtractor textExtractor, IExtractedTextValidator textValidator, IDocumentClassifier classifier, 
            IDocumentStorage documentStorage, IDocumentRepository documentRepository, IAuditLogRepository auditLogRepository, IDocumentPolicy policy)
        {
            _textExtractor = textExtractor;
            _textValidator = textValidator;
            _classifier = classifier;
            _documentStorage = documentStorage;
            _documentRepository = documentRepository;
            _auditLogRepository = auditLogRepository;
            _policy = policy;
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

            var policyResult = _policy.DeterminePolicy(classificationResult.Classification, DateTime.UtcNow);

            var document = new Document(fileName, classificationResult.Classification, classificationResult.Reason, metadata);

            document.StoragePath = temporaryPath;
            document.AccessLevel = policyResult.AccessLevel;
            document.RetentionUntil = policyResult.RetentionUntil;

            var savedDocument = await _documentRepository.AddAsync(document);

            var auditLog = new AuditLog
            {
                DocumentId = savedDocument.Id,
                Action = AuditAction.Analyzed,
                Timestamp = DateTime.UtcNow,
                Details = $"Classified as {classificationResult.Classification}. {classificationResult.Reason}"
            };

            await _auditLogRepository.AddAsync(auditLog);

            return new DocumentAnalysisResult
            {
                DocumentId = savedDocument.Id,
                ClassificationResult = classificationResult,
                Status = savedDocument.Status,
            };
        }

        public async Task<DocumentStoreResult> StoreDocumentAsync(int id)
        {
            var foundDocument = await _documentRepository.GetByIdAsync(id);
            if (foundDocument is null)
            {
                return DocumentStoreResult.Fail("No document found with given id");
            }
            if (foundDocument.Status != DocumentStatus.Analyzed)
            {
                return DocumentStoreResult.Fail(
                    "Document is not ready to be stored.");
            }
            if (string.IsNullOrWhiteSpace(foundDocument.StoragePath))
            {
                return DocumentStoreResult.Fail(
                    "Document has no temporary storage path.");
            }

            var finalPath = await _documentStorage.MoveToFinalStorageAsync(foundDocument.StoragePath, foundDocument.Classification);

            foundDocument.StoragePath = finalPath;
            foundDocument.Status = DocumentStatus.Stored;

            await _documentRepository.UpdateAsync(foundDocument);

            var auditLog = new AuditLog
            {
                DocumentId = foundDocument.Id,
                Action = AuditAction.Stored,
                Timestamp = DateTime.UtcNow,
                Details = $"Stored in {foundDocument.Classification} storage."
            };

            await _auditLogRepository.AddAsync(auditLog);

            return DocumentStoreResult.Success(foundDocument);
        }

        public async Task<List<AuditLog>> GetAllAuditLogsByDocumentIdAsync(int documentId)
        {
            return await _auditLogRepository.GetByDocumentIdAsync(documentId);
        }

    }
}
