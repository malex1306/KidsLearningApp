using System.ComponentModel.DataAnnotations;

namespace KidsLearning.Backend.Models;

public class LearningTask
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    public ICollection<Questions> Questions { get; set; } = [];
}