using hackweek_backend.dtos;
using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IPropositionService
    {
        Task<IEnumerable<PropositionModel>> GetPropositions();
        Task<PropositionModel?> GetPropositionById(int id);
        Task CreateProposition(PropositionDtoInsert request);
        Task DeleteProposition(int id);
        Task UpdateProposition(int id, PropositionDtoUpdate request);
    }
}