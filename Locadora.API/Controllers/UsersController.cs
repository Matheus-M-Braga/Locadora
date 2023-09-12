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
        public IActionResult Get() {
            var result = _repo.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UsersDto>>(result));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var user = _repo.GetUserById(id);
            if (user == null) return BadRequest("Usuário não encontrado");
            var userDto = _mapper.Map<UsersDto>(user);  
            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult Post(UsersDto model) {
            var user = _mapper.Map<Users>(model);
            _repo.Add(user);
            if (_repo.SaveChanges()) {
                return Created($"/api/Users/{model.Id}", _mapper.Map<UsersDto>(user));
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
