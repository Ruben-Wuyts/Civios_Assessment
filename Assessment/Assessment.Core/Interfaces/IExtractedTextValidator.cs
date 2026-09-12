using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IExtractedTextValidator
    {
        public ExtractedTextResult IsTextValid(string text);
    }
}
