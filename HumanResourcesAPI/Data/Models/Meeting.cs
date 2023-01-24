namespace HumanResourcesAPI.Data.Models;

public class Meeting
{
    public int Id {  get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public string InterviewDate { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public string EmploymentDate { get; set; }
    public bool Employed { get; set; }

    public int PersonId { get; set; }
    public Person? Person { get; set; }
}
