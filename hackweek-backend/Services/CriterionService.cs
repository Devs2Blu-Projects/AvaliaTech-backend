using hackweek_backend.Data;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class CriterionService : ICriterionService
    {
        private readonly DataContext _context;

        public CriterionService(DataContext context) { _context = context; }
        async public Task CreateCriterion(CriterionModel criterio)
        {
            _context.Criteria.Add(criterio);
            await _context.SaveChangesAsync();
        }

        async public Task DeleteCriterion(int id)
        {
            var criteria = await _context.Criteria.FirstOrDefaultAsync(c => c.Id == id);

            if (criteria == null) throw new ArgumentException("ID não encontrado");

            _context.Criteria.Remove(criteria);
            await _context.SaveChangesAsync();

        }

        async public Task<IEnumerable<CriterionModel>> GetCriteria()
        {
            return await _context.Criteria.ToListAsync();
        }

        async public Task<IEnumerable<CriterionModel>> GetCriteriaByEvent(int idEvent)
        {
            var criteria = await _context.EventCriteria.Where(c => c.EventId == idEvent).Select(c => c.Criterion).ToListAsync();
            return (criteria == null || criteria.Count == 0) ? null : criteria;
        }

        async public Task<CriterionModel?> GetCriterionById(int id)
        {
            return await _context.Criteria.FirstOrDefaultAsync(c => c.Id == id);
        }

        async public Task UpdateCriterion(int id, CriterionModel request)
        {
            var criteria = await _context.Criteria.FirstOrDefaultAsync(c => c.Id == id);
            if (criteria == null || id != request.Id) throw new ArgumentException("IDs diferentes!");
            criteria.Description = request.Description;
            criteria.Name = request.Name;
            await _context.SaveChangesAsync();

        }
    }
}
