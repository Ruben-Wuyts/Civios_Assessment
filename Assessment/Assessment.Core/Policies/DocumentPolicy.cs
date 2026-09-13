using Assessment.Core.Enums;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;

namespace Assessment.Core.Policies
{
    public class DocumentPolicy : IDocumentPolicy
    {
        public DocumentPolicyResult DeterminePolicy(
            DataClassification classification,
            DateTime fromDate)
        {
            return classification switch
            {
                DataClassification.PublicData => new DocumentPolicyResult
                {
                    AccessLevel = AccessLevel.Public,
                    RetentionUntil = fromDate.AddYears(1)
                },

                DataClassification.InternalData => new DocumentPolicyResult
                {
                    AccessLevel = AccessLevel.Internal,
                    RetentionUntil = fromDate.AddYears(3)
                },

                DataClassification.PersonalData => new DocumentPolicyResult
                {
                    AccessLevel = AccessLevel.Restricted,
                    RetentionUntil = fromDate.AddYears(5)
                },

                DataClassification.SensitivePersonalData => new DocumentPolicyResult
                {
                    AccessLevel = AccessLevel.HighlyRestricted,
                    RetentionUntil = fromDate.AddYears(10)
                },

                _ => throw new ArgumentOutOfRangeException(
                    nameof(classification),
                    classification,
                    "Unknown data classification.")
            };
        }
    }
}
