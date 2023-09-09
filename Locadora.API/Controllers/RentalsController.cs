using Locadora.API.Data;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase {
        private readonly IRepository _repo;

        public RentalsController( IRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllRentals(false, false);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var rental = _repo.GetRentalById(id, true, true);
            if (rental == null) return BadRequest("Aluguel não encontrado");
            return Ok(rental);
        }

        [HttpPost]
        public IActionResult Post(Rentals rental) {
            _repo.Add(rental);
            if (_repo.SaveChanges()) {
                return Ok(rental);
            }
            return BadRequest("Erro ao cadastrar aluguel.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Rentals rental) {
            var r = _repo.GetRentalById(id, true, true);
            if (r == null) return BadRequest("Aluguel não encontrado.");
            _repo.Update(rental);
            if (_repo.SaveChanges()) {
                return Ok(rental);
            }
            return BadRequest("Erro ao realizar devolução.");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Rentals rental) {
            var r = _repo.GetRentalById(id, true, true);
            if (r == null) return BadRequest("Aluguel não encontrado.");
            _repo.Update(rental);
            if (_repo.SaveChanges()) {
                return Ok(rental);
            }
            return BadRequest("Erro ao realizar devolução.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var r = _repo.GetRentalById(id);
            if (r == null) return BadRequest("Aluguel não encontrado.");
            _repo.Delete(r);

            if (_repo.SaveChanges()) {
                return Ok("Aluguel deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar aluguel.");
        }
    }
}
