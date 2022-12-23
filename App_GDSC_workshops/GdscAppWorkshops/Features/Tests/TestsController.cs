using App_GDSC_workshops.Features.Tests.Models;
using App_GDSC_workshops.Features.Tests.Views;
using Microsoft.AspNetCore.Mvc;

namespace App_GDSC_workshops.Features.Tests;

[ApiController]
[Route("tests")]
public class TestsController : ControllerBase
{
    private static List<TestModel> _mockDbTests = new List<TestModel>();

    public TestsController() {}

    [HttpPost]
    public TestResponse Add(TestRequest request)
    {
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            TestDate = request.TestDate
        };

        _mockDbTests.Add(test);

        return new TestResponse()
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpGet]
    public IEnumerable<TestResponse> Get()
    {
        return _mockDbTests.Select(
            test => new TestResponse
            {
                Id = test.Id,
                Subject = test.Subject,
                TestDate = test.TestDate
            }).ToList();
    }

    [HttpGet("{id}")]
    public TestResponse Get([FromRoute] string id)
    {
        var test = _mockDbTests.FirstOrDefault(x => x.Id == id);
        
        if (test is null)
            return null;

        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpDelete("{id}")]
    public TestResponse Delete([FromRoute] string id)
    {
        var test = _mockDbTests.FirstOrDefault(x => x.Id == id);

        if (test is null)
            return null;

        _mockDbTests.Remove(test);

        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }

    [HttpPatch("{id}")]
    public TestResponse Patch([FromRoute] string id, [FromBody] TestRequest request)
    {
        var test = _mockDbTests.FirstOrDefault(x => x.Id == id);

        if (test is null)
            return null;

        test.Subject = request.Subject;
        test.TestDate = request.TestDate;
        test.Updated = DateTime.UtcNow;

        return new TestResponse
        {
            Id = test.Id,
            Subject = test.Subject,
            TestDate = test.TestDate
        };
    }
}