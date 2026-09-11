using Assessment.Core.Interfaces;
using Assessment.Core.Results;
using Microsoft.AspNetCore.Http;

namespace Assessment.Core.Validators
{
    public class FileUploadValidator : IFileUploadValidator
    {
        FileValidationResult validationResult { get; set; }

        public FileValidationResult ValidateFile(string fileName, long fileSize)
        {
            validationResult = FileExtensionAllowed(fileName);
            validationResult = FileNotEmpty(fileSize);
            validationResult = FileSizeAllowed(fileSize);
            return validationResult;
        }

        private FileValidationResult FileExtensionAllowed(string fileName)
        {
            string extension = fileName.Substring(fileName.Length - 3);
            if (!string.IsNullOrWhiteSpace(extension) && extension == "pdf") 
            {
                return validationResult;
            }
            else
            {
                return validationResult;
            }
        }

        private FileValidationResult FileNotEmpty(long fileSize)
        {
            if (fileSize > 0)
            {
                return validationResult;
            }
            else
            {
                return validationResult;
            }
        }

        private FileValidationResult FileSizeAllowed(long fileSize)
        {
            if (fileSize > 100)
            {
                return validationResult;
            }
            else 
            { 
                return validationResult; 
            }
        }
    }
}
