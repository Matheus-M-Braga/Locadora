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
            var rentals = await _service.GetAsync();
            if (rentals.IsSucess) return Ok(rentals);
            return BadRequest(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _service.GetByIdAsync(id);
            if (rental.IsSucess) return Ok(rental);
            return BadRequest(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentalDto model)
        {
            var result = await _service.CreateAsync(model);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] RentalReturnDto model)
        {
            var result = await  _service.UpdateAsync(model);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if(result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
