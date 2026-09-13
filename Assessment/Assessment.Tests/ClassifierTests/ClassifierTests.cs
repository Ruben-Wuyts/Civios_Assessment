using Assessment.Core.Components;
using Assessment.Core.Entities;
using Assessment.Core.Enums;

namespace Assessment.Tests.ClassifierTests
{
    public class ClassifierTests
    {
        [Fact]
        public void AssignClassification_WhenTextContainsSensitiveKeyword_ReturnsSensitivePersonalData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Public);

            var text = "Dit is een medisch verslag.";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.SensitivePersonalData,
                result.Classification);

        }

        [Fact]
        public void AssignClassification_WhenTextContainsPersonalKeyword_ReturnsPersonalData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Public);

            var text = "Naam: Jan Janssens";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.PersonalData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenTextContainsInternalKeyword_ReturnsInternalData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Public);

            var text = "Verslag vergadering";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.InternalData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenTextContainsPublicKeyword_ReturnsPublicData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Public);

            var text = "Brochure 'groen in jouw omgeving'";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.PublicData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenTextContainsNoKeywordAndNoSignificantMetadata_ReturnsPublicData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Public);

            var text = "Testcontent";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.PublicData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenTextContainsNoKeywordAndContainsInternalDescriptionMetadata_ReturnsInternalData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Verslag vergadering",
                IntendedVisibility.Public);

            var text = "Testcontent";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.InternalData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenIntendedVisibilityIsInternal_ReturnsInternalData()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Testdocument",
                DateTime.UtcNow,
                "Testbeschrijving",
                IntendedVisibility.Internal);

            var text = "Testcontent";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.InternalData,
                result.Classification);
        }

        [Fact]
        public void AssignClassification_WhenMultipleClassificationSignalsExist_ReturnsHighestClassification()
        {
            var classifier = new Classifier();

            var metadata = new DocumentMetadata(
                "Ruben",
                "Vergadering",
                DateTime.UtcNow,
                "Naam van medewerker",
                IntendedVisibility.Internal);

            var text = "Dit document bevat een medisch verslag.";

            var result = classifier.AssignClassification(text, metadata);

            Assert.Equal(
                DataClassification.SensitivePersonalData,
                result.Classification);
        }
    }
}
