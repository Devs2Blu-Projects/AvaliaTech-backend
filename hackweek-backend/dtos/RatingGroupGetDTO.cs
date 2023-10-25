namespace hackweek_backend.dtos
{
    public class RatingGroupGetDTO
    {
        public int Id { get; set; }
        public double Grade { get; set; }
        public GroupDto? Group { get; set; }
        public UserDto? User { get; set; }
    }
}
