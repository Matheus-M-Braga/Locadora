using Locadora.API.Dtos;
using Locadora.API.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var books = await _service.GetAsync();
            if (books.IsSucess) return Ok(books);
            return BadRequest(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetByIdAsync(id);
            if (book.IsSucess) return Ok(book);
            return BadRequest(book);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDto model)
        {
            var result = await _service.CreateAsync(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateBookDto model)
        {
            var result = await _service.UpdateAsync(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

    }
}
