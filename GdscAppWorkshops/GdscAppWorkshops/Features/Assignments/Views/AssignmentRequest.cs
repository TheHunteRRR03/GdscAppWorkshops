using System.ComponentModel.DataAnnotations;

namespace App_GDSC_workshops.Features.Assignments.Views;

public class AssignmentRequest
{
    [Required] 
    public string Subject { get; set; }
    
    [Required] 
    public string Description { get; set; }
    
    [Required]
    public DateTime DeadLine { get; set; }
}