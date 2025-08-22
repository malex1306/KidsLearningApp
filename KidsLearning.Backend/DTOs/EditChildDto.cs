namespace KidsLearning.Backend.DTOs;

public class EditChildDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? AvatarUrl { get; set; }
    
    public string Difficulty { get; set; } = string.Empty;
}