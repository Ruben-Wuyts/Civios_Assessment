using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IFileUploadValidator
    {
        public FileValidationResult ValidateFile(string fileName, long fileSize);
    }
}
