namespace KidsLearning.Backend.Models;

public class SubjectProgress
{
    public int Id { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public int ChildId { get; set; }
    public Child Child { get; set; } = new Child();
}