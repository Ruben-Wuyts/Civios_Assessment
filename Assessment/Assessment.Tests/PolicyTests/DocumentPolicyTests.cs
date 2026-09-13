using Assessment.Core.Enums;
using Assessment.Core.Policies;

namespace Assessment.Tests.PolicyTests
{
    public class DocumentPolicyTests
    {
        [Fact]
        public void DeterminePolicy_WhenClassificationIsPersonalData_ReturnsRestrictedAccess()
        {
            var policy = new DocumentPolicy();
            var startDate = new DateTime(2026, 9, 13);

            var result = policy.DeterminePolicy(
                DataClassification.PersonalData,
                startDate);

            Assert.Equal(
                AccessLevel.Restricted,
                result.AccessLevel);
        }

        [Fact]
        public void DeterminePolicy_WhenClassificationIsPersonalData_ReturnsFiveYearRetention()
        {
            var policy = new DocumentPolicy();
            var startDate = new DateTime(2026, 9, 13);

            var result = policy.DeterminePolicy(
                DataClassification.PersonalData,
                startDate);

            Assert.Equal(
                new DateTime(2031, 9, 13),
                result.RetentionUntil);
        }

        [Fact]
        public void DeterminePolicy_WhenClassificationIsSensitivePersonalData_ReturnsHighlyRestrictedAccessAndReturnsTenYearRetention()
        {
            var policy = new DocumentPolicy();
            var startDate = new DateTime(2026, 9, 13);

            var result = policy.DeterminePolicy(
                DataClassification.SensitivePersonalData,
                startDate);

            Assert.Equal(
                AccessLevel.HighlyRestricted,
                result.AccessLevel);
            Assert.Equal(
                new DateTime(2036, 9, 13),
                result.RetentionUntil);
        }
    }
}
