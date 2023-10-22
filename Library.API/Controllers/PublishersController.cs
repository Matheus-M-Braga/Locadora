using Library.Business.Interfaces.IServices;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Pagination;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
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
        [SwaggerOperation(Summary = "GetAll")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get([FromQuery] FilterDb filterDb)
        {
            var publishers = await _service.GetAll(filterDb);
            if (publishers.IsSucess) return Ok(publishers);
            return NotFound(publishers);
        }

        [HttpGet("getallselect")]
        [SwaggerOperation(Summary = "GetAllSelect")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetAllSelect()
        {
            var publishers = await _service.GetAllSelect();
            if (publishers.IsSucess) return Ok(publishers);
            return NotFound(publishers);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "GetById")]
        [SwaggerResponse(200)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var publisher = await _service.GetById(id);
            if (publisher.IsSucess) return Ok(publisher);
            return NotFound(publisher);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Post([FromBody] CreatePublisherDto model)
        {
            var result = await _service.Create(model);
            if (result.IsSucess) return StatusCode(201, result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Put([FromBody] UpdatePublisherDto model)
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
