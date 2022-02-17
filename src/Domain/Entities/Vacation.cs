using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Vacation : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public DateTime? VacationStart { get; set; }
        public DateTime? VacationEnd { get; set; }

        /* EF Relation */
        public AppUser User { get; set; }
        public Guid UserId { get; set; }
    }
}