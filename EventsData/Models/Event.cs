using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventsData.Models
{
    public class Event
    {
        public int ID { get; set; }               // Matches [ID] int IDENTITY(1,1) NOT NULL, primary key
        public string Name { get; set; }          // Matches [Name] nvarchar(100) NOT NULL
        public DateTime StartDate { get; set; }   // Matches [StartDate] datetime NOT NULL
        public DateTime EndDate { get; set; }     // Matches [EndDate] datetime NOT NULL
        public int MaxRegistrations { get; set; } // Matches [MaxRegistrations] int NOT NULL
        public string Location { get; set; }      // Matches [Location] nvarchar(50) NOT NULL

        // Navigation
        [JsonIgnore]
        public ICollection<EventUser>? EventUsers { get; set; }
    }


}

