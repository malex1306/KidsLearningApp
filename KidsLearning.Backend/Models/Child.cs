using Microsoft.AspNetCore.Identity;

namespace KidsLearning.Backend.Models;

public class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string AvatarUrl { get; set; } = string.Empty;

    public string ParentId { get; set; } = string.Empty;
    public IdentityUser? Parent { get; set; }

    public ICollection<SubjectProgress> Progress { get; set; } = new List<SubjectProgress>();

    // Belohnungssystem Eigenschaften
    public int StarCount { get; set; } = 0;
    public int DailyLearningMinutes { get; set; } = 0;
    public DateTime? LastLearningDay { get; set; }
    public int ConsecutiveLearningDays { get; set; } = 0;

    // Navigationseigenschaften für Belohnungen
    public ICollection<Badge> Badges { get; set; } = new List<Badge>();

    public string Difficulty { get; set; } = string.Empty;
}