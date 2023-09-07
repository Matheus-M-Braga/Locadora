using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class EditoraController : ControllerBase {
        private readonly DataContext _context;

        public EditoraController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(_context.Editoras);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var editora = _context.Editoras.FirstOrDefault(user => user.Id == id);
            if (editora == null) return BadRequest("Usuário não encontrado");
            return Ok(editora);
        }

        [HttpPost]
        public IActionResult Post(Editora editora) {
            _context.Add(editora);
            _context.SaveChanges();
            return Ok(editora);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Editora editora) {
            var publisher = _context.Usuarios.AsNoTracking().FirstOrDefault(publisher => publisher.Id == id);
            if (publisher == null) return BadRequest("Editora não encontrada.");
            _context.Update(editora);
            _context.SaveChanges();
            return Ok(editora);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Editora editora) {
            var publisher = _context.Editoras.AsNoTracking().FirstOrDefault(publisher => publisher.Id == id);
            if (publisher == null) return BadRequest("Editora não encontrada.");
            _context.Update(editora);
            _context.SaveChanges();
            return Ok(editora);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var editora = _context.Editoras.FirstOrDefault(publisher => publisher.Id == id);
            if (editora == null) return BadRequest("Usuário não encontrado.");
            _context.Remove(editora);
            _context.SaveChanges();
            return Ok();
        }
    }
}
