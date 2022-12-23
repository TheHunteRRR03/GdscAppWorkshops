using System.ComponentModel.DataAnnotations;

namespace App_GDSC_workshops.Features.Subjects.Views;

public class SubjectRequest
{
    [Required] 
    public string Name { get; set; }
    
    [Required] 
    public string ProfessorMail { get; set; }
    
    [Required] 
    public List<Double> Grades { get; set; }
}