using Assessment.Core.Enums;

namespace Assessment.Core.Results
{
    public class DocumentAnalysisResult
    {
        public int DocumentId { get; init; }
        public DataClassificationResult ClassificationResult { get; init; } = null!;
        public DocumentStatus Status {  get; init; }
    }
}
