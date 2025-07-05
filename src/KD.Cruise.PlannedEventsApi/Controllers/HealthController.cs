using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PlannedEventsApi.Controllers
{
    [ApiController]
    [Route("healthz")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(Summary = "Checks the health status of the API.")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GetHealth()
        {
            return Ok(new { Status = "Healthy" });
        }
    }
}