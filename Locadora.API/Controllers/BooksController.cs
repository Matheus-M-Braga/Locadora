using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public BooksController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllBooks(true);
            return Ok(_mapper.Map<IEnumerable<BooksDto>>(result));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var book = _repo.GetBookById(id, true);
            if (book == null) return BadRequest("Livro não encontrado");
            var bookDto = _mapper.Map<BooksDto>(book);
            return Ok(bookDto);
        }

        [HttpPost]
        public IActionResult Post(BookRegisterDto model) {
            var book = _mapper.Map<Books>(model);
            _repo.Add(book);
            if (_repo.SaveChanges()) {
                return Created($"/api/Books/{book.Id}", _mapper.Map<BooksDto>(book));
            }
            return BadRequest("Erro ao cadastrar livro.");
        }

        [HttpPut("{id}")]  
        public IActionResult Put(int id, BookRegisterDto model) {
            var book = _repo.GetBookById(id, false);
            if (book == null) return BadRequest("Livro não encontrado.");
            _mapper.Map(model, book);
            _repo.Update(book);
            if (_repo.SaveChanges()) {
                return Created($"/api/Books/{book.Id}", _mapper.Map<BooksDto>(book));
            }
            return BadRequest("Erro ao atualizar livro.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var book = _repo.GetBookById(id);
            if (book == null) return BadRequest("Livro não encontrado.");
            _repo.Delete(book);

            if (_repo.SaveChanges()) {
                return Ok("Livro deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar livro.");
        }

    }
}
