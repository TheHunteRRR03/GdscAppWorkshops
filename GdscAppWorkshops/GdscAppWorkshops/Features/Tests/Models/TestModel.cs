using App_GDSC_workshops.Base;
using App_GDSC_workshops.Base.Models;

namespace App_GDSC_workshops.Features.Tests.Models;

public class TestModel : Model
{
    public string Subject { get; set; }
    
    public DateTime TestDate { get; set; }
}