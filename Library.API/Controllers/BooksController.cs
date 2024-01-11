using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Book;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var books = await _service.GetAll(filterDb);
            if (books.StatusCode == HttpStatusCode.OK) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("getsummary")]
        public async Task<IActionResult> GetSummary()
        {
            var books = await _service.GetSummary();
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookDto model)
        {
            var result = await _service.Create(model);
            if (result.StatusCode == HttpStatusCode.Created) return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateBookDto model)
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
