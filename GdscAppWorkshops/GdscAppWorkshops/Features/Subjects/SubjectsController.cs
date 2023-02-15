using App_GDSC_workshops.Database;
using App_GDSC_workshops.Features.Subjects.Models;
using App_GDSC_workshops.Features.Subjects.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GDSC_workshops.Features.Subjects;

[ApiController]
[Route("subjects")]
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SubjectsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubjectResponse>>Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = new List<double>()
        };

        var response = await _dbContext.Subjects.AddAsync(subject);
        await _dbContext.SaveChangesAsync();
        
        return Created("Subject", response.Entity);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SubjectResponse>> Get()
    {
        var entities = await _dbContext.Subjects.Select(
            subject => new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades
            }
        ).ToListAsync();
        
        return Ok(entities);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SubjectResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        
        return entity is null ? NotFound("ete nu-i") : Ok(entity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SubjectResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("ete nu-i");

        var result = _dbContext.Subjects.Remove(entity);
        await _dbContext.SaveChangesAsync();
        
        return Ok(result.Entity);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SubjectResponse>> Patch([FromRoute] string id, [FromBody] SubjectRequest request)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("ete nu-i");
        
        entity.Name = request.Name;
        entity.ProfessorMail = request.ProfessorMail;
        entity.Updated = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        
        return Ok(entity);
    }
}