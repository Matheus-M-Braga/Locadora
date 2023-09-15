using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var result = await _repo.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            var user = await _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuário não encontrado");
           
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsersDto model) {
            var user = _mapper.Map<Users>(model);
            _repo.Add(user);
            if (_repo.SaveChanges()) {
                return Created($"/api/Users/{user.Id}", _mapper.Map<Users>(user));
            }
            return BadRequest("Erro ao cadastrar usuário.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UsersDto model) {
            var user = await _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuário não encontrado.");
            _mapper.Map(model, user);
            _repo.Update(user);
            if (_repo.SaveChanges()) {
                return Created($"/api/Users/{user.Id}", _mapper.Map<Users>(user));
            }
            return BadRequest("Erro ao atualizar usuário.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var user = await _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuário não encontrado.");
            _repo.Delete(user);

            if (_repo.SaveChanges()) {
                return Ok("Usuário deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar usuário.");
        }
    }
}
