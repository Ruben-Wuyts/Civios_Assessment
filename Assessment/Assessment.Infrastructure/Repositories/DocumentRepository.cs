using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Infrastructure.DatabaseContext;


namespace Assessment.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AssessmentDbContext _context;

        public DocumentRepository(AssessmentDbContext context)
        {
            _context = context;
        }

        public async Task<Document> AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);

        }

        public async Task UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }
    }
}
