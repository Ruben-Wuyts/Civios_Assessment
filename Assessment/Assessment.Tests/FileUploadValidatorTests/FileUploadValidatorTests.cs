

using Assessment.Core.Validators;

namespace Assessment.Tests.FileUploadValidatorTests
{
    public class FileUploadValidatorTests
    {
        [Fact]
        public void ValidateFile_WhenFileIsValidPdf_ReturnsValid()
        {
            var validator = new FileUploadValidator();

            var result = validator.ValidateFile(
                "document.pdf",
                1024);

            Assert.True(result.IsValid);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void ValidateFile_WhenExtensionIsNotPdf_ReturnsInvalid()
        {
            var validator = new FileUploadValidator();

            var result = validator.ValidateFile(
                "document.docx",
                1024);

            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void ValidateFile_WhenFileIsLargerThanTenMb_ReturnsInvalid()
        {
            var validator = new FileUploadValidator();

            var result = validator.ValidateFile(
                "document.pdf",
                10 * 1024 * 1024 + 1);

            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void ValidateFile_WhenPdfExtensionUsesUppercase_ReturnsValid()
        {
            var validator = new FileUploadValidator();

            var result = validator.ValidateFile(
                "document.PDF",
                1024);

            Assert.True(result.IsValid);
        }
    }
}
