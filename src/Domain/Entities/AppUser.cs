using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class AppUser : BaseEntity<Guid>
    {
        public long TelegramId { get; set; }
        public Guid StateId { get; set; }
        public string LastCommand { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime EmploymentStart { get; set; }
        public DateTime? EmploymentEnd { get; set; }
        public bool isDeleted { get; set; }

        /* EF Relation */
        public IEnumerable<Vacation> Vacations { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

        public Guid? PrefferdWorkPlaceId { get; set; }
        public WorkPlace WorkPlace { get; set; }
    }
}