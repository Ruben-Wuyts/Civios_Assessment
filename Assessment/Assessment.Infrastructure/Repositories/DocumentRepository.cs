using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Infrastructure.DatabaseContext;


namespace Assessment.Infrastructure.Repositories
{
    public class DocumentRepository: IDocumentRepository
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

        public async Task<Document> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
