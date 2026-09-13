using Assessment.Core.Entities;
using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentService
    {
        public Task<ExtractedTextResult> ExtractText(Stream stream);
        public Task<DataClassificationResult> Classify(string text, DocumentMetadata documentMetadata);
        public Task<DocumentAnalysisResult> SaveAnalyzedDocumentAsync(Stream stream, string fileName, DocumentMetadata metadata, DataClassificationResult classificationResult);
        public Task<DocumentStoreResult> StoreDocumentAsync(int id);
        public Task<List<AuditLog>> GetAllAuditLogsByDocumentIdAsync(int documentId);
    }
}
