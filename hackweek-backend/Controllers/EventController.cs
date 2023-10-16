using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hackweek_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService Service)
        {
            _service = Service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventModel>>> GetEvents()
        {
            return Ok(await _service.GetEvents());
        }

        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EventModel?>> GetEventById(int Id)
        {
            return Ok(await _service.GetEventById(Id));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> CreateEvent(EventDtoInsert request)
        {
            try
            {
                await _service.CreateEvent(request);
                return Ok("Evento criado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> DeleteEvent(int Id)
        {
            try
            {
                await _service.DeleteEvent(Id);
                return Ok("Evento excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> UpdateEvent(int Id, EventDtoUpdate request)
        {
            try
            {
                await _service.UpdateEvent(Id, request);
                return Ok("Evento atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
