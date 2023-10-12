using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {
        private readonly IBooksService _service;

        public BooksController(IBooksService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb) {
            var books = await _service.GetAll(filterDb);
            if (books.IsSucess) return Ok(books);
            return BadRequest(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var book = await _service.GetById(id);
            if (book.IsSucess) return Ok(book);
            return BadRequest(book);
        }

        [HttpGet("GetAllSelect")]
        public async Task<IActionResult> GetAllSelect() {
            var books = await _service.GetAllSelect();
            if (books.IsSucess) return Ok(books);
            return BadRequest(books);
        }

        [HttpGet("Dash")]
        public async Task<IActionResult> GetAllDash()
        {
            var books = await _service.GetAllDash();
            if (books.IsSucess) return Ok(books);
            return BadRequest(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDto model) {
            var result = await _service.Create(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateBookDto model) {
            var result = await _service.Update(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _service.Delete(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

    }
}
