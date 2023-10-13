using hackweek_backend.DTOs;
using hackweek_backend.Models;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hackweek_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _service;
        public RatingController(IRatingService Service)
        {
            _service = Service;
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> CreateRating(RatingDTO rating)
        {
            try
            {
                await _service.CreateRating(rating);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("avaliador/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteRatingByAvaliadorById(int id)
        {
            try
            {
                await _service.DeleteRating(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("avaliador/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetRatingById(int id) //TODO: Check method name
        {
            try
            {
                var result = await _service.GetRatingById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> GetAllRatingsByAvaliador()
        {
            try
            {
                var result = await _service.GetAllRatings(); //TODO: check parameter
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("group/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAllRatingsByGroup(int id)
        {
            try
            {
                var result = await _service.GetRatingsByGroup(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
