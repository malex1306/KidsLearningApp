namespace KidsLearning.Backend.DTOs;


public class SubjectProgressDto
{
    public string SubjectName { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }
    
}
public class ChildDto
{
    public int ChildId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string LastActivity { get; set; } = string.Empty;
    public List<SubjectProgressDto> Progress { get; set; } = [];
}

public class ParentDashboardDto
{
    public string WelcomeMessage { get; set; } = string.Empty;
    public List<ChildDto> Children { get; set; } = [];
    public List<string> RecentActivities { get; set; } = [];
}