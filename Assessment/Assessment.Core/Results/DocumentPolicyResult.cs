
using Assessment.Core.Enums;

namespace Assessment.Core.Results
{
    public class DocumentPolicyResult
    {
        public AccessLevel AccessLevel { get; init; }
        public DateTime RetentionUntil { get; init; }
    }
}
