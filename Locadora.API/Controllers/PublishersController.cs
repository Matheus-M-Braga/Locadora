using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _service;

        public PublishersController(IPublishersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var publishers = await _service.GetAsync();
            if (publishers.IsSucess) return Ok(publishers);
            return BadRequest(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publisher = await _service.GetByIdAsync(id);
            if (publisher.IsSucess) return Ok(publisher);
            return BadRequest(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePublisherDto model)
        {
            var result = await _service.CreateAsync(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Publishers model)
        {
            var result = await _service.UpdateAsync(model);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSucess) return Ok(result);
            return BadRequest(result);
        }
    }
}
