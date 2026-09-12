
namespace Assessment.Core.Results
{
    public class ExtractedTextResult
    {
        public bool IsValid { get; init; }
        public string? ExtractedText { get; init; }
        public string? ErrorMessage { get; init; }


        public static ExtractedTextResult Success(string extractedText)
        {
            return new ExtractedTextResult
            {
                IsValid = true,
                ExtractedText = extractedText
            };
        }
        public static ExtractedTextResult Fail(string errorMessage)
        {
            return new ExtractedTextResult
            {
                IsValid = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
