using App_GDSC_workshops.Database;
using App_GDSC_workshops.Features.Tests.Models;
using App_GDSC_workshops.Features.Tests.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GDSC_workshops.Features.Tests;

[ApiController]
[Route("tests")]
public class TestsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TestsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TestResponse>> Add(TestRequest request)
    {
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            TestDate = request.TestDate
        };

        var response = await _dbContext.Tests.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return Created("Test", response.Entity);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TestResponse>> Get()
    {
        var entities = await _dbContext.Tests.Select(
            test => new TestResponse
            {
                Id = test.Id,
                Subject = test.Subject,
                TestDate = test.TestDate
            }
        ).ToListAsync();

        return Ok(entities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TestResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        
        return entity is null ? NotFound("ete nu-i") : Ok(entity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TestResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("ete nu-i");

        var result = _dbContext.Tests.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return Ok(result.Entity);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TestResponse>> Patch([FromRoute] string id, [FromBody] TestRequest request)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("ete nu-i");

        entity.Subject = request.Subject;
        entity.TestDate = request.TestDate;
        entity.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return Ok(entity);
    }
}