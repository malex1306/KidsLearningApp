using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsLearning.Backend.Models;

public class ChildCompletedTask
{
    [Key] public int Id { get; set; }

    [ForeignKey("Child")] public int ChildId { get; set; }
    public Child? Child { get; set; }

    [ForeignKey("LearningTask")] public int LearningTaskId { get; set; }
    public LearningTask? LearningTask { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.Now;
}