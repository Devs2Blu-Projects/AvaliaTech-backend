using hackweek_backend.dtos;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hackweek_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _service;
        private readonly ILoginService _login;

        public GroupController(IGroupService service, ILoginService login)
        {
            _service = service;
            _login = login;
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
            var group = await _service.GetGroupById(id);
            if (group == null) return NotFound("Grupo não encontrado!");
            if (!_login.HasAccessToUser(HttpContext, group.UserId)) return Unauthorized();

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
            if (!_login.HasAccessToUser(HttpContext, idUser)) return Unauthorized();

            var group = await _service.GetGroupByUser(idUser);
            if (group == null) return NotFound("Grupo não encontrado!");

            return Ok(group);
        }

        [HttpGet("ranking")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsRanking()
        {
            return Ok(await _service.GetGroupsRanking(_login.GetUserRole(HttpContext)));
        }

        [HttpGet("rate/{idUser}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<ActionResult<IEnumerable<GroupDtoWithoutGrade>>> GetGroupsToRate(int idUser)
        {
            if (!_login.HasAccessToUser(HttpContext, idUser)) return Unauthorized();

            return Ok(await _service.GetGroupsToRate(idUser));
        }

        [HttpGet("groupsByDate")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<GroupsByDateDTO>>> GetAllEventGroupsByDate()
        {
            return Ok(await _service.GetAllEventGroupsByDate());
        }

        [HttpPut("updateOrder")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PutGroupOrder([FromBody] List<GroupsByDateDTO> groupOrder)
        {
            try
            {
                await _service.UpdateGroupOrder(groupOrder);
                return Ok("Ordem atualizada com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}