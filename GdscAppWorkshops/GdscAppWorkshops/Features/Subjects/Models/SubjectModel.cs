using App_GDSC_workshops.Base;

namespace App_GDSC_workshops.Features.Subjects.Models;

public class SubjectModel : Model
{
    public string Name { get; set; }
    
    public string ProfessorMail { get; set; }
    
    public List<Double> Grades { get; set; }
}