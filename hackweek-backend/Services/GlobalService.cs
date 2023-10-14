using hackweek_backend.Data;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class GlobalService : IGlobalService
    {
        private readonly DataContext _context;

        public GlobalService(DataContext context) { _context = context; }

        async public Task<GlobalModel> GetGlobal()
        {
            return await _context.Global.FirstAsync();
        }

        async public Task<EventModel?> GetCurrentEvent()
        {
            var global = await _context.Global.Include(g => g.CurrentEvent).FirstAsync();
            return global.CurrentEvent;
        }

        async public Task SetCurrentEvent(int eventId)
        {
            var global = await _context.Global.FirstAsync();

            global.CurrentEventId = eventId;

            await _context.SaveChangesAsync();
        }

        async public Task SetClosedCurrentEvent()
        {
            var currentEvent = await GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            currentEvent.IsClosed = true;
            await _context.SaveChangesAsync();
        }

        async public Task SetPublicCurrentEvent()
        {
            var currentEvent = await GetCurrentEvent() ?? throw new Exception($"Evento atual não selecionado!");

            currentEvent.IsPublic = true;
            await _context.SaveChangesAsync();
        }
    }
}
