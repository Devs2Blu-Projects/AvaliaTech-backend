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
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
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

            var events = await _context.Events.FindAsync(id) ?? throw new Exception($"Evento não encontrado! ({request.Id})");

            events.Name = request.Name;
            events.StartDate = request.StartDate;
            events.EndDate = request.EndDate;
            events.IsClosed = request.IsClosed;
            events.IsPublic = request.IsPublic;

            await _context.SaveChangesAsync();

        }
    }
}
