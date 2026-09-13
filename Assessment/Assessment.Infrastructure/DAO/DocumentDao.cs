using Assessment.Core.Entities;
using Assessment.Core.Enums;

namespace Assessment.Infrastructure.DAO
{
    public class DocumentDao
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string? StoragePath { get; set; }
        public DataClassification Classification { get; set; }
        public string ClassificationReason { get; set; } = string.Empty;
        public DocumentMetadata Metadata { get; set; } = null!;
        public DocumentStatus Status { get; set; } = DocumentStatus.Analyzed;
        public string Errormessage { get; set; } = string.Empty;

        private DocumentDao()
        {
            //EF core
        }

        public DocumentDao(DataClassification classification, string fileName, string classificationReason, DocumentMetadata metaData)
        {
            Classification = classification;
            FileName = fileName;
            ClassificationReason = classificationReason;
            Metadata = metaData;
        }

        public DocumentDao(string fileName, DataClassification classification, string classificationReason, DocumentMetadata metadata)
        {
            FileName = fileName;
            Classification = classification;
            ClassificationReason = classificationReason;
            Metadata = metadata;
        }

        
    }
}

