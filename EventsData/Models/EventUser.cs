using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Models
{
    public class EventUser
    {
        public int EventRef { get; set; }           // Foreign key to Events.ID
        public int UserRef { get; set; }            // Foreign key to Users.ID
        public DateTime Creation { get; set; }      // Timestamp of the relationship

        // Navigation properties (optional but recommended for EF Core relationships)
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
