using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class Vacation: IHasKey<long>
    {
        public long Id { get; set; }
        public DateTime? VacationStart { get; set; }
        public DateTime? VacationEnd { get; set; }

        /* EF Relation */
        public AppUser User { get; set; }
        public long UserId { get; set; }
    }
}
