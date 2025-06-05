using Microsoft.AspNetCore.Mvc;
using EventsData.Interface;
using EventsData.Models;
using EventsAPI.DTOs;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("event/{eventId}/registration")]
    public class RegistrationController : ControllerBase
    {
        private readonly IEventUserRepository _eventUserRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;

        public RegistrationController(
            IEventUserRepository eventUserRepository,
            IEventRepository eventRepository,
            IUserRepository userRepository)
        {
            _eventUserRepository = eventUserRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        // ✅ GET /event/{eventId}/registration
        [HttpGet]
        public IActionResult GetUserNamesForEvent(int eventId)
        {
            var userNames = _eventUserRepository.GetUserNamesByEventId(eventId);

            if (!userNames.Any())
                return NotFound("No users registered for this event.");

            return Ok(userNames);
        }

        // POST /event/{eventId}/registration
        [HttpPost]
        public IActionResult RegisterUserToEvent(int eventId, [FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            // Validate event
            var eventExists = _eventRepository.GetEventById(eventId);
            if (eventExists == null)
                return NotFound($"Event with ID {eventId} not found.");

            // Check if user already exists
            var existingUser = _userRepository.GetUserById(user.ID);
            if (existingUser == null)
            {
                // Create new user
                _userRepository.AddUser(user);
                _userRepository.Save();
            }

            // Check if already registered
            var alreadyRegistered = _eventUserRepository
                .GetRegistrationsByEventId(eventId)
                .Any(eu => eu.UserRef == user.ID);

            if (alreadyRegistered)
                return Conflict("User is already registered for this event.");

            // Register user to event
            var registration = new EventUser
            {
                EventRef = eventId,
                UserRef = user.ID,
                Creation = DateTime.UtcNow
            };

            _eventUserRepository.RegisterUserToEvent(registration);
            _eventUserRepository.Save();

            return Created("", $"User {user.ID} registered to event {eventId}.");
        }


    }
}
