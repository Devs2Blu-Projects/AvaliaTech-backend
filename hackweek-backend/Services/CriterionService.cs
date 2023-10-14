using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class CriterionService : ICriterionService
    {
        private readonly DataContext _context;

        public CriterionService(DataContext context) { _context = context; }
        async public Task CreateCriterion(CriterionDTO request)
        {
            var model = new CriterionModel
            {
                Name = request.Name,
                Description = request.Description,
                Weight = request.Weight,
            };
            await _context.Criteria.AddAsync(model);
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

        async public Task<IEnumerable<CriterionModel?>?> GetCriteriaByEvent(int idEvent)
        {
            var criteria = await _context.EventCriteria.Where(c => c.EventId == idEvent).Select(c => c.Criterion).ToListAsync();
            return (criteria == null || criteria.Count == 0) ? null : criteria;
        }

        async public Task<CriterionModel?> GetCriterionById(int id)
        {
            return await _context.Criteria.FirstOrDefaultAsync(c => c.Id == id);
        }

        async public Task UpdateCriterion(int id, CriterionDTO request)
        {
            if (request.Id != id) throw new Exception("Id diferente do critério informado!");

            var model = await _context.Criteria.FindAsync(id) ?? throw new Exception($"Critério não encontrado! ({request.Id})");

            model.Name = request.Name;
            model.Description = request.Description;
            model.Weight = request.Weight;

            await _context.SaveChangesAsync();

        }
    }
}
