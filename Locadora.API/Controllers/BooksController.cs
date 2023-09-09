using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {
        private readonly IRepository _repo;

        public BooksController(IRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllBooks(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var book = _repo.GetBookById(id, true);
            if (book == null) return BadRequest("Livro não encontrado");
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(Books book) {
            _repo.Add(book);
            if (_repo.SaveChanges()) {
                return Ok(book);
            }
            return BadRequest("Erro ao cadastrar livro.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Books book) {
            var b = _repo.GetBookById(id, false);
            if (b == null) return BadRequest("Livro não encontrado.");
            _repo.Update(book);
            if (_repo.SaveChanges()) {
                return Ok(book);
            }
            return BadRequest("Erro ao atualizar livro.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Books book) {
            var b = _repo.GetBookById(id, false);
            if (b == null) return BadRequest("Livro não encontrado.");
            _repo.Update(book);
            if (_repo.SaveChanges()) {
                return Ok(book);
            }
            return BadRequest("Erro ao atualizar livro.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var u = _repo.GetBookById(id);
            if (u == null) return BadRequest("Livro não encontrado.");
            _repo.Delete(u);

            if (_repo.SaveChanges()) {
                return Ok("Livro deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar livro.");
        }

    }
}
