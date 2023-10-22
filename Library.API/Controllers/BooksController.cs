using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Book;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "GetAll")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var books = await _service.GetAll(filterDb);
            if (books.IsSucess) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("getallselect")]
        [SwaggerOperation(Summary = "GetAllSelect")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllSelect()
        {
            var books = await _service.GetAllSelect();
            if (books.IsSucess) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("count")]
        [SwaggerOperation(Summary = "GetCount")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllCount()
        {
            var books = await _service.GetAllCount();
            if (books.IsSucess) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("mostrented")]
        [SwaggerOperation(Summary = "mostrented")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetMostRented()
        {
            var mostrented = await _service.GetMostRented();
            if (mostrented.IsSucess) return Ok(mostrented);
            return NotFound(mostrented);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetById")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetById(id);
            if (book.IsSucess) return Ok(book);
            return NotFound(book);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Post([FromBody] CreateBookDto model)
        {
            var result = await _service.Create(model);
            if (result.IsSucess) return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Put([FromBody] UpdateBookDto model)
        {
            var result = await _service.Update(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
