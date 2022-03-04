using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Vacation : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public DateTimeOffset? VacationStart { get; set; }
        public DateTimeOffset? VacationEnd { get; set; }

        /* EF Relation */
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}