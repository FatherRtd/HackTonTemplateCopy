using HackTonTemplate.Models;
using ISession = NHibernate.ISession;
using NHibernate.Linq;

namespace HackTonTemplate.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEvent(long id);
        Task<IEnumerable<EventType>> GetEventTypes();
        Task<List<EventCategory>> GetEventCategories();
        Task<Event> AddOrUpdateEvent(Event eventDto);
        Task<IEnumerable<Event>> GetUserEvent(long userId);
    }

    public class EventService : IEventService
    {
        private readonly ISession _session;
        public EventService(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            using var db = _session.BeginTransaction();

            return await _session.Query<Event>()
                                 .Fetch(x => x.CreateUser)
                                 .Fetch(x => x.Category)
                                 .Fetch(x => x.Type)
                                 .ToListAsync();
        }

        public async Task<Event> GetEvent(long id)
        {
            using var db = _session.BeginTransaction();

            return await _session.Query<Event>()
                                 .Fetch(x => x.CreateUser)
                                 .Fetch(x => x.Category)
                                 .Fetch(x => x.Type)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<EventType>> GetEventTypes()
        {
            using var db = _session.BeginTransaction();

            return await _session.Query<EventType>()
                                 .ToListAsync();
        }

        public async Task<List<EventCategory>> GetEventCategories()
        {
            using var db = _session.BeginTransaction();

            return await _session.Query<EventCategory>()
                                 .ToListAsync();
        }

        public async Task<Event> AddOrUpdateEvent(Event eventDto)
        {
            using var db = _session.BeginTransaction();

            var eventToSave = await _session.Query<Event>().FirstOrDefaultAsync(x => x.Id == eventDto.Id) ?? new Event();

            eventToSave.Address = eventDto.Address;
            eventToSave.StartDate = eventDto.StartDate;
            eventToSave.EndDate = eventDto.EndDate;
            eventToSave.Category = eventDto.Category;
            eventToSave.Type = eventDto.Type;
            eventToSave.Desctiption = eventDto.Desctiption;
            eventToSave.ImageUrl = eventDto.ImageUrl;
            eventToSave.SiteUrl = eventDto.SiteUrl;
            eventToSave.IsOnline = eventDto.IsOnline;
            eventToSave.Name = eventDto.Name;

            eventDto.Id = (long)await _session.SaveAsync(eventToSave);
            await db.CommitAsync();

            return eventDto;
        }

        public async Task<IEnumerable<Event>> GetUserEvent(long userId)
        {
            using var db = _session.BeginTransaction();

            var eventIds = await _session.Query<UserEvent>()
                                         .Where(x => x.UserId == userId)
                                         .Select(x => x.EventId)
                                         .ToListAsync();

            return await _session.Query<Event>()
                                 .Where(x => eventIds.Contains(x.Id))
                                 .ToListAsync();
        }
    }
}
