using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{

    public class AppUser : BaseEntity<Guid>
    {
        public long TelegramId { get; set; }
        public Guid LastCommandId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset? EmploymentEnd { get; set; }
        public Guid? PrefferdWorkPlaceId { get; set; }
        public bool IsDeleted { get; set; }

        /* EF Relation */
        public IEnumerable<Vacation> Vacations { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

    }
}