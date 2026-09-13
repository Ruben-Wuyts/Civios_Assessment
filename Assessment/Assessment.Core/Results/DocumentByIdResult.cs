using Assessment.Core.Entities;

namespace Assessment.Core.Results
{
    public class DocumentByIdResult
    {
        public Document Document { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public DocumentByIdResult(Document document)
        {
            Document = document;
        }

        public DocumentByIdResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
