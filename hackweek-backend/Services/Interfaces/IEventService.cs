using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventModel>> GetEvents();
        Task<EventModel?> GetEventById(int id);
        Task CreateEvent(EventModel request);
        Task DeleteEvent(int id);
        Task UpdateEvent(int id, EventModel request);
    }
}