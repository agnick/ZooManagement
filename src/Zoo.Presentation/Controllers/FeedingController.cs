using Microsoft.AspNetCore.Mvc;
using Zoo.Application.DTOs;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingController : ControllerBase
{
    private readonly IFeedingOrganizationService _service;

    public FeedingController(IFeedingOrganizationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedule()
    {
        var schedule = await _service.GetScheduleAsync();
        return Ok(schedule);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] FeedingScheduleDto dto)
    {
        await _service.AddFeedingAsync(dto);
        return Ok();
    }

    [HttpPost("mark/{id:guid}")]
    public async Task<IActionResult> Mark(Guid id)
    {
        await _service.MarkFedAsync(id);
        return Ok();
    }
}