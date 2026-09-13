using Assessment.Core.Enums;

namespace Assessment.Presentation.Requests
{
    public class AnalyzeDocumentRequest
    {
        public IFormFile File { get; set; } = default!;

        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public IntendedVisibility Visibility { get; set; }
    }
}
