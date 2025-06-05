using EventsData.Interface;
using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Repository
{
    public class EventUserRepository : IEventUserRepository
    {
        private readonly EventDbContext _context;

        public EventUserRepository(EventDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EventUser> GetAllRegistrations()
        {
            return _context.EventUser.ToList();
        }
        public IEnumerable<string> GetUserNamesByEventId(int eventId)
        {
            return _context.EventUser
                .Where(eu => eu.EventRef == eventId)
                .Select(eu => eu.User.Name)  // assuming `User` navigation and `Name` field exist
                .ToList();
        }
        public IEnumerable<EventUser> GetRegistrationsByEventId(int eventId)
        {
            return _context.EventUser.Where(eu => eu.EventRef == eventId).ToList();
        }

        public IEnumerable<EventUser> GetRegistrationsByUserId(int userId)
        {
            return _context.EventUser.Where(eu => eu.UserRef == userId).ToList();
        }

        public void RegisterUserToEvent(EventUser registration)
        {
            _context.EventUser.Add(registration);
        }

        public void DeleteRegistration(int eventId, int userId)
        {
            var record = _context.EventUser.FirstOrDefault(eu => eu.EventRef == eventId && eu.UserRef == userId);
            if (record != null)
                _context.EventUser.Remove(record);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}