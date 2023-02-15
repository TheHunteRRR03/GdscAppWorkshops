using App_GDSC_workshops.Database;
using App_GDSC_workshops.Features.Assignments.Models;
using App_GDSC_workshops.Features.Assignments.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GDSC_workshops.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public AssignmentsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AssignmentResponse>> Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            DeadLine = request.DeadLine
        };

        var response = await _dbContext.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();

        return Created("Assignment", response.Entity);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AssignmentResponse>> Get()
    {
        var entities = await _dbContext.Assignments.Select(
            assignment => new AssignmentResponse
            {
                Id = assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine 
            }).ToListAsync();

        return Ok(entities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AssignmentResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);

        return entity is null ? NotFound("ete nu-i") : Ok(entity);
    }
    
    [HttpDelete("{id})")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AssignmentResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("ete nu-i");

        var result = _dbContext.Assignments.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return Ok(result.Entity);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    public async Task<ActionResult<AssignmentResponse>> Patch([FromRoute] string id, [FromBody] AssignmentRequest request) 
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
             return NotFound("ete nu-i");
         
        entity.DeadLine = request.DeadLine;
        entity.Description = request.Description;
        entity.Subject = request.Subject;
        entity.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return Ok(entity);
    }
}