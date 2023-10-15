namespace hackweek_backend.dtos
{
    public class GroupsByDateDTO
    {
        public DateTime Date { get; set; }
        public List<GroupDto?> Groups { get; set; } = new List<GroupDto?>();
    }
}
