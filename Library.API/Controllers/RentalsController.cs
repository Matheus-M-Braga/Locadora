using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Rental;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
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
        [SwaggerOperation(Summary = "GetAll")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var rentals = await _service.GetAll(filterDb);
            if (rentals.IsSucess) return Ok(rentals);
            return NotFound(rentals);
        }

        [HttpGet("count")]
        [SwaggerOperation(Summary = "GetCount")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllCount()
        {
            var rentals = await _service.GetAllCount();
            if (rentals.IsSucess) return Ok(rentals);
            return NotFound(rentals);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetById")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var rental = await _service.GetById(id);
            if (rental.IsSucess) return Ok(rental);
            return NotFound(rental);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Post([FromBody] CreateRentalDto model)
        {
            var result = await _service.Create(model);
            if (result.IsSucess) return Created($"/api/rentals/", result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Put([FromBody] UpdateRentalDto model)
        {
            var result = await _service.Update(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
