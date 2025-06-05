using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventsData.Models;
using EventsData.Interface;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]  // This will use the controller name "Event" as the route
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // POST: /event
        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            if (newEvent == null)
                return BadRequest("Event data is null");

            _eventRepository.AddEvent(newEvent);
            _eventRepository.Save();

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.ID }, newEvent);
        }

        // GET: /event/{id}
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var existingEvent = _eventRepository.GetEventById(id);
            if (existingEvent == null)
                return NotFound();

            return Ok(existingEvent);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            if (id != updatedEvent.ID)
                return BadRequest("Event ID in the URL does not match ID in body.");

            var existingEvent = _eventRepository.GetEventById(id);
            if (existingEvent == null)
                return NotFound($"Event with ID {id} not found.");

            // Update fields
            existingEvent.Name = updatedEvent.Name;
            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;
            existingEvent.MaxRegistrations = updatedEvent.MaxRegistrations;
            existingEvent.Location = updatedEvent.Location;

            _eventRepository.UpdateEvent(existingEvent);
            _eventRepository.Save();

            return Ok($"Event with ID {id} updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var existingEvent = _eventRepository.GetEventById(id);
            if (existingEvent == null)
                return NotFound($"Event with ID {id} not found.");

            _eventRepository.DeleteEvent(id);
            _eventRepository.Save();

            return Ok($"Event with ID {id} deleted successfully.");
        }

        [HttpGet("/schedule")]
        public IActionResult GetEventsBetweenDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
                return BadRequest("Start date must be before end date.");

            var events = _eventRepository.GetEventsBetweenDates(startDate, endDate);

            if (!events.Any())
                return NotFound("No events found in the specified date range.");

            return Ok(events);
        }



    }
}
