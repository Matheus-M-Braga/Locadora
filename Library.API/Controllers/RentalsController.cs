using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Rental;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _service;

        public RentalsController(IRentalsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var rentals = await _service.GetAll(filterDb);
            if (rentals.StatusCode == HttpStatusCode.OK) return Ok(rentals);
            return NotFound(rentals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _service.GetById(id);
            if (rental.StatusCode == HttpStatusCode.OK)
                return Ok(rental);
            return NotFound(rental);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRentalDto model)
        {
            var result = await _service.Create(model);
            if (result.StatusCode == HttpStatusCode.Created)
                return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateRentalDto model)
        {
            var result = await _service.Update(model);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.StatusCode == HttpStatusCode.OK) return Ok(result);
            if (result.StatusCode == HttpStatusCode.NotFound) return NotFound(result);
            return BadRequest(result);
        }
    }
}
