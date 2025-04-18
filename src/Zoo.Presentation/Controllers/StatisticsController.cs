using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IZooStatisticsService _service;

    public StatisticsController(IZooStatisticsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var stats = await _service.GetStatisticsAsync();
        return Ok(stats);
    }
}