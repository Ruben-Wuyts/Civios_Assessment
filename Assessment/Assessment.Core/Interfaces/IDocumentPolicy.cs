using Assessment.Core.Enums;
using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentPolicy
    {
        DocumentPolicyResult DeterminePolicy(DataClassification classification, DateTime fromDate);
    }
}
