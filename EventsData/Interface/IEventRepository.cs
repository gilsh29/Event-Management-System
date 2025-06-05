using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Interface
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);
        void AddEvent(Event newEvent);
        void UpdateEvent(Event updatedEvent);
        void DeleteEvent(int id);

        IEnumerable<Event> GetEventsBetweenDates(DateTime startDate, DateTime endDate);

        void Save();
    }
}