using Assessment.Core.Enums;

namespace Assessment.Core.Results
{
    public class DataClassificationResult
    {
        public DataClassification Classification { get; init; }
        public string Reason { get; init; }

        public DataClassificationResult(DataClassification classification, string reason)
        {
            Classification = classification;
            Reason = reason;
        }

    }
}
