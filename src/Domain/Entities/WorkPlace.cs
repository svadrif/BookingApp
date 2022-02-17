using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class WorkPlace : BaseEntity<Guid>
    {
        public string Number { get; set; }
        public SeatsType Type { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        public bool IsBlocked { get; set; }

        /* EF Relation */
        public IEnumerable<Booking> Bookings { get; set; }

        public Guid MapId { get; set; }
        public Map Map { get; set; }
    }
}