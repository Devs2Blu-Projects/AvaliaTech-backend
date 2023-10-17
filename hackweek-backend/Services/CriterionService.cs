using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;

namespace hackweek_backend.Services
{
    public class CriterionService : ICriterionService
    {
        private readonly DataContext _context;
        private readonly IGlobalService _global;

        public CriterionService(DataContext context, IGlobalService global) { _context = context; _global = global; }
        async public Task CreateCriterion(CriterionDtoInsert request)
        {
            var currentEvent = await _global.GetCurrentEvent() ?? throw new Exception("Evento atual não selecionado!");

            var model = new CriterionModel
            {
                Name = request.Name,
                Description = request.Description,
                Weight = request.Weight,
                EventId = currentEvent.Id,
                Event = currentEvent
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

        async public Task<IEnumerable<CriterionModel?>?> GetCriteriaByCurrentEvent()
        {
            var currentEvent = await _global.GetCurrentEvent() ?? throw new ArgumentNullException("Evento não existe!");
            var criteria = await _context.Criteria.Where(c => c.EventId == currentEvent.Id).ToListAsync();
            return (criteria == null || criteria.Count == 0) ? null : criteria;
        }
        async public Task<IEnumerable<CriterionModel?>?> GetCriteriaByEventId(int Id)
        {
            var criteria = await _context.Criteria.Where(c => c.EventId == Id).ToListAsync();
            return (criteria == null || criteria.Count == 0) ? null : criteria;
        }

        async public Task<CriterionModel?> GetCriterionById(int id)
        {
            return await _context.Criteria.FirstOrDefaultAsync(c => c.Id == id);
        }

        async public Task UpdateCriterion(int id, CriterionDtoUpdate request)
        {
            if (request.Id != id) throw new Exception("Id diferente do critério informado!");
            var currentEvent = await _global.GetCurrentEvent() ?? throw new Exception("Evento atual não selecionado!");

            var model = await _context.Criteria.FindAsync(id) ?? throw new Exception($"Critério não encontrado! ({request.Id})");

            model.Name = request.Name;
            model.Description = request.Description;
            model.Weight = request.Weight;
            model.EventId = currentEvent.Id;
            model.Event = currentEvent;
            await _context.SaveChangesAsync();

        }
    }
}
