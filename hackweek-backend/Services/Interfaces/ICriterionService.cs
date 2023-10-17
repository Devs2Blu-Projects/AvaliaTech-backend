using hackweek_backend.dtos;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface ICriterionService
    {
        Task<IEnumerable<CriterionModel>> GetCriteria();
        Task<CriterionModel?> GetCriterionById(int id);
        Task CreateCriterion(CriterionDtoInsert request);
        Task DeleteCriterion(int id);
        Task UpdateCriterion(int id, CriterionDtoUpdate request);
        Task<IEnumerable<CriterionModel?>?> GetCriteriaByCurrentEvent();
        Task<IEnumerable<CriterionModel?>?> GetCriteriaByEventId(int Id);
    }
}