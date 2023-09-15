﻿using AutoMapper;
using Locadora.API.Data;
using Locadora.API.Dtos;
using Locadora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Locadora.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public RentalsController(IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get() {
            var result = _repo.GetAllRentals();
            return Ok(_mapper.Map<IEnumerable<RentalsDto>>(result));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var rental = _repo.GetRentalById(id);
            if (rental == null) return BadRequest("Aluguel não encontrado");
            var rentalDto = _mapper.Map<RentalsDto>(rental);
            return Ok(rentalDto);
        }

        [HttpPost]
        public IActionResult Post(RentalsDto model) {

            var user = _repo.GetUserById((int)model.UserId);
            if (user == null) return BadRequest("Usuário informado não existe no registro.");
            var book = _repo.GetBookById((int)model.BookId);
            if (book == null) return BadRequest("Livro informado não existe no registro.");

            var rental = _mapper.Map<Rentals>(model);
            _repo.Add(rental);
            if (_repo.SaveChanges()) {
                return Created($"/api/Rentals/{rental.Id}", _mapper.Map<RentalsDto>(rental));
            }
            return BadRequest("Erro ao cadastrar aluguel.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, RentalReturnDto model) {
            var rental = _repo.GetRentalById(id);
            if (rental == null) return BadRequest("Aluguel não encontrado.");
            
            _mapper.Map(model, rental);
            _repo.Update(rental);
            if (_repo.SaveChanges()) {
                return Created($"/api/Rentals/{rental.Id}", _mapper.Map<RentalsDto>(rental));
            }
            return BadRequest("Erro ao realizar devolução.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var rental = _repo.GetRentalById(id);
            if (rental == null) return BadRequest("Aluguel não encontrado.");
            _repo.Delete(rental);

            if (_repo.SaveChanges()) {
                return Ok("Aluguel deletado com sucesso.");
            }
            return BadRequest("Erro ao deletar aluguel.");
        }
    }
}
