using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.LoginUser;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginUserController : ControllerBase
    {
        private readonly ILoginUserService _service;
        private readonly IAuthenticateService _authenticate;

        public LoginUserController(ILoginUserService service, IAuthenticateService authenticate)
        {
            _service = service;
            _authenticate = authenticate;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetAll();
            if (books.StatusCode == HttpStatusCode.OK) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetById(id);
            if (book.StatusCode == HttpStatusCode.OK) return Ok(book);
            return NotFound(book);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginUserCreateDto model)
        {
            var result = await _service.Add(model);
            if (result.StatusCode == HttpStatusCode.Created) return StatusCode(201, result);

            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserAuthDto model)
        {
            var result = await _authenticate.Authenticate(model.Email, model.Password);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] LoginUserUpdateDto model)
        {
            var result = await _service.Update(model);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }
    }
}
