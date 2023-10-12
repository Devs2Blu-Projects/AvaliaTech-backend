using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IGlobalService
    {
        Task<EventModel?> GetCurrentEvent();
        Task SetCurrentEvent(int eventId);
    }
}