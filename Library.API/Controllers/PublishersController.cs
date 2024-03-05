using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _service;

        public PublishersController(IPublishersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var publishers = await _service.GetAll(filterDb);
            if (publishers.StatusCode == HttpStatusCode.OK) return Ok(publishers);
            return NotFound(publishers);
        }

        [HttpGet("getsummary")]
        public async Task<IActionResult> GetSummary()
        {
            var publishers = await _service.GetSummary();
            if (publishers.StatusCode == HttpStatusCode.OK) return Ok(publishers);
            return NotFound(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publisher = await _service.GetById(id);
            if (publisher.StatusCode == HttpStatusCode.OK) return Ok(publisher);
            return NotFound(publisher);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePublisherDto model)
        {
            var result = await _service.Create(model);
            if (result.StatusCode == HttpStatusCode.Created) return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdatePublisherDto model)
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
