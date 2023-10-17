using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;

        public EventService(DataContext context) { _context = context; }

        async public Task<IEnumerable<EventModel>> GetEvents()
        {
            return await _context.Events.OrderByDescending(e => e.StartDate).ToListAsync();
        }

        async public Task<EventModel?> GetEventById(int id)
        {
            return await _context.Events
                .Include(e => e.Propositions)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        async public Task CreateEvent(EventDtoInsert request)
        {
            var model = new EventModel
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };
            await _context.Events.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteEvent(int id)
        {
            var events = await _context.Events.FindAsync(id) ?? throw new Exception($"Evento não encontrado! ({id})");

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();

        }

        async public Task UpdateEvent(int id, EventDtoUpdate request)
        {
            if (request.Id != id) throw new Exception("Id diferente do evento informado!");

            var @event = await _context.Events.FindAsync(id) ?? throw new Exception($"Evento não encontrado! ({request.Id})");

            @event.Name = request.Name;
            @event.StartDate = request.StartDate;
            @event.EndDate = request.EndDate;
            @event.IsClosed = request.IsClosed;
            @event.IsPublic = request.IsPublic;

            await _context.SaveChangesAsync();

            await CatchGroupsOutsideEventDateRange(@event);
        }

        async private Task CatchGroupsOutsideEventDateRange(EventModel @event)
        {
            int maxDayOffset = @event.EndDate.Subtract(@event.StartDate).Days;

            var groups = await _context.Groups.Where(g => g.EventId == @event.Id && g.DateOffset > (uint)maxDayOffset).ToListAsync();

            foreach (var group in groups)
            {
                group.DateOffset = (uint)maxDayOffset;
            }

            await _context.SaveChangesAsync();
        }
    }
}
