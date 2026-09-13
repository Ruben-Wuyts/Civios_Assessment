using Assessment.Core.Enums;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentStorage
    {
        Task<string> SaveTemporaryAsync(Stream stream, string fileName);
        Task<string> MoveToFinalStorageAsync(string currentPath, DataClassification classification);
    }
}
