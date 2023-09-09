using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase {
        private readonly IRepository _repo;

        public PublishersController(IRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllPublishers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var publisher = _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada");
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult Post(Publishers publisher) {
            _repo.Add(publisher);
            if (_repo.SaveChanges()) {
                return Ok(publisher);
            }
            return BadRequest("Erro ao cadastrar editora.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Publishers publisher) {
            var p = _repo.GetPublisherById(id);
            if (p == null) return BadRequest("Editora não encontrada.");
            _repo.Update(publisher);
            if (_repo.SaveChanges()) {
                return Ok(publisher);
            }
            return BadRequest("Erro ao atualizar editora.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Publishers publisher) {
            var p = _repo.GetPublisherById(id);
            if (p == null) return BadRequest("Editora não encontrada.");
            _repo.Update(publisher);
            if (_repo.SaveChanges()) {
                return Ok(publisher);
            }
            return BadRequest("Erro ao atualizar editora.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var p = _repo.GetPublisherById(id, true);
            if (p == null) return BadRequest("Editora não encontrada.");
            _repo.Delete(p);

            if (_repo.SaveChanges()) {
                return Ok("Editora deletada com sucesso.");
            }
            return BadRequest("Erro ao deletar editora.");
        }
    }
}
