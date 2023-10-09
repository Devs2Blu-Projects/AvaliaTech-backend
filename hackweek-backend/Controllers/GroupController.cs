using Microsoft.AspNetCore.Mvc;
using hackweek_backend.Services.Interfaces;
using hackweek_backend.dtos;
using hackweek_backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using hackweek_backend.Models;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            return Ok(await _service.GetGroups());
        }

        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<GroupDto?>> GetGroupById(int id)
        {
            var group = await _service.GetGroupById(id);
            if (group == null) return NotFound("Grupo não encontrado!");

            return Ok(group);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Group}")]
        public async Task<ActionResult> UpdateGroup(int id, GroupDtoUpdate request)
        {
            try
            {
                await _service.UpdateGroup(id, request);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("user/{idUser}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Group}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupByUser(int idUser)
        {
            var group = await _service.GetGroupByUser(idUser);
            if (group == null) return NotFound("Grupo não encontrado!");

            return Ok(group);
        }

        [HttpGet("proposition/{idProposition}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsByProposition(int idProposition) // TODO
        {
            return Ok(await _service.GetGroupsByProposition(idProposition));
        }

        [HttpGet("queue")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsOnQueue()
        {
            return Ok(await _service.GetGroupsOnQueue());
        }

        [HttpGet("rate/{idUser}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsToRate(int idUser)
        {
            return Ok(await _service.GetGroupsToRate(idUser));
        }

        [HttpGet("done")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsDone()
        {
            return Ok(await _service.GetGroupsDone());
        }
    }
}