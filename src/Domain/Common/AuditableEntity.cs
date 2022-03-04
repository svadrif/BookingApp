using System;

namespace Domain.Common
{
    public class AuditableEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}