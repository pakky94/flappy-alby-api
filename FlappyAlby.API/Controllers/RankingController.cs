using Microsoft.AspNetCore.Mvc;

namespace FlappyAlby.API.Controllers;

using Bogus;
using DTOs;

[ApiController]
[Route("[controller]")]
public class RankingController : ControllerBase
{
   private readonly ILogger<RankingController> _logger;

    public RankingController(ILogger<RankingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var faker = new Faker();
        var result = Enumerable.Range(1, 5)
            .Select((index, index1) => new PlayerDto(faker.Name.FirstName(), faker.Date.Timespan(TimeSpan.FromMinutes(5)), index + index1))
            .OrderBy(player => player.Total);

        return Ok(result);
    }
    
    [HttpPost]
    public IActionResult Post([FromBody] PlayerDto player)
    {
        return Ok();
    }
}