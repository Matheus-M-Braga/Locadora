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
    [Produces("application/json")]
    public class BooksController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
    
        public BooksController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await _repo.GetAllBooks(true);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var book = await _repo.GetBookById(id);
            if (book == null) return BadRequest("Livro não encontrado");
            return Ok(book);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(BooksDto model) {
            var publisher = await _repo.GetPublisherById((int)model.PublisherId);
            if (publisher == null) return BadRequest("Editora informada não existe no registro.");

            var book = _mapper.Map<Books>(model);
            _repo.Add(book);
            if (_repo.SaveChanges()) {
                return Created($"/api/Books/{book.Id}", _mapper.Map<Books>(book));
            }

            return BadRequest("Erro ao cadastrar livro.");
        }
        
        [HttpPut("{id}")]  
        public async Task<IActionResult> Put(int id, BooksDto model) {
            var publisher = await _repo.GetPublisherById((int)model.PublisherId);
            if (publisher == null) return BadRequest("Editora informada não existe no registro.");

            var book = _repo.GetBookById(id);
            if (book == null) return BadRequest("Livro não encontrado.");
            await _mapper.Map(model, book);
            _repo.Update(book);
            if (_repo.SaveChanges()) {
                return Created($"/api/Books/{book.Id}", _mapper.Map<BooksDto>(book));
            }

            return BadRequest("Erro ao atualizar livro.");
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var book = await _repo.GetBookById(id);
            if (book == null) return BadRequest("Livro não encontrado.");
            _repo.Delete(book);

            if (_repo.SaveChanges()) {
                return Ok("Livro deletado com sucesso.");
            }

            return BadRequest("Erro ao deletar livro.");
        }

    }
}
