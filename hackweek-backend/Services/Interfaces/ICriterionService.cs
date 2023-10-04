using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface ICriterionService
    {
        Task<IEnumerable<CriterionModel>> GetCriteria();
        Task<CriterionModel?> GetCriterionById(int id);
        Task CreateCriterion(CriterionModel user);
        Task DeleteCriterion(int id);
        Task UpdateCriterion(int id, CriterionModel user);

        Task<IEnumerable<CriterionModel>> GetCriteriaByProposition(int idProposition);
    }
}