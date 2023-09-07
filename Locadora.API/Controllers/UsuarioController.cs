using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase {
        private readonly DataContext _context;

        public UsuarioController(DataContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(_context.Usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var usuario = _context.Usuarios.FirstOrDefault(user => user.Id == id);
            if (usuario == null) return BadRequest("Usuário não encontrado");
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario) {
            _context.Add(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario) {
            var user = _context.Usuarios.AsNoTracking().FirstOrDefault(user => user.Id == id);
            if (user == null) return BadRequest("Usuário não encontrado.");
            _context.Update(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Usuario usuario) {
            var user = _context.Usuarios.AsNoTracking().FirstOrDefault(user => user.Id == id);
            if (user == null) return BadRequest("Usuário não encontrado.");
            _context.Update(usuario);
            _context.SaveChanges();
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var usuario = _context.Usuarios.FirstOrDefault(user => user.Id == id);
            if (usuario == null) return BadRequest("Usuário não encontrado.");
            _context.Remove(usuario);
            _context.SaveChanges();
            return Ok();
        }
    }
}
