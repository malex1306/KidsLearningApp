namespace KidsLearning.Backend.DTOs;

public class AvatarDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int UnlockStarRequirement { get; set; }
}