﻿using hackweek_backend.DTOs;
using hackweek_backend.Services;
using hackweek_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> DeleteRatingByAvaliadorById(int id)
        {
            try
            {
                await _service.DeleteRatingByAvaliadorById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("avaliador/{id}")]
        public async Task<IActionResult> GetRatingByIdByAvaliador(int id)
        {
            try
            {
                var result = await _service.GetRatingByIdByAvaliador(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatingsByAvaliador()
        {
            try
            {
                var result = await _service.GetAllRatingsByAvaliador();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("group/{id}")]
        public async Task<IActionResult> GetAllRatingsByGroup(int id)
        {
            try
            {
                var result = await _service.GetAllRatingsByGroup(id);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}