using App_GDSC_workshops.Features.Subjects.Models;
using App_GDSC_workshops.Features.Subjects.Views;
using Microsoft.AspNetCore.Mvc;

namespace App_GDSC_workshops.Features.Subjects;

[ApiController]
[Route("subjects")]
public class SubjectsController : ControllerBase
{
    private static List<SubjectModel> _mockDBSubject = new List<SubjectModel>();
    
    public SubjectsController(){}

    [HttpPost]
    public SubjectResponse Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = request.Grades.ToList()
        };
        
        _mockDBSubject.Add(subject);
        
        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = request.Grades.ToList()
        };
    }

    [HttpGet]
    public IEnumerable<SubjectResponse> Get()
    {
        return _mockDBSubject.Select(
            subject => new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades.ToList()
            }).ToList();
    }

    [HttpGet("{id}")]
    public SubjectResponse Get([FromRoute] string id)
    {
        var subject = _mockDBSubject.FirstOrDefault(x => x.Id == id);
        
        if (subject is null)
            return null;

        return new SubjectResponse
        {
            Id = subject.Id,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail,
            Grades = subject.Grades.ToList()
        };
    }

    [HttpDelete("{id}")]
    public SubjectResponse Delete([FromRoute] string id)
    {
        var subject = _mockDBSubject.FirstOrDefault(x => x.Id == id);

        if (subject is null)
            return null;

        _mockDBSubject.Remove(subject);

        return new SubjectResponse
        {
            Id = subject.Id,
            Grades = subject.Grades,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail
        };
    }

    [HttpPatch("{id}")]
    public SubjectResponse Patch([FromRoute] string id, [FromBody] SubjectRequest request)
    {
        var subject = _mockDBSubject.FirstOrDefault(x => x.Id == id);

        if (subject is null)
            return null;

        subject.Grades = request.Grades;
        subject.Name = request.Name;
        subject.ProfessorMail = request.ProfessorMail;
        subject.Updated = DateTime.UtcNow;

        return new SubjectResponse
        {
            Id = subject.Id,
            Grades = subject.Grades,
            Name = subject.Name,
            ProfessorMail = subject.ProfessorMail
        };
    }
}