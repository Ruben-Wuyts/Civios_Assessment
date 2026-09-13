using Assessment.Core.Entities;
using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentClassifier
    {
        public DataClassificationResult AssignClassification(string text, DocumentMetadata metadata);
    }
}
