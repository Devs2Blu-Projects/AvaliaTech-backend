namespace hackweek_backend.dtos
{
    public class RatingGetDTO
    {
        public double Grade { get; set; }
        public GroupDto? Group { get; set; }
        public UserDto? User { get; set; }
    }
}
