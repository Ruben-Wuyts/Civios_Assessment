

using Assessment.Core.Validators;

namespace Assessment.Tests.ExtractedTextValidatorTests
{
    public class ExtractedTextValidatorTests
    {
        [Fact]
        public void IsTextValid_WhenTextContainsContent_ReturnsValid()
        {
            var validator = new ExtractedTextValidator();

            var result = validator.IsTextValid("Dit document bevat tekst.");

            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void IsTextValid_WhenTextIsEmpty_ReturnsInvalid()
        {
            var validator = new ExtractedTextValidator();

            var result = validator.IsTextValid("");

            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void IsTextValid_WhenTextContainsOnlyWhitespace_ReturnsInvalid()
        {
            var validator = new ExtractedTextValidator();

            var result = validator.IsTextValid("   ");

            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }
    }
}
