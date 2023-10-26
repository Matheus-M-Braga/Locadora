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
        [SwaggerOperation(Summary = "GetAll")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var books = await _service.GetAll(filterDb);
            if (books.StatusCode == HttpStatusCode.OK) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("getallselect")]
        [SwaggerOperation(Summary = "GetAllSelect")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllSelect()
        {
            var books = await _service.GetAllSelect();
            if (books.StatusCode == HttpStatusCode.OK) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("count")]
        [SwaggerOperation(Summary = "GetCount")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllCount()
        {
            var books = await _service.GetAllCount();
            if (books.StatusCode == HttpStatusCode.OK) return Ok(books);
            return NotFound(books);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetById")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _service.GetById(id);
            if (book.StatusCode == HttpStatusCode.OK) return Ok(book);
            return NotFound(book);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Post([FromBody] CreateBookDto model)
        {
            var result = await _service.Create(model);
            if (result.StatusCode == HttpStatusCode.Created) return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Put([FromBody] UpdateBookDto model)
        {
            var result = await _service.Update(model);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }
    }
}
