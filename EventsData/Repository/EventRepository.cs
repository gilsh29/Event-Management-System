using EventsData.Interface;
using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Repository
{
        public class EventRepository : IEventRepository
        {
            private readonly EventDbContext _context;

            public EventRepository(EventDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Event> GetAllEvents()
            {
                return _context.Events.ToList();
            }

            public Event GetEventById(int id)
            {
                return _context.Events.FirstOrDefault(e => e.ID == id);
            }

            public void AddEvent(Event newEvent)
            {
                _context.Events.Add(newEvent);
            }

            public void UpdateEvent(Event updatedEvent)
            {
                _context.Events.Update(updatedEvent);
            }

            public void DeleteEvent(int id)
            {
                var existing = _context.Events.Find(id);
                if (existing != null)
                    _context.Events.Remove(existing);
            }
            public IEnumerable<Event> GetEventsBetweenDates(DateTime startDate, DateTime endDate)
            {
                return _context.Events
                    .Where(e => e.StartDate >= startDate && e.EndDate <= endDate)
                    .ToList();
            }

        public void Save()
            {
                _context.SaveChanges();
            }
        }
    }
