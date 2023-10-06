using Microsoft.AspNetCore.Mvc;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using hackweek_backend.DTOs;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await _service.GetUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> GetUserById(int id)
        {
            var user = await _service.GetUserById(id);
            if (user == null) return NotFound("Usuário não encontrado!");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDtoInsert request)
        {
            try
            {
                await _service.CreateUser(request);
                return Ok("Usuário adicionado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _service.DeleteUser(id);
                return Ok("Usuário excluído com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserDtoUpdate request)
        {
            try
            {
                await _service.UpdateUser(id, request);
                return Ok("Usuário atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/role/{role}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(string role)
        {
            return Ok(await _service.GetUsersByRole(role));
        }
    }
}