using Microsoft.AspNetCore.Identity;

namespace KidsLearning.Backend.Models;

public class Child
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;
    public string ParentId { get; set; } = string.Empty;
    public IdentityUser Parent { get; set; } = new IdentityUser();

    public ICollection<SubjectProgress> Progress { get; set; } = new List<SubjectProgress>();
}