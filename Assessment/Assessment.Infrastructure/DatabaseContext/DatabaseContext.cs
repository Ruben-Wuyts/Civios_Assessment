using Assessment.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.DatabaseContext
{
    public class DatabaseContext: DbContext
    {
        public virtual DbSet<Document> Documents { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .OwnsOne(document => document.Metadata);
        }
    }
}
