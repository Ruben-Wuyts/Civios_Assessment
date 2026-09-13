

using Assessment.Core.Entities;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> AddAsync(Document document);
        Task<Document> GetByIdAsync(int id);
    }
}
