using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropositionController : ControllerBase
    {
        private readonly IPropositionService _service;

        public PropositionController(IPropositionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropositions()
        {
            try
            {
                var propositions = await _service.GetPropositions();
                return Ok(propositions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropositionById(int id)
        {
            try
            {
                var proposition = await _service.GetPropositionById(id);
                if (proposition == null)
                {
                    return NotFound();
                }
                return Ok(proposition);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProposition(PropositionDTO request)
        {
            try
            {
                await _service.CreateProposition(request);
                return Ok("Desafio criado com sucesso.");
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposition(int id)
        {
            try
            {
                await _service.DeleteProposition(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposition(int id, PropositionDTO request)
        {
            try
            {
                await _service.UpdateProposition(id, request);
                return Ok("Desafio atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
