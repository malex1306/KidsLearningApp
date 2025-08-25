namespace KidsLearning.Backend.DTOs;

using KidsLearning.Backend.Models;

public class SubjectProgressDto
{
    public string SubjectName { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }
}

public class ChildDto
{
    public int ChildId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string AvatarUrl { get; set; } = string.Empty;
    public string LastActivity { get; set; } = string.Empty;
    public List<SubjectProgressDto> Progress { get; set; } = [];
    public int StarCount { get; set; }
    public int TotalStarsEarned { get; set; }
    public ICollection<Badge> Badges { get; set; } = new List<Badge>();
    public ICollection<AvatarDto> UnlockedAvatars { get; set; } = new List<AvatarDto>();

    public string Difficulty { get; set; } = string.Empty;
}

public class ParentDashboardDto
{
    public string WelcomeMessage { get; set; } = string.Empty;
    public List<ChildDto> Children { get; set; } = [];
    public List<string> RecentActivities { get; set; } = [];
}