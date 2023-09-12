using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public PublishersController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
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
        public IActionResult Post(PublishersDto model) {
            var publisher = _mapper.Map<Publishers>(model);
            _repo.Add(publisher);
            if (_repo.SaveChanges()) {
                return Created($"/api/Publishers/{publisher.Id}", _mapper.Map<Publishers>(publisher));
            }
            return BadRequest("Erro ao cadastrar editora.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PublishersDto model) {
            var publisher = _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada.");
            _mapper.Map(model, publisher);
            _repo.Update(publisher);
            if (_repo.SaveChanges()) {
                return Created($"/api/Publishers/{publisher.Id}", _mapper.Map<Publishers>(publisher));
            }
            return BadRequest("Erro ao atualizar editora.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var publisher = _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada.");
            _repo.Delete(publisher);

            if (_repo.SaveChanges()) {
                return Ok("Editora deletada com sucesso.");
            }
            return BadRequest("Erro ao deletar editora.");
        }
    }
}
