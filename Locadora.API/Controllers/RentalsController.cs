using Locadora.API.Dtos;
using Locadora.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _service;

        public RentalsController(IRentalsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rentals = await _service.GetAll();
            if (rentals.IsSucess) return Ok(rentals);
            return BadRequest(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _service.GetById(id);
            if (rental.IsSucess) return Ok(rental);
            return BadRequest(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentalDto model)
        {
            var result = await _service.Create(model);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] RentalReturnDto model)
        {
            var result = await  _service.Update(model);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
