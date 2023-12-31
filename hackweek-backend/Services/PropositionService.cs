﻿using hackweek_backend.Data;
using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;


namespace hackweek_backend.Services
{
    public class PropositionService : IPropositionService
    {
        private readonly DataContext _context;
        private readonly IGlobalService _globalService;

        public PropositionService(DataContext context, IGlobalService globalService)
        {
            _context = context;
            _globalService = globalService;
        }

        public async Task<IEnumerable<PropositionModel>> GetPropositions()
        {
            try
            {
                var eventId = (await _globalService.GetGlobal()).CurrentEventId;

                var propositions = await _context.Propositions
                    .Where(p => p.EventId == eventId)
                    .ToListAsync();
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
                var proposition = await _context.Propositions
                    .FirstOrDefaultAsync(p => p.Id == id);
                return proposition;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao buscar o desafio com o identificador {id}.", e);
            }
        }

        public async Task CreateProposition(PropositionDtoInsert request)
        {
            try
            {
                var eventId = (await _globalService.GetGlobal()).CurrentEventId ?? 0;

                if (eventId == 0)
                {
                    throw new ArgumentNullException(nameof(request), "Desafio atual não selecionado!");
                }

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
                    EventId = eventId,
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

        public async Task UpdateProposition(int id, PropositionDtoUpdate request)
        {
            try
            {
                var proposition = await _context.Propositions.FirstOrDefaultAsync(p => p.Id == id);
                if (proposition == null)
                {
                    throw new Exception($"Desafio com o identificador {id} não encontrado.");
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    proposition.Name = request.Name;
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
