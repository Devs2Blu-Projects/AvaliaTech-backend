using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class EventDtoInsert
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
