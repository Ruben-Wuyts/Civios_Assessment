using Assessment.Core.Interfaces;
using Assessment.Core.Results;

namespace Assessment.Core.Validators
{
    public class ExtractedTextValidator : IExtractedTextValidator
    {
        public ExtractedTextResult IsTextValid(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return ExtractedTextResult.Fail("File does not contain readable text.");
            }

            return ExtractedTextResult.Success(text);
        }
    }
}
