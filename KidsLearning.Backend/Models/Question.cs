// src/KidsLearning.Backend.Models/Questions.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsLearning.Backend.Models;

public class Questions
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;

    [Required]
    public string CorrectAnswer { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();

    [ForeignKey("LearningTask")]
    public int LearningTaskId { get; set; }
    public LearningTask? LearningTask { get; set; }
}