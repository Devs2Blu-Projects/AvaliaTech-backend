using hackweek_backend.Models;

namespace hackweek_backend.dtos
{
    public class CriterionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
    }
}
