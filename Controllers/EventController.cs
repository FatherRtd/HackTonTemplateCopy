using HackTonTemplate.Models;
using Microsoft.AspNetCore.Mvc;
using HackTonTemplate.Services;
using Microsoft.Extensions.Logging;

namespace HackTonTemplate.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Route("api/event/get")]
        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            var events = await _eventService.GetEvents();

            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");

            if (userIdClaim == null)
                return events;

            long userId = long.Parse(userIdClaim!.Value);

            var userEvents = await _eventService.GetUserEvent(userId);

            var userEventIds = userEvents.Select(x => x.Id);

            var eventDtos = events.ToList();
            for (var i = 0; i < eventDtos.Count(); i++)
            {
                eventDtos[i].UserIsRegister = userEventIds.Contains(eventDtos[i].Id);
            }

            return eventDtos;
        }

        [Route("api/event/getUserEvents")]
        [HttpGet]
        public async Task<IEnumerable<Event>> GetUserEvents()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");
            long userId = long.Parse(userIdClaim!.Value);

            var userEvents = await _eventService.GetUserEvent(userId);

            var userEventIds = userEvents.Select(x => x.Id);

            var eventDtos = userEvents.ToList();
            for (var i = 0; i < eventDtos.Count(); i++)
            {
                eventDtos[i].UserIsRegister = userEventIds.Contains(eventDtos[i].Id);
            }

            return userEvents;
        }

        [Route("api/event/get/{id}")]
        [HttpGet]
        public async Task<Event> GetEvent(long id)
        {
            return await _eventService.GetEvent(id);
        }

        [Route("api/event-types")]
        [HttpGet]
        public async Task<IEnumerable<EventType>> GetEventTypes()
        {
            return await _eventService.GetEventTypes();
        }

        [Route("api/event-categories")]
        [HttpGet]
        public async Task<List<EventCategory>> GetEventCategories()
        {
            return await _eventService.GetEventCategories();
        }

        [Route("api/event/save")]
        [HttpPost]
        public async Task<Event> Save([FromBody]Event eventDto)
        {
            return await _eventService.AddOrUpdateEvent(eventDto);
        }
    }
}
