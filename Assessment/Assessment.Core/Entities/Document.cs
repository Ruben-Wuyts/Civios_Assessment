using Assessment.Core.Enums;

namespace Assessment.Core.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string StoragePath { get; set; }
        public DataClassification Classification { get; set; }
        public string ClassificationReason { get; set; }
        public DocumentMetadata Metadata { get; set; }

        public Document(string storagePath, DataClassification classification, string fileName, string classificationReason, DocumentMetaData metaData)
        {
            StoragePath = storagePath;
            Classification = classification;
            FileName = fileName;
            ClassificationReason = classificationReason;
            Metadata = metaData;
        }

    }
}
