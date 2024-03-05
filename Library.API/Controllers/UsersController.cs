using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.User;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var users = await _service.GetAll(filterDb);
            if (users.StatusCode == HttpStatusCode.OK)
                return Ok(users);
            return NotFound(users);
        }

        [HttpGet("getsummary")]
        public async Task<IActionResult> GetSummary()
        {
            var users = await _service.GetSummary();
            if (users.StatusCode == HttpStatusCode.OK)
                return Ok(users);
            return NotFound(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetById(id);
            if (user.StatusCode == HttpStatusCode.OK)
                return Ok(user);
            return NotFound(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto model)
        {
            var result = await _service.Create(model);
            if (result.StatusCode == HttpStatusCode.Created)
                return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto model)
        {
            var result = await _service.Update(model);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);
            return BadRequest(result);
        }
    }
}
