using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AssessmentDbContext _context;

        public AuditLogRepository(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetByDocumentIdAsync(int documentId)
        {
            return await _context.AuditLogs
                .AsNoTracking()
                .Where(auditLog => auditLog.DocumentId == documentId)
                .OrderBy(auditLog => auditLog.Timestamp)
                .ToListAsync();

        }
    }
}
