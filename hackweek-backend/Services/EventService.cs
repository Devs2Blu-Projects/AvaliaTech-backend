using Azure.Core;
using hackweek_backend.Data;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hackweek_backend.Services
{
    public class EventService : IEventService
    {
        private readonly DataContext _context;

        public EventService(DataContext context) { _context = context; }

        async public Task<IEnumerable<EventModel>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        async public Task<EventModel?> GetEventById(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        async public Task CreateEvent(EventModel request)
        {
            await _context.Events.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteEvent(int id)
        {
            var events = await _context.Events.FindAsync(id) ?? throw new Exception($"Evento não encontrado! ({id})");

            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
          
        }

        async public Task UpdateEvent(int id, EventModel request)
        {
            var model = await _context.Events.FindAsync(id) ?? throw new Exception($"Evento não encontrado! ({id})");

            _context.Entry(request).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
