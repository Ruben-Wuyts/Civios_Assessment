using Assessment.Core.Entities;
using Assessment.Core.Enums;

namespace Assessment.Core.Results
{
    public class DocumentStoreResult
    {
        public int DocumentId { get; init; }
        public string FileName { get; init; } = string.Empty;
        public DataClassification Classification { get; init; }
        public string ClassificationReason { get; init; } = string.Empty;
        public DocumentStatus Status { get; init; }
        public AccessLevel AccessLevel { get; init; }
        public DateTime RetentionUntil { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;
        public DocumentStoreError? Error { get; init; }

        public static DocumentStoreResult Success(Document document)
        => new()
        {
            DocumentId = document.Id,
            FileName = document.FileName,
            Classification = document.Classification,
            ClassificationReason = document.ClassificationReason,
            Status = document.Status,
            AccessLevel = document.AccessLevel,
            RetentionUntil = document.RetentionUntil
        };

        public static DocumentStoreResult Fail(string message, DocumentStoreError error)
        {
            return new DocumentStoreResult
            {
                ErrorMessage = message,
                Error = error
            };

        }
    }

}
