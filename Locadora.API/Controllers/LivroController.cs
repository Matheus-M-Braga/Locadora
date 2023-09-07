using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase {
        private readonly DataContext _context;

        public LivroController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(_context.Livros);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var livro = _context.Livros.FirstOrDefault(book => book.Id == id);
            if (livro == null) return BadRequest("Livro não encontrado");
            return Ok(livro);
        }

        [HttpPost]
        public IActionResult Post(Livro livro) {
            _context.Add(livro);
            _context.SaveChanges();
            return Ok(livro);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Livro livro) {
            var book = _context.Usuarios.AsNoTracking().FirstOrDefault(book => book.Id == id);
            if (book == null) return BadRequest("Livro não encontrado");
            _context.Update(livro);
            _context.SaveChanges();
            return Ok(livro);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Livro livro) {
            var book = _context.Usuarios.AsNoTracking().FirstOrDefault(book => book.Id == id);
            if (book == null) return BadRequest("Livro não encontrado");
            _context.Update(livro);
            _context.SaveChanges();
            return Ok(livro);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var livro = _context.Livros.FirstOrDefault(book => book.Id == id);
            if (livro == null) return BadRequest("Livro não encontrado.");
            _context.Remove(livro);
            _context.SaveChanges();
            return Ok();
        }

    }
}
