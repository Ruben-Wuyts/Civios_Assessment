using Assessment.Core.Interfaces;
using Assessment.Core.Results;

namespace Assessment.Core.Validators
{
    public class FileUploadValidator : IFileUploadValidator
    {
        private const long MaxFileSize = 10 * 1024 * 1024;

        public FileValidationResult ValidateFile(string fileName, long fileSize)
        {

            var result = FileExtensionAllowed(fileName);

            if (!result.IsValid)
                return result;

            result = FileSizeAllowed(fileSize);

            if (!result.IsValid)
                return result;

            return FileValidationResult.Success();
        }

        private static FileValidationResult FileExtensionAllowed(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (!string.IsNullOrWhiteSpace(extension) && extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                return FileValidationResult.Success();
            }
            return FileValidationResult.Fail("Extension is not allowed.");
        }

        private static FileValidationResult FileSizeAllowed(long fileSize)
        {
            if (fileSize <= MaxFileSize)
            {
                return FileValidationResult.Success();
            }
            return FileValidationResult.Fail("File size is too large.");
        }
    }
}
