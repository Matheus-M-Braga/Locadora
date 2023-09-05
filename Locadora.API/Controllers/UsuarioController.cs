using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locadora.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {
        // GET: api/<UsuarioController>
        public List<Usuario> Usuarios = new List<Usuario>() {
            new Usuario() {
                Id = 1,
                Nome = "Francisca Menezes",
                Cidade = "Fortaleza",
                Endereco = "Rua A, 27",
                Email = "francisquinha@gmail.com",
            },
            new Usuario() {
                Id = 2,
                Nome = "Matheus",
                Cidade = "Fortaleza",
                Endereco = "Rua B, 27",
                Email = "matheus@gmail.com",
            },
            new Usuario() {
                Id = 3,
                Nome = "Emilly",
                Cidade = "Fortaleza",
                Endereco = "Rua C, 27",
                Email = "emilly@gmail.com",
            },
        };

        public UsuarioController() { }

        [HttpGet("todos")]
        public IActionResult Get() {
            return Ok(Usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var usuario = Usuarios.FirstOrDefault(user => user.Id == id);
            if (usuario == null) return BadRequest("Usuário não encontrado");
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario) {
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario) {
            return Ok(usuario);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Usuario usuario) {
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            return Ok();
        }
    }
}
