using App_GDSC_workshops.Features.Assignments.Models;
using App_GDSC_workshops.Features.Assignments.Views;
using Microsoft.AspNetCore.Mvc;

namespace App_GDSC_workshops.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentsController
{
    private static List<AssignmentModel> _mockDB = new List<AssignmentModel>();

    public AssignmentsController(){}

    [HttpPost]

    public AssignmentResponse Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            DeadLine = request.Deadline
        };
        
        _mockDB.Add(assignment);
        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
    }

    [HttpGet]
    public IEnumerable<AssignmentResponse> Get()
    {
        return _mockDB.Select(
            assignment => new AssignmentResponse
            {
                Id = assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DeadLine = assignment.DeadLine
            }).ToList();
    }

    [HttpGet("{id}")]
    public AssignmentResponse Get([FromRoute] string id)
    {
        var assignment = _mockDB.FirstOrDefault(x => x.Id == id);
        if (assignment is null)
        {
            return null;
        }

        return new AssignmentResponse
        {
            Id = assignment.Id,
            Subject = assignment.Subject,
            Description = assignment.Description,
            DeadLine = assignment.DeadLine
        };
        
        //delete update la toate
    }
}