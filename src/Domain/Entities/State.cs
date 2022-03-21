using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Domain.Common;

namespace Domain.Entities
{
    public class State: IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string LastCommand { get; set; } = string.Empty;
        public UserState StateNumber { get; set; } = UserState.NotAuthorized;

        public UserState StateNumber { get; set; }
        public int MessageId { get; set; }
        
        /* EF Relation */
        [ForeignKey("AppUser")]
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
