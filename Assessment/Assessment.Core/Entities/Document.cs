using Assessment.Core.Enums;

namespace Assessment.Core.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string? StoragePath { get; set; }
        public DataClassification Classification { get; set; }
        public string ClassificationReason { get; set; } = string.Empty;
        public DocumentMetadata Metadata { get; set; } = null!;
        public DocumentStatus Status { get; set; } = DocumentStatus.Analyzed;
        public AccessLevel AccessLevel { get; set; }
        public DateTime RetentionUntil { get; set; }

        private Document()
        {
            //EF core
        }

        public Document(string fileName, DataClassification classification, string classificationReason, DocumentMetadata metadata)
        {
            FileName = fileName;
            Classification = classification;
            ClassificationReason = classificationReason;
            Metadata = metadata;
        }

    }
}
