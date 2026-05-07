using first_sprint.DTO;
using first_sprint.Models;
using first_sprint.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace first_sprint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDTO>>> GetAllAsync()
        {
            var events = await _eventService.GetAsync();
            var response = events.Select(e => new EventResponseDTO
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartAt = e.StartAt,
                EndAt = e.EndAt
            });
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponseDTO>> GetEventByIdAsync(Guid id)
        {
            var eventTime = await _eventService.GetByIdAsync(id);
            if (eventTime == null)
            {
                return NotFound(new { message = $"Событие с ID {id} не найдено" });
            }
            else
            {
                return Ok(new EventResponseDTO 
                {
                    Id = eventTime.Id,
                    Title = eventTime.Title,
                    Description = eventTime.Description,
                    StartAt = eventTime.StartAt, 
                    EndAt = eventTime.EndAt
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<EventResponseDTO>> CreateEvent([FromBody] CreateEventDTO createDTO)
        {
            if (string.IsNullOrWhiteSpace(createDTO.Title))
            {
                return BadRequest(new { message = "Необходим заголовок" });
            }
            if (createDTO.StartAt == default)
            {
                return BadRequest(new { message = "Необходимо время начала события" });
            }
            if (createDTO.EndAt == default)
            {
                return BadRequest(new { message = "Необходимо время завершения события" });
            }

            var newEvent = new Event
            {
                Title = createDTO.Title,
                Description = createDTO.Description,
                StartAt = createDTO.StartAt,
                EndAt = createDTO.EndAt
            };

            var createdEvent = await _eventService.CreateAsync(newEvent);
            var responseDto = new EventResponseDTO
            {
                Id = createdEvent.Id,
                Title = createdEvent.Title,
                Description = createdEvent.Description,
                StartAt = createdEvent.StartAt,
                EndAt = createdEvent.EndAt
            };
            return Created($"/api/events/{createdEvent.Id}", responseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventResponseDTO>> UpdateEvent(Guid id, [FromBody] UpdateEventDTO updateDTO)
        {
            if (string.IsNullOrWhiteSpace(updateDTO.Title))
            {
                return BadRequest(new { message = "Необходим заголовок" });
            }
            if (updateDTO.StartAt == default)
            {
                return BadRequest(new { message = "Необходимо время начала события" });
            }
            if (updateDTO.EndAt == default)
            {
                return BadRequest(new { message = "Необходимо время завершения события" });
            }

            var newEvent = new Event
            {
                Title = updateDTO.Title,
                Description = updateDTO.Description,
                StartAt = updateDTO.StartAt,
                EndAt = updateDTO.EndAt
            };

            var updatedEvent = await _eventService.UpdateAsync(id, newEvent);
            if (updatedEvent == null)
            {
                return NotFound(new { message = $"Событие с ID {id} не найдено" });
            }
            else
            {
                return Ok(new EventResponseDTO
                {
                    Id = updatedEvent.Id,
                    Title = updatedEvent.Title,
                    Description = updatedEvent.Description,
                    StartAt = updatedEvent.StartAt,
                    EndAt = updatedEvent.EndAt
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            var deletedEvent = await _eventService.DeleteAsync(id);
            if (!deletedEvent)
            {
                return NotFound(new { message = $"Событие с ID {id} не найдено" });
            }
            else
            {
                return NoContent();
            }
        }
    }
}
