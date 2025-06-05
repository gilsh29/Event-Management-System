using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventsData.Models
{
    public class User
    {
        public int ID { get; set; }                 // Matches [ID] int IDENTITY(1,1) NOT NULL, primary key
        public string Name { get; set; }            // Matches [Name] nvarchar(50) NOT NULL
        public DateTime DateOfBirth { get; set; }   // Matches [DateOfBirth] date NOT NULL

        [JsonIgnore]

        public ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();



    }
}
