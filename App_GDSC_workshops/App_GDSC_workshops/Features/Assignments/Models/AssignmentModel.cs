using App_GDSC_workshops.Base;

namespace App_GDSC_workshops.Features.Assignments.Models;

public class AssignmentModel : Model
{
    public string Subject { get; set; }

    public string Description { get; set; }
    
    public DateTime DeadLine { get; set; }
}