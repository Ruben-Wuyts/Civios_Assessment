using Assessment.Core.Entities;
using Assessment.Core.Interfaces;
using Assessment.Core.Results;
using Assessment.Infrastructure.DAO;
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

        public async Task<DocumentByIdResult> GetByIdAsync(int id)
        {
            Document document = await _context.Documents.FindAsync(id);

            if (document == null)
            {
                return new DocumentByIdResult("No document found with given Id.");
            }
            return new DocumentByIdResult(document);

        }
    }
}
