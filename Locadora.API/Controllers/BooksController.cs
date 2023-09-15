using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Locadora.API.Controllers {
    /// <summary>
    /// Isso
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BooksController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
    
        public BooksController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllBooks();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var book = _repo.GetBookById(id);
            if (book == null) return BadRequest("Livro não encontrado");
            return Ok(book);
        }
        
        [HttpPost]
        public IActionResult Post(BooksDto model) {
            var publisher = _repo.GetPublisherById((int)model.PublisherId);
            if (publisher == null) return BadRequest("Editora informada não existe no registro.");

            var book = _mapper.Map<Books>(model);
            _repo.Add(book);
            if (_repo.SaveChanges()) {
                return Created($"/api/Books/{book.Id}", _mapper.Map<BooksDto>(book));
            }

            return BadRequest("Erro ao cadastrar livro.");
        }
        
        [HttpPut("{id}")]  
        public IActionResult Put(int id, BooksDto model) {
            var publisher = _repo.GetPublisherById((int)model.PublisherId);
            if (publisher == null) return BadRequest("Editora informada não existe no registro.");

            var book = _repo.GetBookById(id);
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
