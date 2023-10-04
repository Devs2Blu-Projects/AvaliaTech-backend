using hackweek_backend.Models;

namespace hackweek_backend.Services.Interfaces
{
    public interface IPropositionService
    {
        Task<IEnumerable<PropositionModel>> GetPropositions();
        Task<PropositionModel?> GetPropositionById(int id);
        Task CreateProposition(PropositionModel Proposition);
        Task DeleteProposition(int id);
        Task UpdateProposition(int id, PropositionModel Proposition);
    }
}