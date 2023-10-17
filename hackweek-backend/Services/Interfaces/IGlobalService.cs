using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IGlobalService
    {
        Task<GlobalModel> GetGlobal();
        Task<EventModel?> GetCurrentEvent();
        Task SetCurrentEvent(int eventId);

        Task SetClosedCurrentEvent();
        Task SetPublicCurrentEvent();
    }
}