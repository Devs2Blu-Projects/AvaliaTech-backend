using hackweek_backend.Data;
using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hackweek_backend.Services
{
    public class PropositionService : IPropositionService
    {
        private readonly DataContext _context;
        public PropositionService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropositionModel>> GetPropositions()
        {
            try
            {
                var propositions = await _context.Propositions.ToListAsync();
                return propositions;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar os desafios.", e);
            }
        }

        public async Task<PropositionModel?> GetPropositionById(int id)
        {
            try
            {
                var proposition = await _context.Propositions.FindAsync(id);
                return proposition;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o desafio com o identificador {id}.", e);
            }
        }

        public async Task CreateProposition(PropositionDTO request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Os dados do desafio não podem ser nulos.");
                }

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    throw new ArgumentException("O nome do desafio não pode estar vazio.", nameof(request.Name));
                }

                var proposition = new PropositionModel
                {
                    Name = request.Name,
                    PropositionCriteria = request.PropositionCriteria?.Select(c => new PropositionCriterionModel
                    {
                        Weight = c.Weight,
                        CriterionId = c.CriterionId
                    }).ToList()
                };

                _context.Propositions.Add(proposition);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar o desafio.", e);
            }
        }


        public async Task DeleteProposition(int id)
        {
            try
            {
                var proposition = await _context.Propositions.FindAsync(id);
                if (proposition == null)
                {
                    throw new Exception($"Desafio com o identificador {id} não encontrado.");
                }

                _context.Propositions.Remove(proposition);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao excluir o desafio com o identificador {id}.", e);
            }
        }

        public async Task UpdateProposition(int id, PropositionDTO request)
        {
            try
            {
                var proposition = await _context.Propositions.Include(p => p.PropositionCriteria).FirstOrDefaultAsync(p => p.Id == id);
                if (proposition == null)
                {
                    throw new Exception($"Desafio com o identificador {id} não encontrado.");
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    proposition.Name = request.Name;
                }

                if (request.PropositionCriteria != null)
                {
                    
                    if (!request.PropositionCriteria.Any())
                    {
                        proposition.PropositionCriteria?.Clear();
                    }
                    else
                    {
                        foreach (var criterionDTO in request.PropositionCriteria)
                        {
                            var existingCriterion = proposition.PropositionCriteria?.FirstOrDefault(c => c.CriterionId == criterionDTO.CriterionId);
                            if (existingCriterion != null)
                            {
                                existingCriterion.Weight = criterionDTO.Weight;
                            }
                            else
                            {
                                var newCriterion = new PropositionCriterionModel
                                {
                                    Weight = criterionDTO.Weight,
                                    CriterionId = criterionDTO.CriterionId
                                };
                                proposition.PropositionCriteria?.Add(newCriterion);
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao atualizar o desafio com o identificador {id}.", e);
            }
        }

    }
}
