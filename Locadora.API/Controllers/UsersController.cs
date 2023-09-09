using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly IRepository _repo;

        public UsersController(IRepository repo) {
            _repo = repo;

        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var user = _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuário não encontrado");
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post(Users user) {
            _repo.Add(user);
            if (_repo.SaveChanges()) {
                return Ok(user);
            }
            return BadRequest("Erro ao cadastrar usuário.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Users user) {
            var u = _repo.GetUserById(id);
            if (u == null) return BadRequest("Usuário não encontrado.");
            _repo.Update(user);
            if (_repo.SaveChanges()) {
                return Ok(user);
            }
            return BadRequest("Erro ao atualizar usuário.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Users user) {
            var u = _repo.GetUserById(id);
            if (u == null) return BadRequest("Usuário não encontrado.");
            _repo.Update(user);
            if (_repo.SaveChanges()) {
                return Ok(user);
            }
            return BadRequest("Erro ao atualizar usuário.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var u = _repo.GetUserById(id, true);
            if (u == null) return BadRequest("Usuário não encontrado.");
            _repo.Delete(u);

            if (_repo.SaveChanges()) {
                return Ok("Usuário deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar usuário.");
        }
    }
}
