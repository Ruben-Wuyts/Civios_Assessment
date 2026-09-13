using Assessment.Core.Entities;
using Assessment.Core.Results;

namespace Assessment.Core.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> AddAsync(Document document);
        Task<Document?> GetByIdAsync(int id);
        Task UpdateAsync(Document document);
    }
}
