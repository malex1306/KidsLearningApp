using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace KidsLearning.Backend.Models;

public class Child
{
    public int Id { get; set; }

    [Required]
    [MaxLength(20, ErrorMessage = "Name darf maximal 20 Zeichen haben.")]
    public string Name { get; set; } = string.Empty;

    [Required] [DataType(DataType.Date)] public DateTime DateOfBirth { get; set; }
    [Required] public string AvatarUrl { get; set; } = string.Empty;

    public string ParentId { get; set; } = string.Empty;
    public IdentityUser? Parent { get; set; }

    public ICollection<SubjectProgress> Progress { get; set; } = new List<SubjectProgress>();

    public int StarCount { get; set; } = 0;
    public int DailyLearningMinutes { get; set; } = 0;
    public DateTime? LastLearningDay { get; set; }
    public int ConsecutiveLearningDays { get; set; } = 0;

    public ICollection<Badge> Badges { get; set; } = new List<Badge>();
    [Required] public string Difficulty { get; set; } = string.Empty;


    public ICollection<Avatar> UnlockedAvatars { get; set; } = new List<Avatar>();

    public int Age => DateTime.Today.Year - DateOfBirth.Year
                                          - (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
}