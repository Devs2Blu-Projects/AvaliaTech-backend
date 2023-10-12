namespace hackweek_backend.Models
{
    public class GlobalModel
    {
        public int Id { get; set; }
        public int? CurrentEventId { get; set; }
        public EventModel? CurrentEvent { get; set; }
    }
}
