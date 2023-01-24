namespace HumanResourcesAPI.Data.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Birth { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }

    public ICollection<Meeting> Meetings { get; set; }
}
