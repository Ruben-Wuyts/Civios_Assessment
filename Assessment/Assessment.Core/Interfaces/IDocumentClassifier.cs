using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentClassifier
    {
        public DataClassificationResult AssignClassification(string text);
    }
}
