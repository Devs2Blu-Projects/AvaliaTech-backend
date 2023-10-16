namespace hackweek_backend.dtos
{
    public class CriterionDtoInsert
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Weight { get; set; }
    }
}
