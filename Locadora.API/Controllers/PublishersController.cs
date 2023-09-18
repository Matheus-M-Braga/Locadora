using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly PublishersServices _managePublishers;

        public PublishersController(IRepository repo, IMapper mapper, PublishersServices managePublishers)
        {
            _repo = repo;
            _mapper = mapper;
            _managePublishers = managePublishers;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repo.GetAllPublishers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publisher = await _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada");
            return Ok(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PublishersDto model)
        {
            var existingPublisher = _managePublishers.VerifyName(model.Name);
            if (existingPublisher == false)
            {
                return BadRequest("Uma editora com este nome já existe.");
            }
            
            var publisher = _mapper.Map<Publishers>(model);
            _repo.Add(publisher);
            if (_repo.SaveChanges())
            {
                return Created($"/api/Publishers/{publisher.Id}", _mapper.Map<Publishers>(publisher));
            }
            return BadRequest("Erro ao cadastrar editora.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PublishersDto model)
        {

            var publisher = await _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada.");

            var existingPublisher = _managePublishers.VerifyName(model.Name);
            if (existingPublisher != null)
            {
                return BadRequest("Uma editora com este nome já existe.");
            }

            _mapper.Map(model, publisher);
            _repo.Update(publisher);
            if (_repo.SaveChanges())
            {
                return Created($"/api/Publishers/{publisher.Id}", _mapper.Map<Publishers>(publisher));
            }
            return BadRequest("Erro ao atualizar editora.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _repo.GetPublisherById(id);
            if (publisher == null) return BadRequest("Editora não encontrada.");
            _repo.Delete(publisher);

            if (_repo.SaveChanges())
            {
                return Ok("Editora deletada com sucesso.");
            }
            return BadRequest("Erro ao deletar editora.");
        }
    }
}
