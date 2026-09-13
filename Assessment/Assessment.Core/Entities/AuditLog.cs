using Assessment.Core.Enums;

namespace Assessment.Core.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public AuditAction Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty;
    }
}
