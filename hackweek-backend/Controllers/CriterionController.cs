using Azure.Core;
using hackweek_backend.Models;
using hackweek_backend.Services;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriterionController : ControllerBase
    {
        private readonly ICriterionService _service;
        public CriterionController(ICriterionService Service)
        {
            _service = Service;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateCriterion(CriterionModel request)
        {
            try
            {
                await _service.CreateCriterion(request);
                return CreatedAtAction(nameof(GetCriterionById), new { id = request.Id }, request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetCriterionById(int Id)
        {
            try
            {
                var result =  await _service.GetCriterionById(Id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteCriterion(int Id)
        {
            try
            {
                await _service.DeleteCriterion(Id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetCriteria()
        {
            try
            {
                var result = await _service.GetCriteria();
                return Ok(result);
            }
            catch(Exception e) 
            { 
                return BadRequest(e.Message);
            }
        }

        [HttpGet("proposition/{IdProposition}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetCriteriaByProposition(int IdProposition)
        {
            try
            {
                var result = await _service.GetCriteriaByProposition(IdProposition);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Id")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateCriterion(int Id, CriterionModel request)
        {
            try
            {
                await _service.UpdateCriterion(Id, request);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
