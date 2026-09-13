using Assessment.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.DatabaseContext
{
    public class AssessmentDbContext : DbContext
    {
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Document>()
                .OwnsOne(document => document.Metadata);

            modelBuilder.Entity<AuditLog>()
                .HasOne<Document>()
                .WithMany()
                .HasForeignKey(auditLog => auditLog.DocumentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
