namespace KidsLearning.Backend.DTOs;

public class AddChildDto
{
    public string Name { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Difficulty { get; set; } = string.Empty;
}