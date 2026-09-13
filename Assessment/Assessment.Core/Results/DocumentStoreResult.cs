using Assessment.Core.Entities;

namespace Assessment.Core.Results
{
    public class DocumentStoreResult
    {
        public Document? Document { get; init; }
        public string? ErrorMessage { get; init; }

        public static DocumentStoreResult Success(Document document)
            => new() { Document = document };

        public static DocumentStoreResult Fail(string message)
            => new()
            {
                ErrorMessage = message
            };
    }
    
}
