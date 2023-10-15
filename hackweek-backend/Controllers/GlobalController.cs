using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlobalController : ControllerBase
    {
        private readonly IGlobalService _service;

        public GlobalController(IGlobalService service)
        {
            _service = service;
        }

        [HttpGet("currentEvent")]
        [AllowAnonymous]
        public async Task<ActionResult<EventModel?>> GetCurrentEvent()
        {
            return Ok(await _service.GetCurrentEvent());
        }

        [HttpPut("currentEvent/{eventId}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<string>> SetCurrentEvent(int eventId)
        {
            try
            {
                await _service.SetCurrentEvent(eventId);
                return Ok("Evento ativo atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("currentEvent/close")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> SetClosedCurrentEvent()
        {
            try
            {
                await _service.SetClosedCurrentEvent();
                return Ok("Evento ativo encerrado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("currentEvent/public")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> SetPublicCurrentEvent()
        {
            try
            {
                await _service.SetPublicCurrentEvent();
                return Ok("Evento ativo disponibilizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
