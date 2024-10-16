using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepasoAhoraSi.Data;
using RepasoAhoraSi.Interfaces;

namespace RepasoAhoraSi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
    private readonly IEventsRepository _eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventsRepository.GetAll();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var events = await _eventsRepository.GetOne(id);
            if (events == null) return NotFound();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Events events)
        {
           int eventId = await _eventsRepository.Insert(events);
           return Ok(eventId);
        }

        
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Events events)
        {
            if (id != events.Id) return BadRequest();

            var result = await _eventsRepository.Update(events);
            if (!result) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _eventsRepository.Delete(id);
            if (!result) return NotFound();
            return Ok(result);
        }
    }
}
