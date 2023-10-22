using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.User;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            if (users.IsSucess) return Ok(users);
            return BadRequest(users);
        }

        [HttpGet("GetAllSelect")]
        public async Task<IActionResult> GetAllSelect()
        {
            var users = await _service.GetAllSelect();
            if (users.IsSucess) return Ok(users);
            return BadRequest(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetById(id);
            if (user.IsSucess) return Ok(user);
            return BadRequest(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto model)
        {
            var result = await _service.Create(model);
            if (result.IsSucess) return Created($"/api/users/", result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto model)
        {
            var result = await _service.Update(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
