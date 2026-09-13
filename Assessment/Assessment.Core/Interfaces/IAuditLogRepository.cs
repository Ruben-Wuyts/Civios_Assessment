using Assessment.Core.Entities;

namespace Assessment.Core.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog auditLog);
        Task<List<AuditLog>> GetByDocumentIdAsync(int documentId);
    }
}
