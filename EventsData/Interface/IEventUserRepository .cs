using EventsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsData.Interface
{
    public interface IEventUserRepository
    {
        IEnumerable<EventUser> GetAllRegistrations();
        IEnumerable<EventUser> GetRegistrationsByEventId(int eventId);
        IEnumerable<EventUser> GetRegistrationsByUserId(int userId);
        IEnumerable<string> GetUserNamesByEventId(int eventId);

        void RegisterUserToEvent(EventUser registration);
        void DeleteRegistration(int eventId, int userId);
        void Save();
    }
}