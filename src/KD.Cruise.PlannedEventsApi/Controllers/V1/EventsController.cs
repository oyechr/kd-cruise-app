using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlannedEventsApi.Data;
using PlannedEventsApi.Dtos; 
using PlannedEventsApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Asp.Versioning;

namespace PlannedEventsApi.Controllers.V1;

[ApiController]
[Route("v1/events")]
[ApiVersion("1.0")]
public class EventsController : ControllerBase
{
    private readonly PlannedEventsDbContext _context;
    private readonly ILogger<EventsController> _logger;

    public EventsController(PlannedEventsDbContext context, ILogger<EventsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Gets all planned events.")]
    [ProducesResponseType(typeof(IEnumerable<PlannedEventDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PlannedEventDto>>> GetPlannedEvents()
    {
        var events = await _context.PlannedEvents.ToListAsync();

        var result = events.Select(e => new PlannedEventDto
        {
            VoyageId = e.VoyageId,
            VesselId = e.VesselId,
            FromUtc = e.FromUtc,
            ToUtc = e.ToUtc,
            Event = e.Event
        });

        return Ok(result);
    }

    [HttpGet("{id}", Name = nameof(GetPlannedEvent))]
    [SwaggerOperation(Summary = "Gets a planned event by ID.")]
    [ProducesResponseType(typeof(PlannedEventDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlannedEventDto>> GetPlannedEvent(Guid id)
    {
        var e = await _context.PlannedEvents.FindAsync(id);
        if (e == null)
        {
            _logger.LogWarning("PlannedEvent not found: {Id}", id);
            return NotFound();
        }

        var dto = new PlannedEventDto
        {
            VoyageId = e.VoyageId,
            VesselId = e.VesselId,
            FromUtc = e.FromUtc,
            ToUtc = e.ToUtc,
            Event = e.Event
        };

        return Ok(dto);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Creates a new planned event.")]
    [ProducesResponseType(typeof(PlannedEventDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PlannedEventDto>> CreatePlannedEvent([FromBody] PlannedEventDto dto)
    {
        var entity = new PlannedEvent
        {
            VoyageId = dto.VoyageId,
            VesselId = dto.VesselId,
            FromUtc = dto.FromUtc,
            ToUtc = dto.ToUtc,
            Event = dto.Event
        };

        try
        {
            _context.PlannedEvents.Add(entity);
            await _context.SaveChangesAsync();

            var resultDto = new PlannedEventDto
            {
                Id = entity.Id, 
                VoyageId = entity.VoyageId,
                VesselId = entity.VesselId,
                FromUtc = entity.FromUtc,
                ToUtc = entity.ToUtc,
                Event = entity.Event
            };

            return CreatedAtAction(nameof(GetPlannedEvent), new { id = entity.Id }, resultDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create planned event");
            return StatusCode(500, "An error occurred while saving the event.");
        }
    }
}